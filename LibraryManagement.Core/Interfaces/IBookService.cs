using LibraryManagement.Core.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto?> GetBookByISBNAsync(string isbn);
        Task<IEnumerable<BookDto>> GetBooksByLibraryAsync(int libraryId);
        Task<IEnumerable<BookDto>> GetAvailableBooksAsync();
        Task<BookDto> CreateBookAsync(CreateBookDto dto);
        Task UpdateBookAsync(int id, UpdateBookDto dto);
        Task DeleteBookAsync(int id);
    }
}
