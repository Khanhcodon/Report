namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// 
    /// </summary>
    public class Relation
    {
        /// <summary>
        /// 
        /// </summary>
        public int RelationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SourceTableId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TargetTableId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TargetColumn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int JoinType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JoinExpression { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JoinOperators { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int JoinId { get; set; }

    }
}
