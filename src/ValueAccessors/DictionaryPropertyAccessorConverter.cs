using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.DataAccess.Readers;
using Sitecore.DataExchange.DataAccess.Writers;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadCill.DataExchangeAPIProvider.ValueAccessors
{
    [SupportedIds(DictionaryPropertyAccessorTemplateId)]
    public class DictionaryPropertyAccessorConverter : PropertyValueAccessorConverter
    {
        public const string DictionaryPropertyAccessorTemplateId = Templates.DictionaryPropertyValueAccessorTemplate.IDValue;
        public const string TemplateFieldPropertyName = "PropertyName";

        public DictionaryPropertyAccessorConverter(IItemModelRepository repository) : base(repository)
        {
        }
        protected override IValueReader GetValueReader(ItemModel source)
        {
            var reader = base.GetValueReader(source);
            if (reader == null)
            {
                var propertyName = this.GetStringValue(source, TemplateFieldPropertyName);
                if (string.IsNullOrEmpty(propertyName))
                {
                    return null;
                }
                reader = new DictionaryValueReader<string, string>(propertyName);
            }
            return reader;
        }
        protected override IValueWriter GetValueWriter(ItemModel source)
        {
            var writer = base.GetValueWriter(source);
            if (writer == null)
            {
                var propertyName = this.GetStringValue(source, TemplateFieldPropertyName);
                if (string.IsNullOrEmpty(propertyName))
                {
                    return null;
                }
                writer = new DictionaryValueWriter<string, string>(propertyName);
            }
            return writer;
        }
    }
}