namespace keywords_usage;
class Program
{
    /*
     *Additional documents. 
     *https://www.pluralsight.com/guides/csharp-in-out-ref-parameters
     *https://github.com/pslcorp/perficient.training.content/blob/main/content/guides-lectures/conventions.md
     *https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof
     *
     * To improve:
     * - Practice an review naming conventions. 
     *
     * Increment:
     * - Change the exercise to learn how to use in, out and ref.
     * - Review the naming convention. 
     * - In the exercise one refactor the code to use the operator nameof
     */

    static void Main(string[] args)
    {   
        // Exercise 1
        exerciseOne("John","25","test@test.com");
        Console.WriteLine("____________________\n");
        // Exercise 2
        Console.WriteLine(exerciseTwo(2,4,5,8,10,84,65));
        Console.WriteLine("____________________\n");
        // Exercise 3
        string salary = Console.ReadLine();
        int intSalary = int.Parse(salary);
        Console.WriteLine(intSalary>50000);
        Console.WriteLine(intSalary>100000 ? CurrencyToWord(intSalary, string.Empty) : "");

    }

    //Review naming convention. 
    static void exerciseOne(string name, string age, string email){
        Console.WriteLine($"Name: {name}\nAge: {age}\nEmail: {email}");
    }

    //Review naming convention. 
    static string exerciseTwo(params int[] numbers){
        int sum = 0;
        string stringSum = "";
        for (int i = 0; i < numbers.Length; i++){
            sum += numbers[i];
            stringSum += numbers[i];
            if (i < numbers.Length-1) stringSum += " + ";
        }
        return stringSum + " = " + sum;
    }

    static string CurrencyToWord(int number, string word)
    {
        if (number / 1_000_000 != 0)
        {
            word = string.Concat(CurrencyToWord(number / 1_000_000, word), " million ");
            number %= 1_000_000;
        }

        if (number / 1_000 != 0)
        {
            word = string.Concat(CurrencyToWord(number / 1_000, word), " thousand ");
            number %= 1_000;
        }

        if (number / 100 != 0)
        {
            word = string.Concat(CurrencyToWord(number / 100, word), " hundred ");
            number %= 100;
        }

        word = NumberToWord(number, word);

        return word;
    }

    static string NumberToWord(int number, string words)
    {
        if (words != "") words += " ";

        var unitValues = new[]
        {
            "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve",
            "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        };
        var tensValues = new[]
            { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };


        if (number >= 20)
        {
            words += tensValues[number / 10];
            if (number % 10 > 0) words += "-" + unitValues[number % 10];
        }
        else
            words += unitValues[number];

        return words;
    }

}
