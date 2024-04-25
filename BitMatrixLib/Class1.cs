using System.Collections;
using System.Text;

// prostokątna macierz bitów o wymiarach m x n
public partial class BitMatrix : IEnumerable<BitMatrix>
{
    private BitArray data;
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public bool IsReadOnly => false;

    // tworzy prostokątną macierz bitową wypełnioną `defaultValue`
    public BitMatrix(int numberOfRows, int numberOfColumns, int defaultValue = 0)
    {
        if (numberOfRows < 1 || numberOfColumns < 1)
            throw new ArgumentOutOfRangeException("Incorrect size of matrix");
        data = new BitArray(numberOfRows * numberOfColumns, BitToBool(defaultValue));
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
    }
    public BitMatrix(int numberOfRows, int numberOfColumns, params int[] bits)
    {
        if (numberOfRows < 1 || numberOfColumns < 1)
            throw new ArgumentOutOfRangeException("Incorrect size of matrix");
        if (bits == null || bits.Length == 0)
        {
            data = new BitArray(numberOfRows * numberOfColumns, false);
        }

        else
        {
            data = new BitArray(numberOfRows * numberOfColumns, false);
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    try
                    {
                        data[i * numberOfColumns + j] = BitToBool(bits[i * numberOfColumns + j]);
                    }
                    catch 
                    {
                        
                    }
                }

            }
        }
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
    }

    public static int BoolToBit(bool boolValue) => boolValue ? 1 : 0;
    public static bool BitToBool(int bit) => bit != 0;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbrow = new StringBuilder();
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                if (j < NumberOfColumns)
                {
                    sbrow.Append(BoolToBit(data[i * NumberOfColumns + j]));
                }
            }
            sb.Append(sbrow);
            sb.Append(Environment.NewLine);
            sbrow.Clear();
        }
        return sb.ToString();
    }

    public IEnumerator<BitMatrix> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}