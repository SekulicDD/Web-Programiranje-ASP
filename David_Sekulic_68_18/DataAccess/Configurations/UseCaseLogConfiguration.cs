using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class UseCaseLogConfiguration : IEntityTypeConfiguration<UseCaseLog>
    {
        public void Configure(EntityTypeBuilder<UseCaseLog> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Actor).IsRequired();
            builder.Property(x => x.UserId).IsRequired(false);
            builder.Property(x => x.UseCaseId).IsRequired();
        }
    }
}
