using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TourismDXP.Core.Enums;

namespace TourismDXP.Core.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter role name")]
        [Remote("IsRoleDuplicate", "Role", HttpMethod = "POST", ErrorMessage = "Role Name already exists in database.", AdditionalFields = "Id")]
        public string RoleName { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsSeeded { get; set; }
        public bool IsReadMode { get; set; }

        //from which Role it's been derived 
        [Required(ErrorMessage = "Please select portal")]
        public int? ParentId { get; set; }
        //ReadOnly/Read Write


        [Required(ErrorMessage = "Please select read/write access")]
        public int? AccessLevel { get; set; }
        public int? OwnerId { get; set; }
        public int? CategoryId { get; set; }

        //Mail Role List 
        public SelectList MainRoleList { get; set; }

        public AccessLevel AccessLevelType { get; set; }

    }
}
