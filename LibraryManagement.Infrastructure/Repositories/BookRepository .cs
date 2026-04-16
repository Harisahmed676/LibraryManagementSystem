using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksByLibraryAsync(int libraryId)
        {
            return await _context.Books
                .Where(x => x.LibraryId == libraryId && x.IsActive)
                .Include(x => x.Library)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books
                .Where(x => x.AvailableCopies > 0 && x.IsActive)
                .Include(x => x.Library)
                .ToListAsync();
        }

        public async Task<Book?> GetBookByISBNAsync(string isbn)
        {
            return await _context.Books
                .Include(x => x.Library)
                .FirstOrDefaultAsync(x => x.ISBN == isbn);
        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(x => x.Library)
                .ToListAsync();
        }

        public override async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(x => x.Library)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
