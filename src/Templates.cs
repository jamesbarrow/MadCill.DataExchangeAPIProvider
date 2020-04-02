using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadCill.DataExchangeAPIProvider
{
    public class Templates
    {
        public struct ReadFromJsonApiStepTemplate
        {
            public static readonly ID ID = new ID(IDValue);
            public const string IDValue = "{4A056980-F8AE-45D1-906F-D7A6C8D4E5E1}";
        }

        public struct JsonApiEndpointTemplate
        {
            public static readonly ID ID = new ID(IDValue);
            public const string IDValue = "{FAE70A67-8CAF-4700-AB3D-123C6CE27799}";
        }

        public struct DictionaryPropertyValueAccessorTemplate
        {
            public static readonly ID ID = new ID(IDValue);
            public const string IDValue = "{AB06851D-AF6E-4289-808A-9395A289605C}";
        }
    }
}