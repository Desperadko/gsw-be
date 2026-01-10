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
    public class Developer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(DeveloperConstants.NAME_MAX_LENGTH)]
        public required string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
