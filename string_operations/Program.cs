using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace string_operations;
class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run(typeof(MyBenchmarks));
        Console.WriteLine(summary);
    }
}

public class MyBenchmarks
{
    public static string[] names = {"Stiven","Maikol","Salome"};
    
    [Benchmark]
    public void plusOperatorConcat() {
        plusOperatorConcat(names);
    }

    [Benchmark]
    public void interpolationConcat() {
        interpolationConcat(names);
    }

    [Benchmark]
    public void formatConcat() {
        formatConcat(names);
    }

    [Benchmark]
    public void stringBuilderConcat() {
        stringBuilderConcat(names);
    }

    [Benchmark]
    public void enumerableAggregateConcat() {
        enumerableAggregateConcat(names);
    }
    
    public string plusOperatorConcat(string [] chain)
    {
        string fullChain = "Hello ";
        foreach (string literal in chain)
        {
            fullChain += literal+" ";
        }
        return fullChain;
    }

    public string interpolationConcat(string [] chain)
    {
        string fullChain = "Hello ";
        foreach (string literal in chain)
        {
            fullChain = $"{fullChain}{literal} ";
        }
        return fullChain;
    }

    public string formatConcat(string [] chain)
    {
        string fullChain = "Hello ";
        foreach (string literal in chain)
        {
            fullChain = String.Format("{0}{1} ", fullChain, literal);
        }
        return fullChain;
    }

    public string stringBuilderConcat(string [] chain)
    {
        System.Text.StringBuilder fullChain = new System.Text.StringBuilder();
        fullChain.Append("Hello ");
        foreach (string literal in chain)
        {
            fullChain.Append(literal).Append(" ");
        }
        return fullChain.ToString();
    }

    public string enumerableAggregateConcat(string [] chain)
    {
        string fullChain = "Hello ";
        fullChain = chain.Aggregate((partialChain, literal) => $"{partialChain} {literal}");
        return fullChain;
    }
}