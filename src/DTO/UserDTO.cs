using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using src.Entity;
using static src.DTO.AddressDTO;

namespace src.DTO
{
    public class UserDTO
    {
        /// <summary>
        /// Data Transfer Object (DTO)
        ///This DTO is designed to encapsulate all necessary information.
        /// transfer data between different parts of an application,
        /// such as between services, and repositories.
        /// Mapper used the class down there.
        /// Contains user details such as name, phone number, email, password, addresses, salt for password hashing,and admin status.
        /// add two scenarios:
        /// 1- user login, only the email and password fields may be required.
        /// 2- user forgot password, only the password fields may be required.
        /// </summary>

        public class UserCreateDto //can use for signUp
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(8)]
            [PasswordComplexityAttribute]
            [RegularExpression(
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
                ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit."
            )]
            public string Password { get; set; }
        }

        public class UserReadDto //for Admin
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public Role Role { get; set; }
            public byte[]? Salt { get; set; }
        }

        public class AdminUpdateDto //for Admin
        {
            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public Role Role { get; set; }
        }

        public class UserLoginDto
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(8)]
            [RegularExpression(
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
                ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit."
            )]
            public string Password { get; set; }
        }

        public class UserProfileDto
        {
            public Guid UserId { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(8)]
            [RegularExpression(
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
                ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit."
            )]
            public string Password { get; set; }

            public ICollection<AddressReadDto> Addresses { get; set; } = new List<AddressReadDto>();
        }

        public class PasswordUpdateDto
        {
            [Required]
            [MinLength(8)]
            [RegularExpression(
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
                ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit."
            )]
            public string Password { get; set; }
        }
    }
}
