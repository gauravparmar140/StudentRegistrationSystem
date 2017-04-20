using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationSystem.Models
{
    [Table("T_Cource")]
    public class Cource
    {

        public int Id { get; set; }

        public string CourceName { get; set; }

        public string CourceDescription { get; set; }

        public int? RoleId { get; set; }
        
    }
}
