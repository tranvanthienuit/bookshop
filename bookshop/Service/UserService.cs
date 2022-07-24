using System.Text;
using bookshop.DbContext;
using bookshop.Entity;
using bookshop.Entity.Model;
using bookshop.Paging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bookshop.Service;

public interface UserInter
{
    public Task<bool> saveUser(UserRequest userRequest);
    public Task<bool> deleteUserById(String userId);
    public Task<bool> editUser(UserRequest userRequest);
    public Task<List<User>> findUser(String user,int pageIndex);
    public Task<User> findUserById(String userId);
}
public class UserService : UserInter
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly Dbcontext _dbContext;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, Dbcontext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task<bool> saveUser(UserRequest userRequest)
    {
        try
        {
            var adminRole = await _roleManager.RoleExistsAsync("admin");
            if (!adminRole)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = "admin";
                await _roleManager.CreateAsync(identityRole);
            }
            var userRole = await _roleManager.RoleExistsAsync("user");
            if (!userRole)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = "seller";
                await _roleManager.CreateAsync(identityRole);
            }
            var sellerRole = await _roleManager.RoleExistsAsync("seller");
            if (!sellerRole)
            {
                IdentityRole identityRole = new IdentityRole();
                identityRole.Name = "seller";
                await _roleManager.CreateAsync(identityRole);
            }

            var userExist = await _userManager.FindByNameAsync(userRequest.username);
            if (userExist!=null)
            {
                return false;
            }
            User user = new User()
            {
                UserName = userRequest.username,
                fullname = userRequest.fullname,
                address = userRequest.address,
                sex = userRequest.sex,
                image = Encoding.ASCII.GetBytes(userRequest.image)
            };
            var result = await _userManager.CreateAsync(user,userRequest.password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> deleteUserById(string userId)
    {
        try
        {
            var userRole = _dbContext.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
            User user = await _userManager.FindByIdAsync(userRole.RoleId);
            IdentityRole role = await _roleManager.FindByIdAsync(userRole.RoleId);
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> editUser(UserRequest userRequest)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(userRequest.username);
            if (user!=null)
            {
                user.UserName = userRequest.username;
                user.fullname = userRequest.fullname;
                user.address = userRequest.address;
                user.sex = userRequest.sex;
                user.image = Encoding.ASCII.GetBytes(userRequest.image);
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<List<User>> findUser(string user,int pageIndex)
    {
        try
        {
            int pageNumber = pageIndex;
            var userList = _userManager.Users.ToList();
            return PaginatedList<User>.CreateAsync(userList, pageNumber, 5);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<User> findUserById(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }
}