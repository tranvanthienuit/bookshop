using bookshop.Entity;

namespace bookshop.Service;

public interface OrderDeInter
{
    public Task<bool> saveOrderDe(OrderDe order);
    public Task<bool> deleteOrderDe(String orderId);
    public Task<bool> editOrderDe(OrderDe order);
    public Task<Order> findOrderDeById(String orderId);
    public Task<List<OrderDe>> findOrderDe(String order);
}
public class OrderDeService: OrderDeInter
{
    public Task<bool> saveOrderDe(OrderDe order)
    {
        throw new NotImplementedException();
    }

    public Task<bool> deleteOrderDe(string orderId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> editOrderDe(OrderDe order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> findOrderDeById(string orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderDe>> findOrderDe(string order)
    {
        throw new NotImplementedException();
    }
}