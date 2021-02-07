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
using Moreland.VulnerableSoap.Data.Model;

#nullable disable

namespace Moreland.VulnerableSoap.Data
{
    public class AddressContext : DbContext
    {
        public AddressContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country> Countries { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ConfigureCountry(modelBuilder.Entity<Country>());
            ConfigureProvince(modelBuilder.Entity<Province>());

            base.OnModelCreating(modelBuilder);

            static void ConfigureCountry(EntityTypeBuilder<Country> entity)
            {
                entity
                    .HasMany<Province>()
                    .WithOne(p => p.Country)
                    .HasForeignKey(p => p.CountryId);
            }

            static void ConfigureProvince(EntityTypeBuilder<Province> entity)
            {
                entity.HasOne<Country>()
                    .WithMany(c => c.Provinces)
                    .HasPrincipalKey(c => c.Id);
            }
        }
    }
}
