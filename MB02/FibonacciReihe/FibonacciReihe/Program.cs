namespace FibonacciReihe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test");
            Console.WriteLine(FibonacciRecursive(50));
        }

        static long FibonacciRecursive(long len)
        {
            if (len == 1 || len == 2)
                return 1;

            return FibonacciRecursive(len - 1) + FibonacciRecursive(len - 2);
        }
    }
}