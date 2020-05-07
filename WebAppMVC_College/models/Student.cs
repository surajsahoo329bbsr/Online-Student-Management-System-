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

        [Display(Name = "Photo")]
        public byte[] StudentPhoto { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }

        [NotMapped, Display(Name = "Upload File"), Required(ErrorMessage ="Please Upload Photo"), FileExtensions("jpg,jpeg,png", ErrorMessage = "Please upload image of jpg, jpeg or png format"), MaxFileSize(1 * 1024 * 1024, ErrorMessage = "Max File Size Is 1 MB")]
        public HttpPostedFileBase ImageFile { get; set; }
        public virtual Admin Admin { get; set; }
    }

    public enum GenderType
    {
        Male,
        Female,
        Others
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileExtensionsAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public FileExtensionsAttribute(string fileExtensions)
        {
            AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {

            if (value is HttpPostedFileBase file)
            {
                var fileName = file.FileName;

                return AllowedExtensions.Any(y => fileName.EndsWith(y));
            }

            return true;
        }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            if (!(value is HttpPostedFileBase file))
            {
                return false;
            }
            return file.ContentLength <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString());
        }
    }
}