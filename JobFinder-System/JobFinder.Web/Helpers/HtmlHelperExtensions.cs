﻿namespace JobFinder.Web.Helpers
{
    using System.Web;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class HtmlHelperExtensions
    {
        public static IHtmlString ToJson(this HtmlHelper helper, object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            return helper.Raw(JsonConvert.SerializeObject(obj, settings));
        }
    }
}
