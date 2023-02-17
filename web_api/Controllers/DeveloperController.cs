using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Models;

namespace web_api.Controllers;

[ApiController]
[Route("developers")]
public class DeveloperController : ControllerBase
{
    private readonly DatabaseContext db;

    public DeveloperController(DatabaseContext databaseContext){
        db = databaseContext;
    }

    [HttpGet]
    public async Task<dynamic> list(string? firstName, string? lastName, int? age, int? workedHours)
    {
        await db.Database.EnsureCreatedAsync();
        
        if (firstName != default(string) && lastName != default(string) && age != default(int) && workedHours != default(int))
        {
            try
            {
                var developer = from dev in db.Developers
                                    where dev.firstName.Equals(firstName) && dev.firstName.Equals(firstName) && dev.firstName.Equals(firstName) && dev.firstName.Equals(firstName)
                                    select dev;
                return developer;
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        return db.Developers.ToList();
    }
    [HttpPost]
    public async Task<dynamic> create(string firstName, string lastName, int age, string typeOfDeveloper, int workedHours, double salaryByHour, string email)
    {   
        if (firstName.Length < 3 || firstName.Length > 20) return BadRequest($"First Name does not comply the lenght policy. {firstName.Length < 3} {firstName.Length > 20}");
        if (lastName.Length < 3 || lastName.Length > 20) return BadRequest("Last Name does not comply the lenght policy.");
        if (age <= 10) return BadRequest("Does not comply the age policy.");
        if (workedHours < 30 || workedHours > 50) return BadRequest("Does not comply worked time policy.");
        if (salaryByHour < 13) return BadRequest("Does not comply the salary policy.");
        DeveloperType developerType;
        switch (typeOfDeveloper.ToLower())
        {
            case "junior":
                developerType = DeveloperType.Junior;
            break;
            case "intermediate":
                developerType = DeveloperType.Intermediate;
            break;
            case "senior":
                developerType = DeveloperType.Senior;
            break;
            case "lead":
                developerType = DeveloperType.Lead;
            break;
            default:
                return BadRequest("Does not match any type of developer policy.");
        }
        Developer developer = new Developer()
        {
            firstName = firstName,
            lastName = lastName,
            age = age,
            type = developerType,
            workedHours = workedHours,
            salaryByHour = salaryByHour,
            email = email
        };
        try
        {
            await db.Database.EnsureCreatedAsync();
            await db.Developers.AddAsync(developer);
            await db.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
        return "Created";
    }

    [HttpGet]
    [Route("/developer")]
    public async Task<dynamic> getDeveloper(string email)
    {   
        try
        {
            var developer = await (from dev in db.Developers
                                where dev.email.Equals(email)
                                select dev).FirstOrDefaultAsync();
            return developer;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("/developer")]
    public async Task<dynamic> deleteDeveloper(string email)
    {   
        try
        {
            var developer = await (from dev in db.Developers
                                where dev.email.Equals(email)
                                select dev).FirstOrDefaultAsync();
            db.Developers.Remove(developer);
            await db.SaveChangesAsync();
            return developer;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    [Route("/developer/{id:int}")]
    public async Task<dynamic> editDeveloper(int id, string firstName, string lastName, int age, string typeOfDeveloper, int workedHours, double salaryByHour, string email)
    {
        if (firstName.Length < 3 || firstName.Length > 20) return BadRequest($"First Name does not comply the lenght policy. {firstName.Length < 3} {firstName.Length > 20}");
        if (lastName.Length < 3 || lastName.Length > 20) return BadRequest("Last Name does not comply the lenght policy.");
        if (age <= 10) return BadRequest("Does not comply the age policy.");
        if (workedHours < 30 || workedHours > 50) return BadRequest("Does not comply worked time policy.");
        if (salaryByHour < 13) return BadRequest("Does not comply the salary policy.");
        DeveloperType developerType;
        switch (typeOfDeveloper.ToLower())
        {
            case "junior":
                developerType = DeveloperType.Junior;
            break;
            case "intermediate":
                developerType = DeveloperType.Intermediate;
            break;
            case "senior":
                developerType = DeveloperType.Senior;
            break;
            case "lead":
                developerType = DeveloperType.Lead;
            break;
            default:
                return BadRequest("Does not match any type of developer policy.");
        }
        try
        {
            var developer = await (from dev in db.Developers
                                where dev.developerId.Equals(id)
                                select dev).FirstOrDefaultAsync();
            
            developer.firstName = firstName;
            developer.lastName = lastName;
            developer.age = age;
            developer.type = developerType;
            developer.workedHours = workedHours;
            developer.salaryByHour = salaryByHour;
            developer.email = email;
            await db.SaveChangesAsync();
            return developer;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}