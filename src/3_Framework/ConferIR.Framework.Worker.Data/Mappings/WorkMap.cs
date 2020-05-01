
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

using Praticis.Framework.Worker.Abstractions;

namespace Praticis.Framework.Worker.Data.Mappings
{
    public class WorkMap : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Works", TableScheme.Default);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("WorkId");

            builder.Property(t => t.Name)
                .IsRequired();

            builder.Property(t => t.CreationDate)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Ignore(t => t.Data);
            
            builder.Property(t => t.Data)
                .HasColumnName("Request")
                .IsRequired();
            
            builder.Property(t => t.RequestType)
                .HasConversion(
                    input => JsonConvert.SerializeObject(input),
                    output => JsonConvert.DeserializeObject<Type>(output)
                );


            // Ignored Properties

            builder.Ignore(t => t.Request);

            builder.Ignore(t => t.Notifications);
        }
    }
}