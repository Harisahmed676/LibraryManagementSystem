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
    public class BorrowRecordRepository : BaseRepository<BorrowRecord>, IBorrowRecordRepository
    {
        public BorrowRecordRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BorrowRecord>> GetBorrowsByMemberAsync(int memberId)
        {
            return await _context.BorrowRecords
                .Where(x => x.MemberId == memberId)
                .Include(x => x.Book)
                .Include(x => x.Member)
                .OrderByDescending(x => x.BorrowDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowRecord>> GetOverdueBorrowsAsync()
        {
            return await _context.BorrowRecords
                .Where(x => !x.IsReturned && x.DueDate < DateTime.UtcNow)
                .Include(x => x.Book)
                .Include(x => x.Member)
                .OrderBy(x => x.DueDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowRecord>> GetActiveBorrowsAsync()
        {
            return await _context.BorrowRecords
                .Where(x => !x.IsReturned)
                .Include(x => x.Book)
                .Include(x => x.Member)
                .OrderByDescending(x => x.BorrowDate)
                .ToListAsync();
        }
    }
}
