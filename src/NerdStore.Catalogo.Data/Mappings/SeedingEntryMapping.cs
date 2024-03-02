using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Data.Entities;

namespace NerdStore.Catalogo.Data.Mappings
{
    public class SeedingEntryMapping : IEntityTypeConfiguration<SeedingEntry>
    {
        public void Configure(EntityTypeBuilder<SeedingEntry> builder)
        {
            builder.ToTable("__SeedingHistory");
            builder.HasKey(s => s.Name);

        }
    }
}
