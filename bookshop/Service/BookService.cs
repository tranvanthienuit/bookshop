using System.Text;
using bookshop.DbContext;
using bookshop.Entity;
using bookshop.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface BookInter
{
    public Task<bool> saveBook(Book Book);
    public Task<bool> deleteBook(String bookId);
    public Task<bool> editBook(BookRequest bookRequest);
    public Task<Book> findBookById(String bookId);
    public Task<List<Book>> findBook(String Book);
}

public class BookService : BookInter
{
    private readonly Dbcontext _dbcontext;

    public BookService(Dbcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<bool> saveBook(Book Book)
    {
        try
        {
            await _dbcontext.Books.AddAsync(Book);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> deleteBook(string bookId)
    {
        try
        {
            Book book = await _dbcontext.Books.FindAsync(bookId) ?? throw new InvalidOperationException();
            _dbcontext.Books.Remove(book);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> editBook(BookRequest bookRequest)
    {
        try
        {
            Book book = await _dbcontext.Books.FindAsync(bookRequest.bookId) ?? throw new InvalidOperationException();
            if (book != null)
            {
                book.nameBook = bookRequest.nameBook;
                book.author = bookRequest.author;
                book.publish = bookRequest.publish;
                book.count = bookRequest.count;
                book.price = bookRequest.price;
                book.image = Encoding.ASCII.GetBytes(bookRequest.image);
            }

            _dbcontext.Books.Update(book);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<Book> findBookById(string bookId)
    {
        try
        {
            return await _dbcontext.Books.FindAsync(bookId) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<List<Book>> findBook(string Book)
    {
        try
        {
            return await _dbcontext.Books.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}