using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentRegistrationSystem.Models
{
    [Table("T_CourceRegistration")]
    public class CourceRegistration
    {
        public int Id { get; set; }
        public int CourceId { get; set; }
        public int StudentId { get; set; }
    }
}