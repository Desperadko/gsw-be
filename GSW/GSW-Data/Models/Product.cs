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
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(ProductConstants.NAME_MAX_LENGTH)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(ProductConstants.DESCRIPTION_MAX_LENGTH)]
        public required string Description { get; set; }

        [Required(ErrorMessage = "ReleaseDate is required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        public virtual ICollection<Genre>? Genres { get; set; }
        public virtual ICollection<Developer>? Developers { get; set; }
        public virtual ICollection<Publisher>? Publishers { get; set; }
        public virtual ICollection<Platform>? Platforms { get; set; }
    }
}
