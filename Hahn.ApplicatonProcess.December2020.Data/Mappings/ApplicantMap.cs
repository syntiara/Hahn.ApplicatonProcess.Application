using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.ApplicatonProcess.December2020.Data.Mappings
{
    internal abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            // define base configurations
            builder.HasKey(t => t.Id);
        }
    }

    internal class MaintainerConfiguration : BaseEntityTypeConfiguration<Applicant>
    {
        public override void Configure(EntityTypeBuilder<Applicant> builder)
        {
            // inherit base configurations
            base.Configure(builder);
            builder.Property(t => t.Name).HasMaxLength(50);
            builder.Property(t => t.FamilyName).HasMaxLength(50);
            builder.Property(t => t.Address).HasMaxLength(50);
            builder.Property(t => t.EmailAddress).HasMaxLength(50);
        }
}
}