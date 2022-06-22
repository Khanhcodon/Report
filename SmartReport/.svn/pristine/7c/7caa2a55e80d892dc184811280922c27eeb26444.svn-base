using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Mail;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceHelper
    {
        
        /// <summary>
        /// Ctor
        /// </summary>
        public InvoiceHelper()
        {
           
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="ACpass"></param>
        /// <param name="xmlInvData"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="pattern"></param>
        /// <param name="serial"></param>
        /// <param name="convert"></param>
        public string PublishInvoice(string Account, string ACpass,
            string xmlInvData, string username,
            string password, string pattern, 
            string serial, int convert)
        {
            var services = new PublishInvoice.PublishServiceSoapClient();
            var result = services.ImportAndPublishInv(Account, ACpass, xmlInvData, username, password, pattern, serial, convert);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="ACpass"></param>
        /// <param name="xmlInvData"></param>
        /// <param name="username"></param>
        /// <param name="pass"></param>
        /// <param name="fkey"></param>
        /// <param name="AttachFile"></param>
        /// <param name="convert"></param>
        public string AdjustInvoice(string Account, string ACpass, 
            string xmlInvData, string username, string pass, string fkey, 
            string AttachFile, int convert)
        {
            var services = new BusinessInvoice.BusinessServiceSoapClient();
            var result = services.adjustInv(Account, ACpass, xmlInvData, username, pass, fkey, convert);
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Acpass"></param>
        /// <param name="xmlInvData"></param>
        /// <param name="username"></param>
        /// <param name="pass"></param>
        /// <param name="fkey"></param>
        /// <param name="Attachfile"></param>
        /// <param name="convert"></param>
        public void ReplaceInvoice(string Account, string Acpass, 
            string xmlInvData, string username, string pass, string fkey,
            string Attachfile, int convert)
        {
            var services = new BusinessInvoice.BusinessServiceSoapClient();
            var result = services.replaceInv(Account, Acpass, xmlInvData, username, pass, fkey, convert);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="ACpass"></param>
        /// <param name="fkey"></param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        public string CancelInvoice(string Account, string ACpass, string fkey, string userName, string userPass)
        {
            var services = new BusinessInvoice.BusinessServiceSoapClient();
            var result = services.cancelInv(Account, ACpass, fkey, userName, userPass);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <returns></returns>
        public string ListInvoiceFkey(string key, string userName, string userPass)
        {
            var services = new PortalService.PortalServiceSoapClient();
            var result = services.listInvByCusFkey(key, null, null, userName, userPass);
            result = Xml2JSON(result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <returns></returns>
        public string DetailFkey(string key, string userName, string userPass)
        {
            var services = new PortalService.PortalServiceSoapClient();
            var result = services.getInvViewFkeyNoPay(key, userName, userPass);
            //result = Xml2JSON(result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string Xml2JSON(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }
    }
}
