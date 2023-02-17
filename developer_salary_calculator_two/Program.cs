using Microsoft.EntityFrameworkCore;

namespace developer_salary_calculator_two;
class Program
{   
    static string database = "dbSqlite.db";
    static async Task Main(string[] args)
    {
        using( var db = new DatabaseContext(database))
        {
            await db.Database.EnsureCreatedAsync();
            
            List<Developer> developers = db.Developers.ToList();

            double totalSalary = 0;
            int totalHours = 0;
            int totalDevs = 0;
            foreach ( Developer developer in developers )
            {
                string name = developer.Name;
                string type = developer.Type;
                int workedHours = developer.WorkedHours;
                double salaryByHour = developer.SalaryByHour;
                Console.WriteLine($"Dev Name: {name}\n"+
                $"Dev Type: {type}\n"+
                $"Worked Hours: {workedHours}\n"+
                $"SalaryByHour: {salaryByHour} USD\n");
                double salaryMultiplier = 1;
                switch (type)
                {
                    case "Intermediate":
                    salaryMultiplier = 1.12;
                    break;
                    case "Senior":
                    salaryMultiplier = 1.25;
                    break;
                    case "Lead":
                    salaryMultiplier = 1.5;
                    break;
                    default:
                    salaryMultiplier = 1;
                    break;
                }
                double actualSalary = salaryByHour * workedHours * salaryMultiplier;
                totalSalary += actualSalary;
                totalHours += workedHours;
                totalDevs++;
            }
                Console.WriteLine($"Resume:\n"+
                $"Total Salary: {totalSalary} USD\n"+
                $"Total Hours: {totalHours}\n"+
                $"Total Devs: {totalDevs}\n");
        }
    }
}
