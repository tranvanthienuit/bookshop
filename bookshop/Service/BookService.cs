using System.Text;
using bookshop.DbContext;
using bookshop.Entity;
using bookshop.Entity.Model;
using bookshop.Paging;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface BookInter
{
    public Task<bool> saveBook(BookRequest bookRequest);
    public Task<bool> deleteBook(String bookId);
    public Task<bool> editBook(BookRequest bookRequest);
    public Task<Book> findBookById(String bookId);
    public Task<Response<Book>> findBook(String Book,int pageIndex);
}

public class BookService : BookInter
{
    private readonly Dbcontext _dbcontext;

    public BookService(Dbcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<bool> saveBook(BookRequest bookRequest)
    {
        try
        {
            Book book = new Book()
            {
                nameBook = bookRequest.nameBook,
                author = bookRequest.author,
                publish = bookRequest.publish,
                count = bookRequest.count,
                price = bookRequest.price,
                image = Encoding.ASCII.GetBytes(bookRequest.image)
            };
            await _dbcontext.Books.AddAsync(book);
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

    public async Task<Response<Book>> findBook(string Book,int pageIndex)
    {
        try
        {
            if (Book==null)
            {
                Response<Book> book = new Response<Book>()
                {
                    result = PaginatedList<Book>.CreateAsync(_dbcontext.Books.ToList(), pageIndex, 5),
                    totalBook = _dbcontext.Books.ToList().Count
                };
                return book;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}