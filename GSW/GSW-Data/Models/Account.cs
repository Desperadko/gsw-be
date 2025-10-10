using GSW_Data.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Data.Models
{
    [PrimaryKey(nameof(Id))]
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is reqiured")]
        [MaxLength(AccountConstants.USERNAME_MAX_LENGTH)]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(AccountConstants.EMAIL_MAX_LENGTH)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(AccountConstants.PASSWORD_MAX_LENGTH)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "No role set")]
        [MaxLength(AccountConstants.ROLE_MAX_LENGTH)]
        public required string Role { get; set; } = "User";

        [NotMapped]
        public bool IsVaild => !string.IsNullOrEmpty(Password);
    }
}
