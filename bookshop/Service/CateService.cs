using bookshop.DbContext;
using bookshop.Entity;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface CateInter
{
    public Task<bool> saveCate(Category Cate);
    public Task<bool> deleteCate(String cateId);
    public Task<bool> editCate(Category Cate);
    public Task<Category> findCateById(String cateId);
    public Task<List<Category>> findCate(String Cate);
}

public class CateService : CateInter
{
    private readonly Dbcontext _dbcontext;

    public CateService(Dbcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<bool> saveCate(Category Cate)
    {
        try
        {
            await _dbcontext.Categories.AddAsync(Cate);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> deleteCate(string cateId)
    {
        try
        {
            Category category = await _dbcontext.Categories.FindAsync(cateId) ?? throw new InvalidOperationException();
            _dbcontext.Categories.Remove(category);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> editCate(Category Cate)
    {
        try
        {
            Category category = await _dbcontext.Categories.FindAsync(Cate.categoryId) ??
                                throw new InvalidOperationException();
            if (category != null)
            {
                category.categoryName = Cate.categoryName;
            }

            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<Category> findCateById(string cateId)
    {
        try
        {
            return await _dbcontext.Categories.FindAsync(cateId) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<List<Category>> findCate(string Cate)
    {
        try
        {
            return await _dbcontext.Categories.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}