using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Pagamentos.Data.Entities;

namespace NerdStore.Pagamentos.Data.Mappings
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
