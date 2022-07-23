﻿using bookshop.DbContext;
using bookshop.Entity;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface OrderDeInter
{
    public Task<bool> saveOrderDe(OrderDe orderRequest);
    public Task<bool> deleteOrderDe(String orderRequestId);
    public Task<OrderDe> findOrderDeById(String orderRequestId);
    public Task<List<OrderDe>> findOrderDe(String orderRequest);
}
public class OrderDeService: OrderDeInter
{
    private readonly Dbcontext _dbcontext;

    public OrderDeService(Dbcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<bool> saveOrderDe(OrderDe orderRequest)
    {
        try
        {
            await _dbcontext.OrderDes.AddAsync(orderRequest);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> deleteOrderDe(string orderRequestId)
    {
        try
        {
            OrderDe orderDe = await _dbcontext.OrderDes.FindAsync(orderRequestId) ?? throw new InvalidOperationException();
            _dbcontext.OrderDes.Remove(orderDe);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<OrderDe> findOrderDeById(string orderRequestId)
    {
        try
        {
            return await _dbcontext.OrderDes.FindAsync(orderRequestId) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<List<OrderDe>> findOrderDe(string orderRequest)
    {
        try
        {
            return await _dbcontext.OrderDes.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}