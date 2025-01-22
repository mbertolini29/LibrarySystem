using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public interface IBookService
    {
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
        Task<Book> GetBook(int id);
        Task<IEnumerable<Book>> GetAllBooks();
    }
}
