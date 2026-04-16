using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByLibraryAsync(int libraryId);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<Book?> GetBookByISBNAsync(string isbn);
    }
}
