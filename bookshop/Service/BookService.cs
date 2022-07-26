﻿using System.Text;
using bookshop.DbContext;
using bookshop.Entity;
using bookshop.Entity.Model;
using bookshop.Paging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface BookInter
{
    public Task<bool> saveBook(BookRequest bookRequest);
    public Task<bool> deleteBook(String bookId);
    public Task<bool> editBook(BookRequest bookRequest);
    public Task<Book> findBookById(String bookId);
    public Task<Response<Book>> findBookAdmin(string? Book, int pageIndex);
    public Task<Response<Book>> findBookUser(string? Book, int pageIndex);
    public Task<List<String>> findNameAllBook(String? Book);
    public Task<Response<Book>> findBookByCateId(String? categoryId);

}

public class BookService : BookInter
{
    private readonly Dbcontext _dbcontext;
    private readonly UserManager<User> _userManager;

    public BookService(Dbcontext dbcontext, UserManager<User> userManager)
    {
        _dbcontext = dbcontext;
        _userManager = userManager;
    }

    public async Task<bool> saveBook(BookRequest bookRequest)
    {
        try
        {
            Book book = new Book()
            {
                nameBook = bookRequest.nameBook,
                author = bookRequest.author,
                publishYear = bookRequest.publishYear,
                publishCom = bookRequest.publishCom,
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
            await _dbcontext.SaveChangesAsync();
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
                book.publishYear = bookRequest.publishYear;
                book.publishCom = bookRequest.publishCom;
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

    public async Task<Response<Book>> findBookAdmin(string? bookRequest, int pageIndex)
    {
        try
        {
            if (bookRequest == null)
            {
                var books = await _dbcontext.Books.Where(x =>
                    x.nameBook.Contains(bookRequest) || x.author.Contains(bookRequest) ||
                    x.Category.categoryName.Contains(bookRequest)).ToListAsync();
                Response<Book> book = new Response<Book>()
                {
                    result = PaginatedList<Book>.CreateAsync(books, pageIndex, 5),
                    totalBook = _dbcontext.Books.ToList().Count
                };
                return book;
            }
            else
            {
                var books = await _dbcontext.Books.Where(x =>
                    x.nameBook.Contains(bookRequest) || x.author.Contains(bookRequest) ||
                    x.Category.categoryName.Contains(bookRequest)).ToListAsync();
                Response<Book> book = new Response<Book>()
                {
                    result = PaginatedList<Book>.CreateAsync(books, pageIndex, 5),
                    totalBook = _dbcontext.Books.ToList().Count
                };
                return book;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Book>> findBookUser(string? bookRequest, int pageIndex)
    {
        try
        {
            if (bookRequest == null)
            {
                var books = await _dbcontext.Books.Where(x => x.count > 0).ToListAsync();
                Response<Book> book = new Response<Book>()
                {
                    result = PaginatedList<Book>.CreateAsync(books, pageIndex, 20),
                    totalBook = _dbcontext.Books.ToList().Count
                };
                return book;
            }
            else
            {
                var books = await _dbcontext.Books.Where(x =>
                    x.nameBook.Contains(bookRequest) || x.author.Contains(bookRequest) ||
                    x.Category.categoryName.Contains(bookRequest)).Where(x => x.count > 0).ToListAsync();
                Response<Book> book = new Response<Book>()
                {
                    result = PaginatedList<Book>.CreateAsync(books, pageIndex, 20),
                    totalBook = _dbcontext.Books.ToList().Count
                };
                return book;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<List<string>> findNameAllBook(string? bookRequest)
    {
        try
        {
            var books = await _dbcontext.Books.Where(x =>
                    x.nameBook.Contains(bookRequest) || x.author.Contains(bookRequest) ||
                    x.Category.categoryName.Contains(bookRequest)).Where(x => x.count > 0).ToListAsync();
            List<String> name = new List<string>();
            foreach (var Book in books)
            {
                name.Add(Book.nameBook);
            }

            return name;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<Book>> findBookByCateId(string? categoryId)
    {
        try
        {
            if (categoryId!=null)
            {
                Response<Book> book = new Response<Book>()
                {
                    result = await _dbcontext.Books.Where(x => x.Category.categoryId == categoryId).ToListAsync(),
                    totalBook = _dbcontext.Books.Where(x => x.Category.categoryId == categoryId).Count()
                };
                return book;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}