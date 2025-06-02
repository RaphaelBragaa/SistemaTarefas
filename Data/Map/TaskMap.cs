using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaTarefa.Models;

namespace SistemaTarefa.Data.Map
{
    public class TaskMap : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(t => t.User)
               .WithMany(u => u.Tasks) // se existir a coleção em UserModel
               .HasForeignKey(t => t.UserId);

        }
    }
}

