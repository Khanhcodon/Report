using Bkav.eGovCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoicePublishObject
    {
        public string key { get; set; }
        public string CusCode { get; set; }
        public string ArisingDate { get; set; }
        public string CusName { get; set; }
        public string CusPhone { get; set; }
        public string CusTaxCode { get; set; }
        public string Total { get; set; }
        public string Type { get; set; }
        public string KindOfService { get; set; }
        public string Amount { get; set; }
        public string AmountInWords { get; set; }
        public string VATAmount { get; set; }
        public string VATRate { get; set; }
        public string CusAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string Extra { get; set; }
        public List<ProductInvoice> Products { get; set; }
        private Dictionary<string, string> ReplaceParams()
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>(){
                {"{key}", key},
                {"{CusCode}", CusCode},
                {"{ArisingDate}", ArisingDate},
                {"{CusName}", CusName},
                {"{CusPhone}", CusPhone},
                {"{CusTaxCode}", CusTaxCode},
                {"{Total}", Total},
                {"{Type}", Type},
                {"{KindOfService}", KindOfService},
                {"{Amount}", Amount},
                {"{AmountInWords}", AmountInWords},
                {"{VATAmount}", VATAmount},
                {"{VATRate}", VATRate},
                {"{CusAddress}", CusAddress},
                {"{PaymentMethod}", PaymentMethod},
                {"{Extra}", Extra},
                {"{Products}", GetXmlProducts()},
            };
            return replacements;
        }
        private string GetXmlProducts()
        {
            if (Products == null)
            {
                return @"<Products>
                            <Product>
                              <Code></Code>
                              <ProdName></ProdName>
                              <ProdUnit>ai</ProdUnit>
                              <ProdQuantity></ProdQuantity>
                              <ProdPrice></ProdPrice>
                              <Total></Total>
                              <Amount></Amount>
                            </Product>
                        </Products>";
            }
            else
            {
                var products = "<Products>";
                foreach (var product in Products)
                {
                    var strPro = @"<Product>
                              <Code/>
                              <ProdName>{0}</ProdName>
                              <ProdUnit></ProdUnit>
                              <ProdQuantity/>
                              <ProdPrice/>
                              <Total>{2}</Total>
                              <Amount>{1}</Amount>
                            </Product>";
                    products = products + string.Format(strPro, product.ProdName, product.Amount, product.Total);
                }
                  products = products + "</Products>";
                return products;
            }
        }
        public string GetXmlInsertInvoice()
        {
            var xml = @"<Invoices> 
                 <Inv>
                <key>{key}</key> <!--Key duy nhất ứng với từng biên lai. Theo trao đổi với các anh đây sẽ là mã 1 cửa-->
                  <Invoice>
                        <CusCode>{CusCode}</CusCode> <!--Mã ứng với người nộp tiền. -->
                        <ArisingDate>{ArisingDate}</ArisingDate> <!--Ngày nộp tiền-->
                        <CusName>{CusName}</CusName> <!--Tên người nộp tiền-->
                        <Total>{Total}</Total> <!--Chỉ cần có thẻ, không cần truyền giá trị. Total nên truyền luôngía trị số tiền nộp-->
                        <Amount>{Amount}</Amount> <!--Số tiền nộp-->
                        <AmountInWords>{AmountInWords}</AmountInWords> <!--Số tiền nộp bằng chữ-->
                        <VATAmount>{VATAmount}</VATAmount> <!--Bắt buộc có thẻ. Để mặc định bằng 0-->
                        <VATRate>{VATRate}</VATRate><!--Bắt buộc có thẻ. Để mặc định bằng 0-->
                        <CusAddress></CusAddress><!--Chỉ cần có thẻ, không cần truyền giá trị-->
                       <PaymentMethod>{PaymentMethod}</PaymentMethod> <!--Hình thức thanh toán-->
                       <Extra>{Extra}</Extra><!--tên loại phí lệ phí hiển thị trên biên lai-->
                        {Products}
                  </Invoice>
                </Inv>
                </Invoices>
                ";
            var paramsStr = ReplaceParams();
            foreach (string key in paramsStr.Keys)
            {
                xml = xml.Replace(key, paramsStr[key]);
            }
            return xml;
        }

        public string GetXmlAdjustInvoice()
        {
            var xml = @"<AdjustInv>
                            <key>{key}</key >
                            <CusCode>{CusCode}</CusCode>
                            <CusName>{CusName}</CusName>
                            <CusAddress>{CusAddress}</CusAddress>
                            <CusPhone>{CusPhone}</CusPhone>
                            <CusTaxCode>{CusTaxCode}</CusTaxCode>
                            <PaymentMethod>{PaymentMethod}</PaymentMethod>
                            <KindOfService>{KindOfService}</KindOfService>
                            <Type>{Type}</Type>
                            {Products}
                            <Total>{Total}</Total>
                            <VATRate>{VATRate}</VATRate>
                            <VATAmount>{VATAmount}</VATAmount>
                            <Amount>{Amount}</Amount>
                            <AmountInWords>{AmountInWords}</AmountInWords>
                            <Extra>{Extra}</Extra>
                       </AdjustInv>
                ";
            var paramsStr = ReplaceParams();
            foreach (string key in paramsStr.Keys)
            {
                xml = xml.Replace(key, paramsStr[key]);
            }
            return xml;
        }

        public string GetXmlReplaceInvoice()
        {
            var xml = @"<ReplaceInv>
                            <key>{key}</key >
                            <CusCode>{CusCode}</CusCode>
                            <CusName>{CusName}</CusName>
                            <CusAddress>{CusAddress}</CusAddress>
                            <CusPhone>{CusPhone}</CusPhone>
                            <CusTaxCode>{CusTaxCode}</CusTaxCode>
                            <PaymentMethod>{PaymentMethod}</PaymentMethod>
                            <KindOfService>{KindOfService}</KindOfService>
                            <Type>{Type}</Type>
                            {Products}
                            <Total>{Total}</Total>
                            <VATRate>{VATRate}</VATRate>
                            <VATAmount>{VATAmount}</VATAmount>
                            <Amount>{Amount}</Amount>
                            <AmountInWords>{AmountInWords}</AmountInWords>
                            <Extra>{Extra}</Extra>
                        </ReplaceInv> ";
            var paramsStr = ReplaceParams();
            foreach (string key in paramsStr.Keys)
            {
                xml = xml.Replace(key, paramsStr[key]);
            }
            return xml;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProductInvoice
    {
        public string Code { get; set; }
        public string ProdName { get; set; }
        public string ProdUnit { get; set; }
        public string ProdQuantity { get; set; }
        public string ProdPrice { get; set; }
        public string Total { get; set; }
        public string Amount { get; set; }
    }
}
