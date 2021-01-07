using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TourismDXP.Core.Models
{
    public class UserModel : BaseModel
    {

        // public string Name { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        [Remote("CheckUserEmail", "User", HttpMethod = "POST", ErrorMessage = "Email Id already exists in database.", AdditionalFields = "Id")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter company name")]
        public string CompanyName { get; set; }
        //[Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                  ErrorMessage = "Please enter a valid 10 digit phone number.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please select role")]
        public int? RoleId { get; set; }

        [Required(ErrorMessage = "Please select Reseller/Client")]
        public int? MainRoleId { get; set; }
        //User For Min Role Name Like Reseller/Client
        public string Role { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Username { get; set; }
        public string Code { get; set; }
        public SelectList RoleList { get; set; }
        //Mail Role List 
        public List<SelectListItem> MainRoleList { get; set; }
        public bool IsCodeUsed { get; set; }
        public bool IsReadMode { get; set; }
        public bool IsUpdated { get; set; }
        public int ResellerId { get; set; }
        public int UserTypeId { get; set; }
        public int? OwnerId { get; set; }
        public string RoleName { get; set; }

        public SelectList UserTypeList { get; set; }
        public SelectList ResellerList { get; set; }

    }
}
