using Microsoft.EntityFrameworkCore;
using NaturalPerson.Infra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalPerson.Infra.Db.Config
{
    public static class BaseTableConfig
    {
        public static void ConfigureBaseEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseTableConfig).Assembly);

            modelBuilder.Entity<City>(e =>
            {
                e.ToTable("City");
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).HasMaxLength(50);
                int id = 1;
                e.HasData(new[]
                {
                    new {CountryName = "Tbilisi"},
                    new {CountryName = "Gori"},
                    new {CountryName = "Kutaisi"},
                    new {CountryName = "Batumi"},
                }.Select(a => new City
                {
                    Id = id++,
                    Name = a.CountryName,
                }));

                e.HasMany(x => x.People)
                    .WithOne(x => x.City)
                    .HasForeignKey(x => x.CityId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PhoneNumber>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Phone).HasMaxLength(50);
                e.ToTable("PhoneNumber");

                e.HasOne(x => x.People)
                .WithMany(x => x.Phone)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Connection>(e =>
            {
                e.ToTable("Connection");
                e.HasKey(x => x.Id);

                e.HasOne(e => e.Person)
                .WithMany(e => e.Connection)
                .HasForeignKey(e => e.PersonId);
                
            });
        }
    }
}

