namespace BitMatrixConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var m = new BitMatrix(2, 3, new int[] { 1, 0, 1, 1 });
foreach (var bit in m)
                Console.Write(bit);
        }
    }
}
