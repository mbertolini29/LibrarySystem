using LibrarySystem.Models;
using LibrarySystem.Repositories.UnitOfWorkPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddBook(Book book)
        {
            //este resultado es el libro.
            await _unitOfWork.GenericRepository<Book>().AddAsync(book);
            _unitOfWork.Save();
        }

        public async Task DeleteBook(int id)
        {
            var book = _unitOfWork.GenericRepository<Book>().GetByIdAsync(filter: x => x.Id == id);
            _unitOfWork.GenericRepository<Book>().Delete(book);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _unitOfWork.GenericRepository<Book>().GetAll();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _unitOfWork.GenericRepository<Book>().GetByIdAsync(filter:x=>x.Id==id);
        }

        public async Task UpdateBook(Book book)
        {
            var BookFromDb = await _unitOfWork.GenericRepository<Book>().GetByIdAsync(filter: x => x.Id == book.Id);

            if(BookFromDb != null)
            {
                BookFromDb.Publisher = book.Publisher;
                BookFromDb.Title = book.Title;
                BookFromDb.Author = book.Author;
                BookFromDb.ISBN = book.ISBN;
                BookFromDb.PublicationDate = DateTime.UtcNow;
                BookFromDb.CategoryId = book.CategoryId;
                BookFromDb.TotalCopies = book.TotalCopies;
            }
        }
    }
}
