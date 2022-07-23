using bookshop.DbContext;
using bookshop.Entity;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface OrderInter
{
    public Task<bool> saveOrder(Order orderRequest);
    public Task<bool> deleteOrder(String orderId);
    public Task<bool> editOrder(Order orderRequest);
    public Task<Order> findOrderById(String orderId);
    public Task<List<Order>> findOrder(String orderRequest);
}
public class OrderService : OrderInter
{
    private readonly Dbcontext _dbcontext;

    public OrderService(Dbcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<bool> saveOrder(Order order)
    {
        try
        {
            await _dbcontext.Orders.AddAsync(order);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> deleteOrder(string orderId)
    {
        try
        {
            Order order = await _dbcontext.Orders.FindAsync(orderId) ?? throw new InvalidOperationException();
            _dbcontext.Orders.Remove(order);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> editOrder(Order orderRequest)
    {
        try
        {
            var order = await _dbcontext.Orders.FindAsync(orderRequest.orderId);
            if (order!=null)
            {
                order.telephone = orderRequest.telephone;
                order.address = orderRequest.address;
                order.fullname = orderRequest.fullname;
            }
            _dbcontext.Orders.Update(order ?? throw new InvalidOperationException());
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<Order> findOrderById(string orderId)
    {
        try
        {
            return await _dbcontext.Orders.FindAsync(orderId) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return  null;
        }
    }

    public async Task<List<Order>> findOrder(string order)
    {
        try
        {
            return await _dbcontext.Orders.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}