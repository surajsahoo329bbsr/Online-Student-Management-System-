using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebAppMVC_College.models
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please Provide Valid Name"), MinLength(1), MaxLength(50), RegularExpression("[a-zA-Z][a-zA-Z ]*"), Display(Name = "Student's Name")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Number Must Be 10 Digit Starting With 7/8/9"), MinLength(10), MaxLength(10), RegularExpression("^[789]{1}[0-9]{9}$"), Display(Name = "Phone Number")]
        public string StudentPhoneNo { get; set; }
        [Required(ErrorMessage = "Please Select Gender"), Display(Name ="Gender")]
        public GenderType StudentGender { get; set; }
        public byte[] Photo { get; set; }
        [ForeignKey("Admin")]
        public int AdminId { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        public virtual Admin Admin { get; set; }
    }

    public enum GenderType
    {
        Male,
        Female,
        Others
    }
}