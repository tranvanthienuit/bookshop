using bookshop.DbContext;
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
    private readonly UserManager<Entity.User> _userManager;
    private readonly SignInManager<Entity.User> _signInManager;
    private readonly UserInter _userInter;
    private readonly IJwtUtils _jwtUtils;
    private readonly BookInter _bookInter;
    private readonly OrderInter _orderInter;
    private readonly OrderDeInter _orderDeInter;
    private readonly Dbcontext _dbcontext;
    private readonly CateInter _cateInter;

// nguoi dung
    [HttpPost("/dang-ky")]
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
            return Ok("that bai");
            ;
        }
    }

    [HttpPost("/dang-nhap")]
    public async Task<IActionResult> login([FromBody] UserLogin userLogin)
    {
        try
        {
            Entity.User user = await _userManager.FindByNameAsync(userLogin.username);
            if (user != null)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(userLogin.username, userLogin.password, false, true);
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
            return Ok("that bai");
            ;
        }
    }

//home
    [HttpGet("/trang-chu/{pageIndex}")]
    public async Task<IActionResult> home(int pageIndex = 0)
    {
        try
        {
            return Ok(await _bookInter.findBookUser(null, pageIndex));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
// loai sach

    [HttpGet("/category")]
    public async Task<IActionResult> getAllCategory()
    {
        try
        {
            return Ok(await _cateInter.findCate(null, 0));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
// sach
    [HttpGet("/tim-sach")]
    public async Task<IActionResult> findBookUser([FromBody] Dictionary<String, String> keyword)
    {
        try
        {
            if (keyword.ContainsKey("infoBook") != null)
            {
                return Ok(await _bookInter.findBookUser(keyword["infoBook"], 0));
            }

            return Ok(null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet("/xem-chi-tiet-sach/{bookId}")]
    public async Task<IActionResult> bookDetail(String bookId)
    {
        try
        {
            if (bookId!=null)
            {
                return Ok(await _bookInter.findBookById(bookId));
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    [HttpPost("/search/book")]
    public async Task<IActionResult> searchNameBook([FromBody] Dictionary<String, String> keyword)
    {
        try
        {
            if (keyword.ContainsKey("infoBook") != null)
            {
                return Ok(await _bookInter.findNameAllBook(keyword["infoBook"]));
            }

            return Ok(null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet("/tim-sach/{categoryId}")]
    public async Task<IActionResult> findBookByCateId(String categoryId)
    {
        try
        {
            return Ok(await _bookInter.findBookByCateId(categoryId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    } 
    // public async Task<IActionResult> findBookByCondition([FromBody] Dictionary<String, String> keyword)
    // {
    //     
    // }
    // public async Task<IActionResult> findDataFilter([FromBody] Dictionary<String, String> keyword)
    // {
    //     
    // }
    // public async Task<IActionResult> appriciateBook([FromBody] Dictionary<String, String> keyword)
    // {
    //     
    // }
    
    // order
    
    
    
    
    //orderdetail
    
    
    
    //comment
    //them comment
    
    
    // xoa comment
    
    
    
    //sua comment
// mua sach
    [HttpPost("/buy/book")]
    public async Task<IActionResult> buy([FromBody] cartRequest cartRequest)
    {
        try
        {
            String username = Thread.CurrentPrincipal.Identity.Name;
            Entity.User user = await _userManager.FindByNameAsync(username);
            Order order = new Order()
            {
                totalBook = cartRequest.totalBook,
                totalPrice = cartRequest.totalPrice,
                telephone = user.telephone,
                address = user.address,
                status = "chua giao",
                pay = "chua thanh toan",
                username = user.UserName,
                fullname = user.fullname,
                User = user
            };
            var orderResult = await _orderInter.saveOrder(order);
            if (!orderResult)
            {
                Console.WriteLine(orderResult);
            }

            Order orderData = await _orderInter.findOrderById(order.orderId);
            Parallel.ForEach(cartRequest.book, async (x) =>
            {
                OrderDe orderDe = new OrderDe()
                {
                    count = x.totalBook,
                    totalPrice = x.totalPrice,
                    Book = x.book,
                    Order = orderData
                };
                var orderDeResult = await _orderDeInter.saveOrderDe(orderDe);
                if (!orderDeResult)
                {
                    Console.WriteLine(orderDeResult);
                }

                var book = await _bookInter.findBookById(x.book.bookId);
                int value = (book.count - x.totalBook);
                book.count = value;
                _dbcontext.Update(book);
                await _dbcontext.SaveChangesAsync();
            });
            return Ok("thanh cong");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Ok("that bai");
        }
    }
}