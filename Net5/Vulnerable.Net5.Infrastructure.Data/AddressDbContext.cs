//
// Copyright © 2021 Terry Moreland
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vulnerable.Domain.Entities;

// ReSharper disable UnusedAutoPropertyAccessor.Global
#nullable disable

namespace Vulnerable.Net5.Infrastructure.Data
{
    public class AddressDbContext : DbContext
    {
        public AddressDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent> Continents { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ConfigureCity(modelBuilder.Entity<City>());
            ConfigureProvince(modelBuilder.Entity<Province>());
            ConfigureCountry(modelBuilder.Entity<Country>());
            ConfigureContinent(modelBuilder.Entity<Continent>());

            base.OnModelCreating(modelBuilder);

            static void ConfigureCity(EntityTypeBuilder<City> entity)
            {
                entity.Property(e => e.Name).IsUnicode().HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasOne(c => c.Province)
                    .WithMany(p => p.Cities);
                entity
                    .HasOne(c => c.Country);
            }

            static void ConfigureProvince(EntityTypeBuilder<Province> entity)
            {
                entity.Property(e => e.Name).IsUnicode().HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasOne(p => p.Country)
                    .WithMany(c => c.Provinces);
                entity
                    .HasMany<City>()
                    .WithOne(c => c.Province)
                    .HasForeignKey(c => c.ProvinceId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
            static void ConfigureCountry(EntityTypeBuilder<Country> entity)
            {
                entity.Property(e => e.Name).IsUnicode().HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasMany<Province>()
                    .WithOne(p => p.Country)
                    .HasForeignKey(p => p.CountryId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(c => c.Continent)
                    .WithMany(c => c.Countries);
            }

            static void ConfigureContinent(EntityTypeBuilder<Continent> entity)
            {
                entity.Property(e => e.Name).IsUnicode().HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasMany<Country>()
                    .WithOne(c => c.Continent)
                    .HasForeignKey(c => c.ContinentId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

        }

    }
}
