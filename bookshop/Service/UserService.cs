using bookshop.Entity.Model;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.Service;

public interface UserInter
{
    public Task<IActionResult> saveUser(UserRequest userRequest);
}
public class UserService : UserInter
{
    public Task<IActionResult> saveUser(UserRequest userRequest)
    {
        throw new NotImplementedException();
    }
}