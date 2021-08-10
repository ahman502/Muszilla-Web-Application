using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Muszilla.Models
{
    public class ConsumerModel //Model for the user tables
    {
        public string User_ID { get; set; }
        public string USERNAME { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email{ get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="The {0} must at least be {2} characters long.", MinimumLength = 6)]
        public string Pass_word{ get; set; }
        public string CreatedDate { get; set; }
        public string Old_Pass_word { get; set; }
        [NotMapped]
        [DisplayName("UploadFile")]
        public IFormFile Picture { get; set; }
       
    }
    public class Register
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must at least be {2} characters long.", MinimumLength = 6)]
        public string Pass_word { get; set; }
    }
}
