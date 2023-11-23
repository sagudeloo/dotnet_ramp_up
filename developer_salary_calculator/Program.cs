using System.IO;

namespace developer_salary_calculator;
class Program
{
    /*
     * To Practice:
     * Refactor the code using FileStream
     * https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-8.0
     *
     * To answer:
     * What's the difference between StreamReader and FileStream?
     * What is an Stream?
     *
     */

    static void Main(string[] args)
    {
        using(var reader = new StreamReader(@"C:\Users\stiven.agudeloo\Projects\perficient\dotnet\developer_salary_calculator\developers.csv"))
        {   
            string? line = "";
            List<string> namesList = new List<string>();
            List<string> typesList = new List<string>();
            List<int> workedHoursList = new List<int>();
            List<double> salaryPerHourList = new List<double>();

            // Developer class missed.
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] splitedLine = line.Split(";");
                namesList.Add(splitedLine[0]);
                typesList.Add(splitedLine[1]);
                workedHoursList.Add(int.Parse(splitedLine[2]));
                salaryPerHourList.Add(Double.Parse(splitedLine[3]));
            }
            string[] names = namesList.ToArray();
            string[] types = typesList.ToArray();
            int[] workedHours = workedHoursList.ToArray();
            double[] salaryPerHour = salaryPerHourList.ToArray();
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"Dev Name: {names[i]}\n"+
                $"Dev Type: {types[i]}\n"+
                $"Worked Hours: {workedHours[i]}\n"+
                $"SalaryByHour: {salaryPerHour[i]} USD\n");
            }
        }
    }
}
