using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBorrowRecordRepository : IRepository<BorrowRecord>
    {
        Task<IEnumerable<BorrowRecord>> GetBorrowsByMemberAsync(int memberId);
        Task<IEnumerable<BorrowRecord>> GetOverdueBorrowsAsync();
        Task<IEnumerable<BorrowRecord>> GetActiveBorrowsAsync();
    }
}
