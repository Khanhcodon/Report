using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Core.License
{
    /// <summary>
    /// 
    /// </summary>
    public static class LicenseHelper
    {
        /// <summary>
        /// Kiểm tra chữ ký trong file bản quyền
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public static bool CheckSignatureXml(XmlDocument xmlDocument)
        {
            var keyPublic = new RSACryptoServiceProvider();
            keyPublic.FromXmlString("<RSAKeyValue><Modulus>t+H1DeC1oX6R/Xf9GWgLpfxwJoFwMKBq14IV4vvk1Bf3L0TxXFFQBXpwFHRPiMr1ABCmpD15xZuhUUVnnQx0gt8kL97692NvJajsg5TCT2lJIdp5nWniwFNBbRaye5UH8kbN1ZCJT82XAxEMLEvLis0QysxonupL6BcPoy6/wd8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

            var signedXml = new SignedXml(xmlDocument);
            var nodeList = xmlDocument.GetElementsByTagName("Signature");
            signedXml.LoadXml((XmlElement)nodeList[0]);

            return signedXml.CheckSignature(keyPublic);
        }

        /// <summary>
        /// Đọc nội dung bản quyền
        /// </summary>
        /// <param name="licensePath"></param>
        /// <returns></returns>
        public static LicenseInfo ReadLicense(string licensePath)
        {
            var bytes = Convert.FromBase64String(File.ReadAllText(licensePath));
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(new MemoryStream(bytes));
            if (!CheckSignatureXml(xmlDocument))
            {
                return null;
            }

            var model = new LicenseInfo();
            try
            {
                var nodeCustomerName = xmlDocument.GetElementsByTagName("customerName");
                model.CustomerName = nodeCustomerName[0].InnerText;

                var nodePhone = xmlDocument.GetElementsByTagName("phone");
                model.Phone = nodePhone[0].InnerText;

                var nodeEmail = xmlDocument.GetElementsByTagName("email");
                model.Email = nodeEmail[0].InnerText;

                var nodeTo = xmlDocument.GetElementsByTagName("to");
                model.ToDate = DateTime.Parse(nodeTo[0].InnerText, CultureInfo.GetCultureInfo("en").DateTimeFormat);

                var nodeTotalUser = xmlDocument.GetElementsByTagName("totalUser");
                model.TotalUser = int.Parse(nodeTotalUser[0].InnerText);

                var licenseCode = xmlDocument.GetElementsByTagName("licenseCode");
                model.LicenseCode = licenseCode[0].InnerText;

                var lastUpdate = xmlDocument.GetElementsByTagName("lastUpdate");
                model.LastUpdate = DateTime.Parse(lastUpdate[0].InnerText, CultureInfo.GetCultureInfo("en").DateTimeFormat);
            }
            catch { }

            //var nodeMotherBoardSerial = xmlDocument.GetElementsByTagName("motherBoardSerial");
            //model.MotherBoardSerial = nodeMotherBoardSerial[0].InnerText;

            //var nodeCpuProcessorId = xmlDocument.GetElementsByTagName("cpuProcessorId");
            //model.CpuProcessorId = nodeCpuProcessorId[0].InnerText;

            //var nodeDiskDriveSerial = xmlDocument.GetElementsByTagName("diskDriveSerial");
            //model.DiskDriveSerial = nodeDiskDriveSerial[0].InnerText;

            return model;
        }

        /// <summary>
        /// Tự động sinh key mã hóa license bất kỳ và trả về chuỗi xml của key
        /// </summary>
        public static string GetRandomLicenseKey
        {
            get
            {
                return new RSAKeyValue().GetXml().InnerXml;
            }
        }

        /// <summary>
        /// Trả về token
        /// </summary>
        /// <param name="licenseCode"></param>
        /// <param name="licenseMail"></param>
        /// <param name="licenseKey"></param>
        /// <returns></returns>
        public static string GetLicenseToken(string licenseCode, string licenseMail, out string licenseKey)
        {
            licenseKey = GetRandomLicenseKey;

            var motherBoardSerial = WmiHelper.GetMotherBoardSerial();
            var cpuProcessorId = WmiHelper.GetCpuProcessorId();
            var diskDriveSerial = WmiHelper.GetDiskDriveSerial();

            var license = new LicenseInfo()
            {
                CpuProcessorId = cpuProcessorId,
                DiskDriveSerial = diskDriveSerial,
                Email = licenseMail,
                MotherBoardSerial = motherBoardSerial,
                LicenseCode = licenseCode
            };

            return CreateLicense(license, licenseKey);
        }

        /// <summary>
        /// Lưu license
        /// </summary>
        /// <param name="path"></param>
        /// <param name="token"></param>
        public static void Save(string path, string token)
        {
            File.WriteAllText(path, token);
        }

        private static string CreateLicense(LicenseInfo license, string key)
        {
            var xmlDoc = new XmlDocument();
            var rootNode = xmlDoc.CreateElement("egovlicense");
            xmlDoc.AppendChild(rootNode);

            var emailNode = xmlDoc.CreateElement("email");
            emailNode.InnerText = license.Email;
            rootNode.AppendChild(emailNode);

            var codeNode = xmlDoc.CreateElement("licenseCode");
            codeNode.InnerText = license.LicenseCode;
            rootNode.AppendChild(codeNode);

            var cpuProcessorIdNode = xmlDoc.CreateElement("cpuProcessorId");
            cpuProcessorIdNode.InnerText = license.CpuProcessorId;
            rootNode.AppendChild(cpuProcessorIdNode);

            var diskDriveSerialNode = xmlDoc.CreateElement("diskDriveSerial");
            diskDriveSerialNode.InnerText = license.DiskDriveSerial;
            rootNode.AppendChild(diskDriveSerialNode);

            var motherBoardSerialNode = xmlDoc.CreateElement("motherBoardSerial");
            motherBoardSerialNode.InnerText = license.MotherBoardSerial;
            rootNode.AppendChild(motherBoardSerialNode);

            var privateKey = CreatePrivateKey(key);

            var signedXml = new SignedXml(xmlDoc) { SigningKey = privateKey };

            // Add the key to the SignedXml document. 

            // Create a reference to be signed.
            var reference = new Reference { Uri = "" };

            // Add an enveloped transformation to the reference.
            var env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            var xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            if (xmlDoc.DocumentElement != null)
            {
                xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
            }
            if (xmlDoc.FirstChild is XmlDeclaration)
            {
                xmlDoc.RemoveChild(xmlDoc.FirstChild);
            }

            var unicode = Encoding.Unicode;
            return Convert.ToBase64String(unicode.GetBytes(xmlDoc.OuterXml));
        }

        private static RSACryptoServiceProvider CreatePrivateKey(string key)
        {
            var keyPrivate = new RSACryptoServiceProvider();
            // keyPrivate.FromXmlString("<RSAKeyValue><Modulus>t+H1DeC1oX6R/Xf9GWgLpfxwJoFwMKBq14IV4vvk1Bf3L0TxXFFQBXpwFHRPiMr1ABCmpD15xZuhUUVnnQx0gt8kL97692NvJajsg5TCT2lJIdp5nWniwFNBbRaye5UH8kbN1ZCJT82XAxEMLEvLis0QysxonupL6BcPoy6/wd8=</Modulus><Exponent>AQAB</Exponent><P>+ka8Czmvtl5DYrrb8YSr+JhrAP2EHE9JJxSQOe4LTEio3v0S6kIB88s9aS42hmN0FHHwwSeDCEKVobWYglAqZQ==</P><Q>vBaD1ASu+TVkvr5TVWz7Yc/LwkYPVKHf1hWicIVd/E5tk4n5CfKj6HycIElq1jm2p6H6HMJtPXCXvx8pRa808w==</Q><DP>4nFdckqfUMG49ntaxQrlDefZ6Ot3vKV/6nwQgll+n2aeZCNWGd3fJlWdGq1VaAJT5KuyyRCW3cJg4A8ODdm6gQ==</DP><DQ>Yzs9sc4GwlP5Iukm7hrhRWlsBsMPs4bzoO8pFFRIkaSPCxfv29+3uiCD/kS9qOgqBabfXez11URhyuOV0r3tIQ==</DQ><InverseQ>Ikac3Kp5WXj7s2363ZRSgxAW3WSGL904fBIVa3IsRgOOIBA+xSeeSQcDdEgangzTbSha7ys3XHv0bMUn+h2PmA==</InverseQ><D>FyEldWLsBc3JaEQVAoKbYHwR0U4bwgoTllsHDL+Zh3IiRgBaib2ynpXJjlEZBgBdc+4kP+l5rTkHwBTGVTNRXkO2Tk7xNpX9CFumzpZhJhqFAR0wz21QWjRahJ9ECbWyxnv7kXWIEr5HMlabASTXxP6uCC7UIXyTt3yfiGWn0l0=</D></RSAKeyValue>");
            keyPrivate.FromXmlString(key);
            return keyPrivate;
        }
    }
}
