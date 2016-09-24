using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using BrewJournal.Domain;

namespace BrewJournal.EF.Mapping
{
    public class BrewMapping : EntityTypeConfiguration<Brew>
    {
        public BrewMapping()
        {
            ToTable("Brews");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}