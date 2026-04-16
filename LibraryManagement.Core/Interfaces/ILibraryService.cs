using LibraryManagement.Core.DTOs.Book;
using LibraryManagement.Core.DTOs.Library;
using LibraryManagement.Core.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryDto>> GetAllLibrariesAsync();
        Task<LibraryDto?> GetLibraryByIdAsync(int id);
        Task<LibraryDto?> GetLibraryWithBooksAsync(int id);
        Task<LibraryDto> CreateLibraryAsync(CreateLibraryDto dto);
        Task UpdateLibraryAsync(int id, UpdateLibraryDto dto);
        Task DeleteLibraryAsync(int id);
    }
}
