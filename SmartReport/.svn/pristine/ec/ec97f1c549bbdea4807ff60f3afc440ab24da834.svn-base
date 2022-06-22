using System.Collections.Generic;
using RazorEngine;
using RazorEngine.Templating;
using RazorEngine.Configuration;
using System;
namespace Bkav.eGovCloud.Core.RazorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class RazorEngineUtil : IDisposable
    {
        private readonly TemplateService _service;

        /// <summary>
        /// Contructor
        /// </summary>
        public RazorEngineUtil()
        {
            var config = new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(MyCustomTemplateBase<>)
            };
            _service = new TemplateService(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyTemplate"></param>
        /// <param name="keyModel"></param>
        /// <returns></returns>
        public string FormatKey(string keyTemplate, IEnumerable<IDictionary<string, object>> keyModel)
        {
            //var config = new TemplateServiceConfiguration
            //{
            //    BaseTemplateType = typeof(MyCustomTemplateBase<>)
            //};

            //using (var service = new TemplateService(config))
            //{
            //    return service.Parse(keyTemplate, keyModel, null, null);
            //}
            return _service.Parse(keyTemplate, keyModel, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public string FormatStringLiteral(string template, dynamic Model)
        {
            return _service.Parse(template, Model, null, null);
        }

        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
        }

        #endregion
    }
}
