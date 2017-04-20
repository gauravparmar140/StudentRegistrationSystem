using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationSystem.Models
{
    
    [Table("T_Users")]
    public class User
    {
    public int Id { get; set;}

    public string UserName{ get; set; }

     public string Password { get; set; }

          public int RoleId { get; set; }

        public Role Role { get; set; }

        public string  UserDescription { get; set; }


    }
}
