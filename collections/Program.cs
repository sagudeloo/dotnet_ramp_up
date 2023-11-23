namespace collections;
class Program
{
    /*
     * To Practice:
     * - Naming Conventions
     *
     *
     * Increment:
     * - Practice the queue in any of the exercises.  https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/collections
     */

    static void Main(string[] args)
    {
        try
        {
            List<string> listOfNames = new List<string>();
            Stack<string> stackOfNames = new Stack<string>();
            string? line = "";
            line = Console.ReadLine();
            while (!line.Equals("e"))
            {
                listOfNames.Add(line);
                stackOfNames.Push(line);
                line = Console.ReadLine();
            }
            Console.WriteLine("Default output");
            listOfNames.ForEach(name => Console.WriteLine(name));
            Console.WriteLine("Reversed List");
            listOfNames.Reverse();
            listOfNames.ForEach(name => Console.WriteLine(name));
            listOfNames.Clear();
            Console.WriteLine("Reverse output");

            // Increment - Refactor this exercise using remove from the listOfName. Let's talk later about it. 
            for (int i = stackOfNames.Count; i > 0; i--)
            {
                Console.WriteLine(stackOfNames.Pop());
            }
            Console.WriteLine($"Count: {stackOfNames.Count}");
        }
        catch
        {
            // This is a bad practice. When you add a try catch you should do something or rethrow the exception. 
        }
    }
}
