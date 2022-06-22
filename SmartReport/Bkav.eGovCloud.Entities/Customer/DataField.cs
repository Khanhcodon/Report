namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class DataField
    {
        /// <summary>
        /// 
        /// </summary>
        public DataField()
        {
            IsActivated = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public int DataFieldId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Datatype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DataTableId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FieldType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActivated { get; set; }
    }
}
