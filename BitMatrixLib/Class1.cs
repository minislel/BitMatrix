using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


// prostokątna macierz bitów o wymiarach m x n
public partial class BitMatrix : IEnumerable<int>, IEquatable<BitMatrix>, ICloneable
{
    private BitArray data;
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public bool IsReadOnly => false;
    public int this[int i, int j]
    {
        get
        {
            if (i >= NumberOfRows || j >= NumberOfColumns || i < 0 || j < 0)
            {
                throw new IndexOutOfRangeException("index is out of range");
            }
            return BoolToBit(data[i * NumberOfColumns + j]);
        }
        set
        {
            if (i >= NumberOfRows || j >= NumberOfColumns || i < 0 || j < 0)
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
    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 0; i < NumberOfRows; i++)
        {

            for (int j = 0; j < NumberOfColumns; j++)
            {
                yield return this[i, j];
            }

        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public object Clone()
    {
        int[] newdata = new int[NumberOfColumns + NumberOfRows];
        for (int i = 0; i < newdata.Length; i++)
        {
            newdata[i] = BoolToBit(data[i]);
        }
        return new BitMatrix(this.NumberOfRows, this.NumberOfColumns, newdata);
    }
    public static BitMatrix Parse(string s)
    {
        if (s == null || s.Length == 0)
        { throw new ArgumentNullException(); }
        int rows, cols;
        List<int> newdata = new List<int>();
        string[] strings = s.Split(Environment.NewLine);
        rows = strings.Length;
        cols = strings[0].Length;
        for (int i = 0; i < rows - 1; i++)
        {
            if (strings[i].Length != strings[i + 1].Length)
            {
                throw new FormatException();
            }
        }
        foreach (string v in strings)
        {
            foreach (var c in v)
            {
                if (c != '1' && c != '0')
                {
                    throw new FormatException();
                }
                newdata.Add(Convert.ToInt32(c - '0'));
            }
        }
        return new BitMatrix(rows, cols, newdata.ToArray());
    }
    public static bool TryParse(string s, out BitMatrix result)
    {
        try { result = BitMatrix.Parse(s); return true; } catch { result = null; return false; }
    }
    public static explicit operator BitMatrix(int[,] arr)
    {
        return new BitMatrix(arr);
    }
    public static implicit operator int[,](BitMatrix matrix)
    {

        int[,] result = new int[matrix.NumberOfRows, matrix.NumberOfColumns];
        for (int i = 0; i < matrix.NumberOfRows; i++)
        {
            for (int j = 0; j < matrix.NumberOfColumns; j++)
            {
                result[i, j] = matrix[i, j];
            }
        }
        return result;
    }
    public static explicit operator BitMatrix(bool[,] arr)
    {  return new BitMatrix(arr); }
    public static implicit operator bool[,](BitMatrix matrix)
    {
        bool[,] result = new bool[matrix.NumberOfRows, matrix.NumberOfColumns];
        for (int i = 0; i < matrix.NumberOfRows; i++)
        {
            for (int j = 0; j < matrix.NumberOfColumns; j++)
            {
                result[i, j] = BitToBool (matrix[i, j]);
            }
        }
        return result;
    }
    public static explicit operator BitArray(BitMatrix matrix)
    {
        BitArray clonedData = (BitArray)matrix.data.Clone();
        return clonedData;
    }
    public BitMatrix And(BitMatrix other)
    {
        if(other == null)
        { throw new ArgumentNullException(); }
        if(other.NumberOfColumns != this.NumberOfColumns || other.NumberOfRows != this.NumberOfRows)
        { throw new ArgumentException(); }
        this.data.And(other.data);
        return this;
    }
    public BitMatrix Or(BitMatrix other)
    {
        if (other == null)
        { throw new ArgumentNullException(); }
        if (other.NumberOfColumns != this.NumberOfColumns || other.NumberOfRows != this.NumberOfRows)
        { throw new ArgumentException(); }
        this.data.Or(other.data);
        return this;
    }
    public BitMatrix Xor(BitMatrix other)
    {
        if (other == null)
        { throw new ArgumentNullException(); }
        if (other.NumberOfColumns != this.NumberOfColumns || other.NumberOfRows != this.NumberOfRows)
        { throw new ArgumentException(); }
        this.data.Xor(other.data);
        return this;
    }
    public BitMatrix Not() 
    { 
        this.data.Not();
        return this;
    }
    public static BitMatrix operator &(BitMatrix left, BitMatrix right)
    {
        if (left == null || right == null)
        { throw new ArgumentNullException(); }
        if (left.NumberOfColumns != right.NumberOfColumns || left.NumberOfRows != right.NumberOfRows)
        { throw new ArgumentException(); }
        BitMatrix leftclone = (BitMatrix)left.Clone();
        BitMatrix rightclone = (BitMatrix)right.Clone();
        leftclone.data.And(rightclone.data);
        return leftclone; 
    }
    public static BitMatrix operator ^(BitMatrix left, BitMatrix right)
    {
        if (left == null || right == null)
        { throw new ArgumentNullException(); }
        if (left.NumberOfColumns != right.NumberOfColumns || left.NumberOfRows != right.NumberOfRows)
        { throw new ArgumentException(); }
        BitMatrix leftclone = (BitMatrix)left.Clone();
        BitMatrix rightclone = (BitMatrix)right.Clone();
        leftclone.data.Xor(rightclone.data);
        return leftclone;
    }
    public static BitMatrix operator |(BitMatrix left, BitMatrix right)
    {
        if (left == null || right == null)
        { throw new ArgumentNullException(); }
        if (left.NumberOfColumns != right.NumberOfColumns || left.NumberOfRows != right.NumberOfRows)
        { throw new ArgumentException(); }
        BitMatrix leftclone = (BitMatrix)left.Clone();
        BitMatrix rightclone = (BitMatrix)right.Clone();
        leftclone.data.Or(rightclone.data);
        return leftclone;
    }
    public static BitMatrix operator !(BitMatrix left)
    {
        if (left == null)
        { throw new ArgumentNullException(); }
        BitMatrix leftclone = (BitMatrix)left.Clone();
        leftclone.data.Not();
        return leftclone;
    }

}