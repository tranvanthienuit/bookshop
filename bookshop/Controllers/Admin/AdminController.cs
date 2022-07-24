using bookshop.Entity;
using bookshop.Entity.Model;
using bookshop.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.Controllers.Admin;
[ApiController]
[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly BookInter _bookInter;
    private readonly CateInter _cateInter;
    private readonly OrderInter _orderInter;
    private readonly OrderDeInter _orderDeInter;
    private readonly UserInter _userInter;

    public AdminController(BookInter bookInter, CateInter cateInter, OrderInter orderInter, OrderDeInter orderDeInter, UserInter userInter)
    {
        _bookInter = bookInter;
        _cateInter = cateInter;
        _orderInter = orderInter;
        _orderDeInter = orderDeInter;
        _userInter = userInter;
    }

    //Book
    [HttpPost("/admin/save-book")]
    public async Task<IActionResult> saveBook([FromBody] BookRequest bookRequest)
    {
        try
        {
            var result = await _bookInter.saveBook(bookRequest);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    [HttpGet("/admin/delete-book/{bookId}")]
    public async Task<IActionResult> deleteBook(String bookId)
    {
        try
        {
            var result = await _bookInter.deleteBook(bookId);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    [HttpPut("/admin/edit-book")]
    public async Task<IActionResult> editBook([FromBody] BookRequest bookRequest)
    {
        try
        {
            var result = await _bookInter.editBook(bookRequest);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    
    
    //category
    [HttpPost("/admin/save-category")]
    public async Task<IActionResult> saveCategory([FromBody] Category category)
    {
        try
        {
            var result = await _cateInter.saveCate(category);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    [HttpGet("/admin/delete-category/{cateId}")]
    public async Task<IActionResult> deleteCategory(String cateId)
    {
        try
        {
            var result = await _cateInter.deleteCate(cateId);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    [HttpPut("/admin/edit-category")]
    public async Task<IActionResult> editBook([FromBody] Category category)
    {
        try
        {
            var result = await _cateInter.editCate(category);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    //Order
    [HttpGet("/admin/delete-order/{orderId}")]
    public async Task<IActionResult> deleteOrder(String orderId)
    {
        try
        {
            var result = await _orderInter.deleteOrder(orderId);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    [HttpPut("/admin/edit-order")]
    public async Task<IActionResult> editBook([FromBody] Order order)
    {
        try
        {
            var result = await _orderInter.editOrder(order);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    //orderdetail
    [HttpGet("/admin/delete-orderde/{orderdeId}")]
    public async Task<IActionResult> deleteOrderDe(String orderDeId)
    {
        try
        {
            var result = await _orderDeInter.deleteOrderDe(orderDeId);
            if (result)
            {
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    //User
    [HttpGet("/admin/find-user/{text}/{pageIndex}")]
    public async Task<IActionResult> findUser(String text,int pageIndex=1)
    {
        try
        {
            var result = await _userInter.findUser(text,pageIndex);
            if (result!=null)
            {
                return Ok(result);
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
}