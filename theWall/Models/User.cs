using System;
using System.ComponentModel.DataAnnotations;
 
namespace theWall.Models
{
    public abstract class BaseEntity{}
    public class RegisterUser : BaseEntity
    {
        [Required]
        [MinLength(2, ErrorMessage="Please enter two or more characters for First Name!")]
        public string firstname { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="Please enter two or more characters for Last Name!")]
        public string lastname { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage="Password must contain at least 8 characters!")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [CompareAttribute("password", ErrorMessage="Password and Confirm Password must match!")]
        public string confirmPassword {get;set;}
        
    }
    public class LoginUser : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        
    }

    
 

}
