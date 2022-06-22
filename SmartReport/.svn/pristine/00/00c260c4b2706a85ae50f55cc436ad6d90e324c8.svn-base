using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bkav.eGovCloud.Core.ReadFile;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Business;
using Bkav.eGovCloud.Entities;

namespace Bkav.eGovCloud.Controllers
{
    public class VoiceToTextController : CustomerBaseController
    {
		private readonly Guid id = Guid.Parse("4c8e871e-d802-4b77-b7dc-1310e76401a2");
        private readonly VoiceTextBll _voiceTextService;
        public VoiceToTextController(VoiceTextBll voiceTextService)
        {
            _voiceTextService = voiceTextService;
        }

        public ActionResult Index()
        {
            var voices = _voiceTextService.Gets();
            return View(voices);


        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase importFile)
        {
            var docxUtil = new DocxParser();
            var text = docxUtil.ExtractText(importFile.InputStream);
            var filePath = "";
            SaveVoice(text, out filePath);

            var voiceText = new VoiceText();
            voiceText.Name = importFile.FileName;
            voiceText.Extentions = ".docx";
            voiceText.DateCreated = DateTime.Now;
            voiceText.Path = filePath.Replace("~", "");
            _voiceTextService.Create(voiceText);

            return Redirect("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult ReadBCTH(string content, string name)
        {
            var docxUtil = new DocxParser();
            var filePath = "";
            
            var voice = _voiceTextService.Get(name);
            if (voice != null && voice.Any())
            {
                filePath = voice.FirstOrDefault().Path;
            }
            else
            {
                var text = content.StripHtml();
                SaveVoice(text, out filePath);

                var voiceText = new VoiceText();
                voiceText.Name = name;
                voiceText.Extentions = ".docx";
                voiceText.DateCreated = DateTime.Now;
                voiceText.Path = filePath.Replace("~", "");
                _voiceTextService.Create(voiceText);
            }

            return Json(new { name = name , path = filePath });
        }

        //public string GetTextFile(Stream file, string extension)
        //{
        //    switch (extension)
        //    {
        //        case "pdf":
        //            var pdfUtil = new PdfParser();
        //            var text = pdfUtil.ExtractText(file);
        //            return text;
        //        case "doc":
        //            var docUtil = new DocParser(file);
        //            break;
        //        case "docx":
        //            var docxUtil = new DocxParser();
        //            var text = docxUtil.ExtractText(file);
        //            return text;

        //        default:
        //            break;
        //    }
        //    return "";
        //}

        public void SaveVoice(string text, out string file)
        {
            string voice = "phamtienquan";
            
            JsonRequestCreateAudio oJsonRequestCreateAudio = new JsonRequestCreateAudio();
            oJsonRequestCreateAudio.text = text;
            oJsonRequestCreateAudio.voice = voice;
            oJsonRequestCreateAudio.id = "2";
            oJsonRequestCreateAudio.without_filter = false;
            oJsonRequestCreateAudio.speed = "1.0";
            oJsonRequestCreateAudio.tts_return_option = "2";
            oJsonRequestCreateAudio.timeout = "60000";
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(oJsonRequestCreateAudio);
            try
            {
                string uriString = "https://vtcc.ai/voice/api/tts/v1/rest/syn";
                WebClient myWebClient = new WebClient();
                string postData = data;
                myWebClient.Headers.Add("Content-Type", "application/json;charset=UTF-8");
                myWebClient.Headers.Add("token", "wjhyCkN-XdRuO0qCPlP9duKI1HF2eQGGbkSUB11UEW6TglwrY7Bd93BfOZTOIVKw");
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                byte[] responseArray = myWebClient.UploadData(uriString, "POST", byteArray);

                Response.Clear();
                file = "~/Audio/" + Guid.NewGuid() + ".wav";

                var fileName = CommonHelper.MapPath(file);
                System.IO.File.WriteAllBytes(fileName, responseArray.ToArray());
            }
            catch (Exception ex)
            {
                file = "";
            }
        }
    }

    public class JsonRequestCreateAudio
    {
        public string text { get; set; }
        public string voice { get; set; }
        public string id { get; set; }
        public bool without_filter { get; set; }
        public string speed { get; set; }
        public string tts_return_option { get; set; }
        public string timeout { get; set; }
    }
}