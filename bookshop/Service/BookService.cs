using bookshop.Entity;

namespace bookshop.Service;

public interface BookInter
{
    public Task<bool> saveBook(Book Book);
    public Task<bool> deleteBook(String bookId);
    public Task<bool> editBook(Book Book);
    public Task<Book> findBookById(String bookId);
    public Task<List<Book>> findBook(String Book);
}
public class BookService: BookInter
{
    public Task<bool> saveBook(Book Book)
    {
        throw new NotImplementedException();
    }

    public Task<bool> deleteBook(string bookId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> editBook(Book Book)
    {
        throw new NotImplementedException();
    }

    public Task<Book> findBookById(string bookId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book>> findBook(string Book)
    {
        throw new NotImplementedException();
    }
}