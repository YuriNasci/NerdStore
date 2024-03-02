using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Vendas.Data.Entities;

namespace NerdStore.Vendas.Data.Mappings
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
