using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Models;

namespace web_api.Controllers;

[ApiController]
[Route("developers")]
public class DeveloperController : ControllerBase
{
    private readonly ILogger<DeveloperController> _logger;
    private readonly DatabaseContext db;
    private readonly DeveloperValidator developerValidator;

    public DeveloperController(ILogger<DeveloperController> logger, DatabaseContext databaseContext){
        _logger = logger;
        db = databaseContext;
        developerValidator = new();
    }

    [HttpGet]
    public async Task<dynamic> list(string? firstName, string? lastName, int? age = -1, int? workedHours = -1)
    {
        await db.Database.EnsureCreatedAsync();
        var tempDevelopers = db.Developers.AsEnumerable();
        
        try
        {
            _logger.LogInformation($"DbSet developers type: {db.Developers.GetType()} \nTemp developers type: {tempDevelopers.GetType()}");
            if (firstName != default(string))
            {
                tempDevelopers = tempDevelopers.Where(dev => dev.firstName.IndexOf(firstName, StringComparison.InvariantCultureIgnoreCase) > -1);
            }
            if (lastName != default(string))
            {
                tempDevelopers = tempDevelopers.Where(dev => dev.lastName.IndexOf(lastName, StringComparison.InvariantCultureIgnoreCase) > -1);
            }
            if (age > -1)
            {   
                tempDevelopers = tempDevelopers.Where(dev => dev.age == age);
            }
            if (workedHours > -1)
            {
                tempDevelopers = tempDevelopers.Where(dev => dev.workedHours == workedHours);
            }

        }
        catch (System.Exception)
        {
            return BadRequest();
        }
        return tempDevelopers.ToList();
    }
    [HttpPost]
    public async Task<dynamic> create(string firstName, string lastName, int age, DeveloperType developerType, int workedHours, double salaryByHour, string email)
    {   
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
        var results = developerValidator.Validate(developer);
        if (! results.IsValid)
        {
                string errorsString = "";
            foreach (var error in results.Errors)
            {
                errorsString += $"Property {error.PropertyName} failed validation. Error was: {error.ErrorMessage}";
            }
            return BadRequest(errorsString);
        }
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

    [HttpPut]
    [Route("/developer/{id:int}")]
    public async Task<dynamic> editDeveloper(int id, string firstName, string lastName, int age, DeveloperType developerType, int workedHours, double salaryByHour, string email)
    {
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
            var results = developerValidator.Validate(developer);
            if (! results.IsValid)
            {
                    string errorsString = "";
                foreach (var error in results.Errors)
                {
                    errorsString += $"Property {error.PropertyName} failed validation. Error was: {error.ErrorMessage}";
                }
                return BadRequest(errorsString);
            }
            await db.SaveChangesAsync();
            return developer;
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}