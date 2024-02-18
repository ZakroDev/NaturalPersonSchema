using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NaturalPerson.Infra.Db.Config.Tables
{
    class PersonConfig : IEntityTypeConfiguration<Model.Person>
    {
        public void Configure(EntityTypeBuilder<Model.Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(t => t.Id);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.PersonalId).IsRequired().HasMaxLength(11);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        }
    }
}
