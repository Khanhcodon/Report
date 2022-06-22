using Bkav.eGovCloud.Areas.Admin.Validator;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class DocTypeFormModel: PacketModel
    {
        //DocTypeModel,FormModel, TimeJobModel
        public DocTypeModel DocType { get; set; }
        public FormModel Form { get; set; }
        public DocTypeTimeJobModel TimeJob { get; set; }
    }
}