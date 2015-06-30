using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VABEntLib6Playground.Infra
{
    public class EntLibDataAnotationProvider : DataAnnotationsModelValidatorProvider
    {
        #region Fields
        private static Dictionary<Type, DataAnnotationsModelValidationFactory> AttributeFactories = new Dictionary<Type, DataAnnotationsModelValidationFactory> 
        {         
            { 
                typeof(RequiredAttribute), 
                (metadata, context, attribute) => new RequiredAttributeAdapter(metadata, context, (RequiredAttribute)attribute) 
            }, 
            { 
                typeof(RegularExpressionAttribute), 
                (metadata, context, attribute) => new RegularExpressionAttributeAdapter(metadata, context, (RegularExpressionAttribute)attribute) 
            } 
        }; 
        #endregion

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            List<ModelValidator> validators = new List<ModelValidator>();
            List<ValidatorData> validatorDataList = metadata.ContainerType != null ? metadata.ContainerType.ExtractRules(metadata.PropertyName) : new List<ValidatorData>();
            return GetValidators(metadata, context, validatorDataList);
        }

        #region Private Methods
        private IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<ValidatorData> validatorData)
        {
            List<ModelValidator> validators = new List<ModelValidator>();
            foreach (var item in validatorData)
            {
                if (item.GetType().Name == "OrCompositeValidatorData")
                {
                    validators.AddRange(GetValidators(metadata, context, ((OrCompositeValidatorData)item).Validators.Where(v => !((ValueValidatorData)v).Negated)));
                }
                else
                    validators.Add(GetValidator(metadata, context, item));
            }

            return validators.Where(item => item != null);
        }

        private ModelValidator GetValidator(ModelMetadata metadata, ControllerContext context, ValidatorData item)
        {
            DataAnnotationsModelValidationFactory factory;
            if (item.GetType().Name == "NotNullValidatorData")
            {
                ValidationAttribute attribute = new RequiredAttribute { ErrorMessage = item.MessageTemplate };
                if (AttributeFactories.TryGetValue(attribute.GetType(), out factory))
                    return factory(metadata, context, attribute);
            }
            else if (item.GetType().Name == "RegexValidatorData")
            {
                RegexValidatorData regexData = (RegexValidatorData)item;

                RegularExpressionAttribute regexAttribute = new RegularExpressionAttribute(regexData.Pattern) { ErrorMessage = item.GetMessageTemplate() };

                if (AttributeFactories.TryGetValue(regexAttribute.GetType(), out factory))
                    return factory(metadata, context, regexAttribute);
            }

            return null;
        }
        #endregion
    }
}