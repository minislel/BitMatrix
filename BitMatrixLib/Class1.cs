using System.Collections;
using System.Text;

// prostokątna macierz bitów o wymiarach m x n
public partial class BitMatrix : IEnumerable<int[]>, IEquatable<BitMatrix>
{
    private BitArray data;
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public bool IsReadOnly => false;
    public int this[int i, int j]
    {
        get { 
            if (i>NumberOfColumns || j > NumberOfRows)
            {
                throw new IndexOutOfRangeException("index is out of range");
            }
            return BoolToBit(data[i * NumberOfColumns + j]); 
        }
        set {
            if (i > NumberOfColumns || j > NumberOfRows)
            {
                throw new IndexOutOfRangeException("index is out of range");
            }
            data[i * NumberOfColumns + j] = BitToBool(value); 
        }

    }
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

    public BitMatrix(int[,] bits)
    {
        if (bits == null)
        {
            throw new NullReferenceException("bits cannot be null");
        }
        if (bits.Length == 0)
        {
            throw new ArgumentOutOfRangeException("no bits provided");

        }
        NumberOfColumns = bits.GetLength(1);
        NumberOfRows = bits.GetLength(0);
        data = new BitArray(NumberOfColumns * NumberOfRows);
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                data[i * NumberOfColumns + j] = BitToBool(bits[i, j]);
            }
        }
    }
    public BitMatrix(bool[,] bits)
    {
        if (bits == null)
        {
            throw new NullReferenceException("bits cannot be null");
        }
        if (bits.Length == 0)
        {
            throw new ArgumentOutOfRangeException("no bits provided");

        }
        NumberOfColumns = bits.GetLength(1);
        NumberOfRows = bits.GetLength(0);
        data = new BitArray(NumberOfColumns * NumberOfRows);
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                data[i * NumberOfColumns + j] = bits[i, j];
            }
        }
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


    public static bool operator ==(BitMatrix A, BitMatrix B)
    {

        if (ReferenceEquals(A, B))
        {
            return true;
        }
        if (ReferenceEquals(A, null) || ReferenceEquals(B, null))
        {
            return false;
        }

        return A.Equals(B);
    }

    public static bool operator !=(BitMatrix A, BitMatrix B) => !(A == B);
    public bool Equals(BitMatrix other)
    {

        if (ReferenceEquals(other, this))
        {
            return true;
        }
        if (ReferenceEquals(other, null) || ReferenceEquals(this, null))
        {
            return false;
        }



        if (this.NumberOfColumns == other.NumberOfColumns && this.NumberOfRows == other.NumberOfRows && this.data.Length == other.data.Length)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (this.data[i] != other.data[i])
                { return false; }
            }
            return true;
        }
        return false;
    }
    public override bool Equals(object obj)
    {

        if (!ReferenceEquals(obj, null) && (obj.GetType() == this.GetType()))
        {
            return Equals(obj);
        }
        else
        {
            return false;
        }
    }



    public override int GetHashCode()
    {
        return HashCode.Combine(data, NumberOfColumns, NumberOfRows);
    }

    public IEnumerator<int[]> GetEnumerator()
    {
        for (int i = 0; i < NumberOfRows; i++)
        {
            int[] row = new int[NumberOfColumns];
            for (int j = 0; j < NumberOfColumns; j++)
            {
                row[j] = this[i, j];
            }
            yield return row;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}