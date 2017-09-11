using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class User
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        public string HashedPassword { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = Constants.Errors.InvaidFirstName, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z ,.'-]*[a-zA-Z]$")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = Constants.Errors.InvaidLastName, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z ,.'-]*[a-zA-Z]$")]
        public string LastName { get; set; }

        [StringLength(200, ErrorMessage = Constants.Errors.InvaidLastName)]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z ,.'-]*[a-zA-Z]$")]
        public string MiddleName { get; set; }

        [RegularExpression(@"^[0-9]{9}$")]
        public string SSN { get; set; }

        public DateTime DateOfBirth { get; set; }

        public UserRoleEnum UserRole { get; set; }
    }
}
