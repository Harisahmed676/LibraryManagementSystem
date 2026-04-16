using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ISBN)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.ISBN)
                .IsUnique();

            builder.Property(x => x.Genre)
                .HasMaxLength(100);

            builder.HasMany(x => x.BorrowRecords)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
