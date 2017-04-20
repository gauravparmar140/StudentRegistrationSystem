namespace StudentRegistrationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public partial class T_Student
    {
        public int Id { get; set; }

        public string Password { get; set; }
       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string UserName { get; set; }
        public int? RoleId { get; set; }
        
        public bool IsAdmin { get; set; } 

        public bool IsTeacher { get; set; }

        //public Genders GenderList{ get; set; }
    }

    public enum Genders
    {
        Male = 0,
        Female = 1,
        Other = 2,
    }
}
