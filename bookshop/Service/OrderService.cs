using bookshop.Entity;

namespace bookshop.Service;

public interface OrderInter
{
    public Task<bool> saveOrder(Order order);
    public Task<bool> deleteOrder(String orderId);
    public Task<bool> editOrder(Order order);
    public Task<Order> findOrderById(String orderId);
    public Task<List<Order>> findOrder(String order);
}
public class OrderService : OrderInter
{
    public Task<bool> saveOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<bool> deleteOrder(string orderId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> editOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> findOrderById(string orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> findOrder(string order)
    {
        throw new NotImplementedException();
    }
}