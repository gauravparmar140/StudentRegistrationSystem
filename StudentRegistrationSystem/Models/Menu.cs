using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationSystem.Models
{
    [Table("T_Menus")]
    public class Menu
    {
        public int Id { get; set; }

        public string MenuName { get; set; }

        public string MenuDescription { get; set; }

        public string MenuAction { get; set; }

        public string MenuController { get; set; }

        
       // [ForeignKey("Role")]
        public int RoleId { get; set; }

        public bool Hidden { get; set; }
    }
}
