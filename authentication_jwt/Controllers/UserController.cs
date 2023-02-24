using Microsoft.AspNetCore.Mvc;
using authentication_jwt.Models;
using Microsoft.AspNetCore.Authorization;

namespace authentication_jwt.Controllers;

[ApiController]
[Route("/api/Users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DatabaseContext _databaseContext;

    public UserController(ILogger<UserController> logger, DatabaseContext databaseContext)
    {
        (_logger, _databaseContext) = (logger, databaseContext);
    }

    [Authorize(Roles = "Reader, Contributor, Manager")]
    [HttpGet]
    public List<User> get(){
        return _databaseContext.Users.ToList();
    }

    [Authorize(Roles = "Contributor, Manager")]
    [HttpPost]
    public async Task<dynamic> create(Guid id, string name, string email, string password, DateTime createdAt, UserRole role, bool isActiveRole){
        try
        {
            User user = new User(){
                Id = id,
                Name = name,
                Email = email,
                Password = password,
                CreatedAt = createdAt,
                Role = role,
                IsActiveRole = isActiveRole
            };
            _databaseContext.Add(user);
            await _databaseContext.SaveChangesAsync();
            return Ok();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Reader, Contributor, Manager")]
    [HttpGet]
    [Route("{id}")]
    public dynamic getById(Guid id){
        try
        {
            var user = (from u in _databaseContext.Users
                        where u.Id.Equals(id)
                        select u).FirstOrDefault();
            return user;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Contributor, Manager")]
    [HttpPut]
    [Route("{id}")]
    public async Task<dynamic> edit(Guid id, string name, string email, string password, DateTime createdAt, UserRole role, bool isActiveRole){
        try
        {
            var user = (from u in _databaseContext.Users
                        where u.Id.Equals(id)
                        select u).FirstOrDefault();
            user.Id = id;
            user.Name = name;
            user.Email = email;
            user.Password = password;
            user.CreatedAt = createdAt;
            user.Role = role;
            user.IsActiveRole = isActiveRole;
            await _databaseContext.SaveChangesAsync();
            return user;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<dynamic> delete(Guid id){
        try
        {
            var user = (from u in _databaseContext.Users
                        where u.Id.Equals(id)
                        select u).FirstOrDefault();
            _databaseContext.Remove(user);
            await _databaseContext.SaveChangesAsync();
            return user;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}