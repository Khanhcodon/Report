namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    ///
    /// </summary>
    public class Notify
    {
        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Option { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual Template Template { get; set; }
    }
}