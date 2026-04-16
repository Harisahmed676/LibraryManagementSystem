using LibraryManagement.Core.DTOs.Book;
using LibraryManagement.Core.DTOs.BorrowRecord;
using LibraryManagement.Core.DTOs.Library;
using LibraryManagement.Core.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBorrowService
    {
        Task<IEnumerable<BorrowRecordDto>> GetAllBorrowsAsync();
        Task<IEnumerable<BorrowRecordDto>> GetBorrowsByMemberAsync(int memberId);
        Task<IEnumerable<BorrowRecordDto>> GetActiveBorrowsAsync();
        Task<IEnumerable<BorrowRecordDto>> GetOverdueBorrowsAsync();
        Task<BorrowRecordDto> BorrowBookAsync(CreateBorrowRecordDto dto);
        Task ReturnBookAsync(ReturnBookDto dto);
    }
}
