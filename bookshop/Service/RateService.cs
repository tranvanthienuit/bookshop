using bookshop.DbContext;
using bookshop.Entity;

namespace bookshop.Service;

public interface RateInter
{
    public Task<bool> saveRate(Rate rate);
}
public class RateService: RateInter
{
    private readonly Dbcontext _dbcontext;

    public RateService(Dbcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<bool> saveRate(Rate rate)
    {
        try
        {
            await _dbcontext.Rates.AddAsync(rate);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}