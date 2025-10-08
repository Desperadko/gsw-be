using GSW_Data.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [MaxLength(AccountConstants.USERNAME_MAX_LENGTH)]
        public required string Username { get; set; }

        [MaxLength(AccountConstants.EMAIL_MAX_LENGTH)]
        public required string Email { get; set; }

        [MaxLength(AccountConstants.PASSWORD_MAX_LENGTH)]
        public required string Password { get; set; }
    }
}
