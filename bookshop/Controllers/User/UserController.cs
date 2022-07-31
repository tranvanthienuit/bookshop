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
    
    
    // tim order
    
    
    
    //tim orderdetail
    
}