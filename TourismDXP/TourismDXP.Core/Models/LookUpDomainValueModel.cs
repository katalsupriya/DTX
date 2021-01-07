using System.ComponentModel.DataAnnotations;
namespace TourismDXP.Core.Models
{
    public class LookUpDomainValueModel : BaseModel
    {
        public int DomainId { get; set; }
        public int DomainValueParentId { get; set; }
        // only add first time work as unique key 
        public string DomainValue { get; set; }
        // on change we will keep the change value 
        // [Remote("IsTemplateCategoryDuplicate", "Template", HttpMethod = "POST", ErrorMessage = "Template name is already exists in database.", AdditionalFields = "Id")]
        [Required(ErrorMessage = "Please enter name")]
        public string DomainText { get; set; }
        public bool IsActive { get; set; }
    }
}
