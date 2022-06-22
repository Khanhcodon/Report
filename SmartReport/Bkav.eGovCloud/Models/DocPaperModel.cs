namespace Bkav.eGovCloud.Models
{
    public class DocPaperModel
    {
        /// <summary>
        /// Auto increment key.
        /// </summary>
        public int DocPaperId { get; set; }

        /// <summary>
        /// Get or set the document id.
        /// </summary>
        public System.Guid DocumentId { get; set; }

        /// <summary>
        /// Get or set the PaperName.
        /// </summary>
        public string PaperName { get; set; }

        /// <summary>
        /// Get or set the amount of Paper.
        /// </summary>
        public int Amount { get; set; }

        public bool IsRequired { get; set; }

        /// <summary>
        /// Get or set the type of Paper.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SupplementaryId { get; set; }

        public DocPaperModel()
        {
            IsRequired = true;
        }
    }
}