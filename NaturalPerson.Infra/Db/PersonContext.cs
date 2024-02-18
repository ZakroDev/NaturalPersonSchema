using Microsoft.EntityFrameworkCore;
using NaturalPerson.Infra.Db.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalPerson.Infra.Db
{
    public class PersonContext(DbContextOptions<PersonContext> options) : DbContext(options)
    {
        public DbSet<Model.Person> Persons { get; set; }
        public DbSet<Model.City> PersonsCountries { get; set; }
        public DbSet<Model.PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Model.Connection> Connections { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureBaseEntity();
        }
    }
}
