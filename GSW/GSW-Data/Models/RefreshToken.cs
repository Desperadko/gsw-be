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
    public class RefreshToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "AccountId is required")]
        public required int AccountId { get; set; }

        [Required(ErrorMessage = "Token is required")]
        [MaxLength(RefreshTokenConstants.TOKEN_MAX_LENGTH)]
        public required string Token { get; set; }

        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "ExpiresAt time required")]
        public required DateTime ExpiresAt { get; set; }

        public required bool IsRevoked { get; set; } = false;

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    }
}
