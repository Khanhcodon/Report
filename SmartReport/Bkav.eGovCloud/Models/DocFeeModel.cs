namespace Bkav.eGovCloud.Models
{
    public class DocFeeModel
    {
        /// <summary>
        /// Auto increment key
        /// </summary>
        public int DocFeeId { get; set; }

        /// <summary>
        /// Get or set the document id.
        /// </summary>
        public System.Guid DocumentId { get; set; }

        /// <summary>
        /// Get or set the FeeName
        /// </summary>
        public string FeeName { get; set; }

        /// <summary>
        /// Get or set the price of Fee
        /// </summary>
        public int Price { get; set; }

        public bool IsRequired { get; set; }

        /// <summary>
        /// Get or set the type of Fee
        /// </summary>
        public int Type { get; set; }

        public int SupplementaryId { get; set; }

        public DocFeeModel()
        {
            IsRequired = true;
        }
    }
}