//
// Copyright © 2020 Terry Moreland
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

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Vulnerable.Domain.Entities;

#nullable disable

namespace Vulnerable.Net48.Infrastructure.Data
{
    public sealed class AddressDbContext : DbContext
    {
        public AddressDbContext(IDbContextOptions options)
            : base(options.ConnectionStringName)
        {
            Database.SetInitializer(new AddressDbInitializer());
        }

        /// <summary>
        /// Private constructor for Entity Framework, ensure
        /// AddressConnectionDebug connection string is available  
        /// in app or web.config
        /// </summary>
        /// <remarks>
        /// will need to make this public when adding migrations
        /// </remarks>
        private AddressDbContext()
            : base("LocalAddressConnection")
        {
            Database.SetInitializer(new AddressDbInitializer());
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent> Continents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureCity(modelBuilder.Entity<City>());
            ConfigureProvince(modelBuilder.Entity<Province>());
            ConfigureCountry(modelBuilder.Entity<Country>());
            ConfigureContinent(modelBuilder.Entity<Continent>());

            static void ConfigureCity(EntityTypeConfiguration<City> entity)
            {
                entity.Property(p => p.Name).IsUnicode(false).HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasOptional(c => c.Province)
                    .WithMany(p => p.Cities);
                entity
                    .HasOptional(c => c.Country);
            }
            static void ConfigureProvince(EntityTypeConfiguration<Province> entity)
            {
                entity.Property(p => p.Name).IsUnicode(false).HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasOptional(p => p.Country)
                    .WithMany(c => c.Provinces);
                entity
                    .HasMany(p => p.Cities)
                    .WithOptional(c => c.Province)
                    .HasForeignKey(c => c.ProvinceId)
                    .WillCascadeOnDelete(true);
            }
            static void ConfigureCountry(EntityTypeConfiguration<Country> entity)
            {
                entity.Property(p => p.Name).IsUnicode(false).HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasMany(c => c.Provinces)
                    .WithOptional(p => p.Country)
                    .HasForeignKey(p => p.CountryId)
                    .WillCascadeOnDelete(true);
                entity
                    .HasOptional(c => c.Continent)
                    .WithMany(c => c.Countries);
            }
            static void ConfigureContinent(EntityTypeConfiguration<Continent> entity)
            {
                entity.Property(p => p.Name).IsUnicode(false).HasMaxLength(100);
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.Name).IsConcurrencyToken();
                entity
                    .HasMany(c => c.Countries)
                    .WithOptional(c => c.Continent)
                    .HasForeignKey(c => c.ContinentId)
                    .WillCascadeOnDelete(true);
            }
        }

    }
}
