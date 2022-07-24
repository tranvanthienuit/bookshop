using bookshop.Entity;
using bookshop.Entity.Model;
using bookshop.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using uitbooks.Token;

namespace bookshop.Controllers.Common;

[ApiController]
public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserInter _userInter;
    private readonly IJwtUtils _jwtUtils;
    private readonly BookInter _bookInter;

    public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        SignInManager<User> signInManager, UserInter userInter, IJwtUtils jwtUtils, BookInter bookInter)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _userInter = userInter;
        _jwtUtils = jwtUtils;
        _bookInter = bookInter;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> register([FromBody] UserRequest userRequest)
    {
        try
        {
            var result = await _userInter.saveUser(userRequest);
            if (result)
            {
                User user = await _userManager.FindByNameAsync(userRequest.username);
                await _userManager.AddToRoleAsync(user, "user");
                return Ok("thanh cong");
            }

            return Ok("that bai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");;
        }
    }

    [HttpPost("/login")]
    public async Task<IActionResult> login([FromBody] UserLogin userLogin)
    {
        try
        {
            User user = await _userManager.FindByNameAsync(userLogin.username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(userLogin.username, userLogin.password, false, true);
                await _signInManager.SignInAsync(user, false);
                if (result.Succeeded)
                {
                    return Ok(_jwtUtils.GenerateToken(user));
                }
            }

            return Ok("user khong ton tai");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");;
        }
    }

    [HttpGet("/home/{pageIndex}")]
    public async Task<IActionResult> home(int pageIndex=1)
    {
        try
        {
            return Ok(_bookInter.findBook(null, pageIndex));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}