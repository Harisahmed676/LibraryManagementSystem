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
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<Library?> GetLibraryWithBooksAsync(int libraryId)
        {
            return await _context.Libraries
                .Include(x => x.Books.Where(b => b.IsActive))
                .FirstOrDefaultAsync(x => x.Id == libraryId);
        }
    }
}
