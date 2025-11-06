using GSW_Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Data.ModelConfigurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasMany(p => p.Genres)
                .WithMany(g => g.Products);

            builder
                .HasMany(p => p.Platforms)
                .WithMany(p => p.Products);

            builder
                .HasMany(p => p.Developers)
                .WithMany(d => d.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "DeveloperProduct",
                    j => j.HasOne<Developer>().WithMany().HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            builder
                .HasMany(p => p.Publishers)
                .WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "PublisherProduct",
                    j => j.HasOne<Publisher>().WithMany().HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
