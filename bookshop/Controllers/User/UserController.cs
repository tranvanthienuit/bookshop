using bookshop.Entity.Model;
using bookshop.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.Controllers.User;
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly UserInter _userInter;
    private readonly OrderInter _orderInter;
    private readonly OrderDeInter _orderDeInter;

    public UserController(UserInter userInter)
    {
        _userInter = userInter;
    }

    //User
    [HttpGet("/user/delete-user/{userId}")]
    [HttpGet("/admin/delete-admin/{userId}")]
    public async Task<IActionResult> deleteUser(String userId)
    {
        try
        {
            var result = await _userInter.deleteUser(userId);
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
    [HttpGet("/user/edit-user")]
    [HttpGet("/admin/delete-admin/{userId}")]
    public async Task<IActionResult> editUser([FromBody] UserRequest userRequest)
    {
        try
        {
            var result = await _userInter.editUser(userRequest);
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
    [HttpGet("/user/find-user/{userId}")]
    [HttpGet("/admin/find-admin/{userId}")]
    public async Task<IActionResult> findUserById(String userId)
    {
        try
        {
            var user = await _userInter.findUserById(userId);
            if (user!=null)
            {
                return Ok(user);
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
    // quen mat khau
    
    
    
    
    // cai mat khau moi
    
    
    // tim user theo email
    public async Task<IActionResult> findUserByEmail(String email)
    {
        try
        {
            if (email!=null)
            {
                return Ok(await _userInter.findUser(email, 0));
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // order
    public async Task<IActionResult> findOrderById(String orderId)
    {
        try
        {
            if (orderId!=null)
            {
                return Ok(await _orderInter.findOrderById(orderId));
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    //orderdetail
    public async Task<IActionResult> findOrderDeByOrderId(String orderId)
    {
        try
        {
            if (orderId!=null)
            {
                return Ok(await _orderDeInter.findOrderDeById(orderId));
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