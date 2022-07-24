using bookshop.Entity.Model;
using bookshop.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using uitbooks.Token;

namespace bookshop.Controllers.Common;

[ApiController]
public class HomeController : Controller
{
    private readonly UserManager<Entity.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<Entity.User> _signInManager;
    private readonly UserInter _userInter;
    private readonly IJwtUtils _jwtUtils;
    private readonly BookInter _bookInter;

    public HomeController(UserManager<Entity.User> userManager, RoleManager<IdentityRole> roleManager,
        SignInManager<Entity.User> signInManager, UserInter userInter, IJwtUtils jwtUtils, BookInter bookInter)
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
                Entity.User user = await _userManager.FindByNameAsync(userRequest.username);
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
            Entity.User user = await _userManager.FindByNameAsync(userLogin.username);
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
            return Ok(await _bookInter.findBook(null, pageIndex));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}