using CarTroubleSolver.Shared.Models.ExtraModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.SentAt)
                .IsRequired();

            builder.HasOne(m => m.SenderUser)  
                .WithMany(u => u.SentMessages) 
                .HasForeignKey(m => m.SenderUserId) 
                .OnDelete(DeleteBehavior.Restrict);  

            builder.HasOne(m => m.ReceiverUser)  
                .WithMany(u => u.ReceivedMessages)  
                .HasForeignKey(m => m.ReceiverUserId)  
                .OnDelete(DeleteBehavior.Restrict); 

            
            builder.HasOne(m => m.SenderWorkshop)  
                .WithMany(w => w.SentMessages) 
                .HasForeignKey(m => m.SenderWorkshopId) 
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(m => m.ReceiverWorkshop)  
                .WithMany(w => w.ReceivedMessages) 
                .HasForeignKey(m => m.ReceiverWorkshopId) 
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
