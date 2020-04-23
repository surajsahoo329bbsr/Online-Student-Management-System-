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
        [Required, MaxLength(30)]
        public string AdminFirstName { get; set; }
        [Required, MaxLength(30)]
        public string AdminLastName { get; set; }
        [Required, MaxLength(60)]
        public string AdminEmail { get; set; }
        [Required]
        public long AdminPhoneNo { get; set; }
        [Required, MaxLength(20)]
        public string AdminPassword { get; set; }
        [NotMapped, MaxLength(20)]
        public string AdminConfirmPassword { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}