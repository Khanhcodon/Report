#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Business.Utils;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Core.Exceptions;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.History;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Models;
using Bkav.eGovCloud.Search;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovOnline.Business.Customer;
using Microsoft.AspNet.SignalR;
using Microsoft.JScript;
using Newtonsoft.Json.Linq;
using SolrNet.Utils;
using System.Data;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using Bkav.eGovCloud.Core.Workflow;
using Bkav.eGovCloud.Business.Objects;
using Bkav.eGovCloud.Business.Objects.CacheObjects;

#endregion

namespace Bkav.eGovCloud.Controllers
{
	//[RequireHttps]
	public class DocumentInvoiceController : CustomerBaseController
	{
        #region Readonly & Static Fields
        private DocumentBll _documentService;
		#endregion

		#region C'tors

		public DocumentInvoiceController(
            DocumentBll documentService
        )
		{
            _documentService = documentService;
		}

        #endregion

        #region Instance Methods
        public JsonResult Demo(string key = "")
        {
            var xml = @"<Invoices> 
                 <Inv>
                <key>test123s23d2</key>
                  <Invoice>
                        <CusCode>KH000001</CusCode>
                        <ArisingDate></ArisingDate>
                        <CusName>Công ty TNHH Đào Tạo và Dịch Vụ Hoàng Nguyên</CusName> 
                        <Buyer>Công ty TNHH Đào Tạo và Dịch Vụ Hoàng Nguyên</Buyer>
                        <Total>123000</Total>
                        <Amount>123000</Amount>
                        <AmountInWords>một trăm hai mươi ba ngàn đồng</AmountInWords> 
                        <VATAmount>0</VATAmount>
                        <VATRate>0</VATRate>
                        <CusAddress>Ha Noi</CusAddress>
                       <PaymentMethod>TM</PaymentMethod>
                       <Extra>phí Hoàn thành</Extra>
                        <Products>
							<Product>
                              <Code/>
                              <ProdName>phí Hoàn thành</ProdName>
                              <ProdUnit></ProdUnit>
                              <ProdQuantity/>
                              <ProdPrice/>
                              <Total></Total>
                              <Amount>123000</Amount>
                            </Product>
                        <Product>
                              <Code/>
                              <ProdName>phí Hoàn thành1</ProdName>
                              <ProdUnit></ProdUnit>
                              <ProdQuantity/>
                              <ProdPrice/>
                              <Total></Total>
                              <Amount>1230000</Amount>
                            </Product>
                  </Invoice>
                </Inv>
                </Invoices>";
            var obj = new InvoicePublishObject();
            obj.CusCode = "KH000001";
            obj.ArisingDate = "14/01/2019";
            obj.CusName = "Nguyễn Việt Dũng";
            obj.Total = "0";
            obj.Amount = "200000";
            obj.AmountInWords = "Hai trăm nghìn đồng";
            obj.VATAmount = "0";
            obj.VATRate = "0";
            obj.CusAddress = "";
            obj.PaymentMethod = "TM";
            obj.Extra = "Phí thành lập doanh nghiệp";
            InvoiceHelper invoiceHelper = new InvoiceHelper();
            var result = invoiceHelper.PublishInvoice("phathanhbienlai02", "123456", xml, "bienlaiservice", "123456", "01BLP0-001", "BL-18E", 0);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImportInvoice(string strInvoice)
        {
            var invoice = JsonConvert.DeserializeObject<InvoicePublishObject>(strInvoice);
            invoice.Total = "0";
            invoice.VATAmount = "0";
            invoice.VATRate = "0";
            invoice.PaymentMethod = "TM";
            invoice.ArisingDate = DateTime.Now.ToString("dd/MM/yyyy");

            InvoiceHelper invoiceHelper = new InvoiceHelper();
            var result = invoiceHelper.PublishInvoice("phathanhbienlai02", "123456", invoice.GetXmlInsertInvoice(), "bienlaiservice", "123456", "01BLP0-001", "BL-18E", 0);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AdjustInvoice(string strInvoice)
        {
            var invoice = JsonConvert.DeserializeObject<InvoicePublishObject>(strInvoice);
            invoice.Total = "0";
            invoice.VATAmount = "0";
            invoice.VATRate = "0";
            invoice.PaymentMethod = "TM";
            invoice.ArisingDate = DateTime.Now.ToString("dd/MM/yyyy");

            InvoiceHelper invoiceHelper = new InvoiceHelper();
            var result = invoiceHelper.AdjustInvoice("phathanhbienlai02", "123456", invoice.GetXmlAdjustInvoice(), "bienlaiservice", "123456", "01BLP0-001", "BL-18E", 0);
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveInvoice(string fKey)
        {
            InvoiceHelper invoiceHelper = new InvoiceHelper();
            var result = invoiceHelper.CancelInvoice("phathanhbienlai02", "123456", fKey, "bienlaiservice", "123456");
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LookupInvoice(string fKey)
        {
            InvoiceHelper invoiceHelper = new InvoiceHelper();
            var result = invoiceHelper.ListInvoiceFkey(fKey, "bienlaiservice", "123456");
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DetailInvoice(string fKey)
        {
            InvoiceHelper invoiceHelper = new InvoiceHelper();
            var result = invoiceHelper.DetailFkey(fKey, "bienlaiservice", "123456");
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}