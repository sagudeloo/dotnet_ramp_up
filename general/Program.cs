﻿namespace general;
class Program
{
    /*
     * To practice:
     * - Review Naming Convention.
     *
     * Additional Material:
     * https://github.com/pslcorp/perficient.training.content/blob/main/content/guides-lectures/conventions.md
     * https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/interop/using-type-dynamic
     * https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/member-overloading
     */

    static void Main(string[] args)
    {   
        int salary1 = 33;
        decimal salary2 = 26.5M;
        float salary3 = 39.99F;
        double salary4 = 66.33D;

        int hours = 240;

        Console.WriteLine(calculateSalary(salary1, hours));
        Console.WriteLine(calculateSalary(salary2, hours));
        Console.WriteLine(calculateSalary(salary3, hours));
        Console.WriteLine(calculateSalary(salary4, hours));
    }

    static dynamic calculateSalary(dynamic baseSalary, int hoursWorked){
        return baseSalary * hoursWorked;
    }
}
