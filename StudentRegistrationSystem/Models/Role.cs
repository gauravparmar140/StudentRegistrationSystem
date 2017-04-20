using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationSystem.Models
{
    [Table("T_Roles")]
    public class Role
    {

        public int Id { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

    }
}