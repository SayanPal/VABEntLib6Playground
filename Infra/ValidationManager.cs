using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VABEntLib6Playground.Infra
{
    public static class ValidationManager
    {
        static string validationFilePath = HttpContext.Current.Server.MapPath("~/ValidationFiles/ValidationFile.config");

        public static ValidationResults Validate(object objectToValidate)
        {
            FileConfigurationSource fileConfigurationsource = new FileConfigurationSource(validationFilePath);
            Validator validator = ValidationFactory.CreateValidator(objectToValidate.GetType(), objectToValidate.GetType().Name, fileConfigurationsource);

            var validationResult = validator.Validate(objectToValidate);
            return validationResult;
        }

        public static List<ValidatorData> ExtractRules(this Type targetType, string propertyName)
        {
            FileConfigurationSource fileConfigurationsource = new FileConfigurationSource(validationFilePath);
            ValidationSettings settings = (ValidationSettings)fileConfigurationsource.GetSection("validation");

            ValidatedTypeReference rules = settings.Types.FirstOrDefault(item => item.Name.Equals(targetType.FullName));

            return rules.Rulesets.SelectMany(item => item.Properties).Where(item => item.Name.Equals(propertyName)).SelectMany(item => item.Validators).ToList();
        }
    }
}