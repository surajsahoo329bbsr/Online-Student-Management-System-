using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppMVC_College.models
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "First Name Has Only Alphabets"), MinLength(1), MaxLength(20), RegularExpression("^[a-zA-Z]{1,20}$"), Display(Name = "First Name")]
        public string AdminFirstName { get; set; }

        [Required(ErrorMessage = "Last Name Has Only Alphabets"), MinLength(1), MaxLength(20), RegularExpression("^[a-zA-Z]{1,20}$"), Display(Name = "Last Name")]
        public string AdminLastName { get; set; }

        [Required(ErrorMessage = "Please Enter A Valid Email"), MaxLength(60), RegularExpression(@"^([a-zA-Z0-9_\-\.]+)\u0040([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"), Display(Name = "Email")]
        public string AdminEmail { get; set; }

        [Required(ErrorMessage = "Number Must Be 10 Digit Starting With 7/8/9"), MinLength(10), MaxLength(10), RegularExpression("^[789]{1}[0-9]{9}$"), Display(Name = "Phone Number")]
        public string AdminPhoneNo { get; set; }

        [Required(ErrorMessage = "Password Must Be >=8 and <= 10 Characters"), MinLength(8), MaxLength(20), RegularExpression("^[a-zA-Z0-9]{8,20}$"), Display(Name = "Password")]
        public string AdminPassword { get; set; }

        [NotMapped, Required(ErrorMessage = "Password Must Be >=8 and <= 10 Characters"), MinLength(8), MaxLength(20), RegularExpression("^[a-zA-Z0-9]{8,20}$"), Display(Name = "Confirm Password")]
        public string AdminConfirmPassword { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}