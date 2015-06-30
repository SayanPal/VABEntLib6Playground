using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VABEntLib6Playground.Models
{
    public class UxModel
    {
        public UxModel()
        {
            AnotherProp = new AnotherUxModel();
            AnotherPropList = new List<AnotherUxModel> { new AnotherUxModel(), new AnotherUxModel() };
        }
        public string Name { get; set; }
        public string NameForCompositeValidator { get; set; }
        public AnotherUxModel AnotherProp { get; set; }
        public List<AnotherUxModel> AnotherPropList { get; set; }
    }

    public class AnotherUxModel
    {
        public string AnotherName { get; set; }
    }
}