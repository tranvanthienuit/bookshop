using bookshop.DbContext;
using bookshop.Entity;
using bookshop.Entity.Model;
using bookshop.Paging;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface OrderInter
{
    public Task<bool> saveOrder(Order orderRequest);
    public Task<bool> deleteOrder(String orderId);
    public Task<bool> editOrder(Order orderRequest);
    public Task<Order> findOrderById(String orderId);
    public Task<Response<Order>> findOrder(String orderRequest,int pageIndex);
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

    public async Task<Response<Order>> findOrder(string? order,int pageIndex)
    {
        try
        {
            if (order==null)
            {
                Response<Order> orderList = new Response<Order>()
                {
                    result = PaginatedList<Order>.CreateAsync(_dbcontext.Orders.ToList(), pageIndex, 5),
                    totalBook = _dbcontext.Books.ToList().Count
                };
                return orderList;
            }
            var orderFilter = await _dbcontext.Orders.Where(x=>
                x.fullname.Contains(order) || x.address.Contains(order) ||
                x.telephone.Contains(order) || x.username.Contains(order) ||
                x.orderId.Contains(order) || x.status.Contains(order)).ToListAsync();
            if (orderFilter!=null)
            {
                Response<Order> orderList = new Response<Order>()
                {
                    result = PaginatedList<Order>.CreateAsync(orderFilter, pageIndex, 5),
                    totalBook = _dbcontext.Books.ToList().Count
                };
                return orderList;
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