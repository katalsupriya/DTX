using System.ComponentModel.DataAnnotations.Schema;

namespace TourismDXP.Core.DataModels
{
    [Table("LookUpDomainValue")]
    public class LookUpDomainValue : BaseEntity
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long Id { get; set; }
        public int DomainId { get; set; }
        public int DomainValueParentId { get; set; }
        // only add first time work as unique key 
        public string DomainValue { get; set; }
        // on change we will keep the change value 
        public string DomainText { get; set; }
        public bool IsActive { get; set; }


        #region foreign 
        [ForeignKey("DomainId")]
        public virtual LookUpDomain LookUpDomain { get; set; }       
        #endregion

    }
}
