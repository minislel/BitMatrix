namespace BitMatrixConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var m = new BitMatrix(2, 2, new int[] { 1, 0, 0, 1 });
            Console.WriteLine(m);

            m = new BitMatrix(2, 2, 1, 0, 0, 0);
            Console.WriteLine(m);
        }
    }
}
