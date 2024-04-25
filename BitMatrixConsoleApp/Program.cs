namespace BitMatrixConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var m = new BitMatrix(2, 4, new int[] {1,0,1,1,0,0,0,0});
            Console.WriteLine(m);

            m = new BitMatrix(2, 2, 1, 0, 0, 1, 1,1,0);
            Console.WriteLine(m);
            // konstruktor BitMatrix(int, int, params int[])
            // macierz 2x2, za dużo danych w tablicy
             m = new BitMatrix(2, 2, 1, 0, 0, 1, 1, 1, 0);
            Console.WriteLine(m);

            // macierz 3x2, za mało danych w tablicy
            m = new BitMatrix(3, 2, 1, 0, 0, 1, 1);
            Console.WriteLine(m);
            // konstruktor BitMatrix(int[,])
            int[,] arr = new int[,] { { 1, 0, 1 }, { 0, 1, 1 } };
             m = new BitMatrix(arr);
            Console.WriteLine(arr.GetLength(0) == m.NumberOfRows);
            Console.WriteLine(arr.GetLength(1) == m.NumberOfColumns);
            Console.Write(m.ToString());

            // konstruktor BitMatrix(bool[,])
            bool[,] arrb = new bool[,] { { true, false, true }, { false, true, true } };
            m = new BitMatrix(arrb);
            Console.WriteLine(arrb.GetLength(0) == m.NumberOfRows);
            Console.WriteLine(arrb.GetLength(1) == m.NumberOfColumns);
            Console.Write(m.ToString());
        }
    }
}
