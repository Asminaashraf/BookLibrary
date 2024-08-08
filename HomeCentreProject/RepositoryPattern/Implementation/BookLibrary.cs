using BookCentreProject.Data;
using BookCentreProject.Migrations;
using BookCentreProject.Models;
using BookCentreProject.RepositoryPattern.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookCentreProject.RepositoryPattern.Implementation
{
    public class BookLibrary : IBookLibrary
    {
        private readonly UserDbContext context;

        public BookLibrary(UserDbContext context) 
        {
            this.context = context;
        }
        public async Task < IEnumerable<Book>> GetBooks()
        {
                    
            
           var data=await context.Books.ToListAsync();
           
            return data;
           
            
        }
        public async Task<int> AddBook(Book book)
        {
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return book.BookId;
        }

        public async Task<Book> GetBookById(int id)
        {
           
            return await context.Books.FindAsync(id);
        }
        
        public async Task<bool> UpdateBook(Book book)
        {
            bool status = false;
            if(book != null)
            {
                 context.Books.Update(book);
                await context.SaveChangesAsync();
                
                status = true;

            }
            return status;
           
        }

        public async Task<bool> DeleteBook(int id)
        {
            bool status= false;
            if(id!=0)
            {
                var data = await context.Books.FindAsync(id);
                if(data!=null)
                {
                    context.Books.Remove(data);
                    await context.SaveChangesAsync();
                    status = true;
                }
            }
            return status;

        }

        public async Task<IEnumerable<Book>> SearchBook(string searchString)
        {
            return await context.Books
            .Where(b => b.Name.Contains(searchString))
            .ToListAsync();
        }
    }
}
