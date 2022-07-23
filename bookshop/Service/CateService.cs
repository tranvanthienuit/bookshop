using bookshop.Entity;

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
    public Task<bool> saveCate(Category Cate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> deleteCate(string cateId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> editCate(Category Cate)
    {
        throw new NotImplementedException();
    }

    public Task<Category> findCateById(string cateId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> findCate(string Cate)
    {
        throw new NotImplementedException();
    }
}