using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace string_operations;

/*
 * To Practice:
 * Naming Conventions
 *
 * Increment:
 * - Remove the foreach in all method to compare the benchmark results.
 * - Analyze the different algorithms using https://sharplab.io/ and let's talk about it. 
 */

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run(typeof(MyBenchmarks));
        Console.WriteLine(summary);
    }
}

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class MyBenchmarks
{
    public static string[] names = {"Stiven","Maikol","Salome"};

    // By default an Empty Contructor is created, this constructor is not needed. 
    public MyBenchmarks()
    {
        
    }
    
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