using BookCentreProject.Migrations;
using BookCentreProject.Models;

namespace BookCentreProject.RepositoryPattern.Interface
{
    public interface IBookLibrary
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> SearchBook(string searchString);
        Task<int>AddBook(Book book);
        Task<Book> GetBookById(int id);
        Task <bool> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);
        
    }
}
