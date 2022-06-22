using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Api.Dto
{
    public class RelationDocumentDto
    {
        public Guid DocumentId { get; set; }
        public int DocumentCopyId { get; set; }
        public string Compendium { get; set; }
        public string DocCode { get; set; }
    }
}
