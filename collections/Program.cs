namespace collections;
class Program
{
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
            for (int i = stackOfNames.Count; i > 0; i--)
            {
                Console.WriteLine(stackOfNames.Pop());
            }
            Console.WriteLine($"Count: {stackOfNames.Count}");
        }
        catch
        {

        }
    }
}
