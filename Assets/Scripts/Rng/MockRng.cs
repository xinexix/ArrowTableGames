using System;

/// <summary>
/// Getting random integers and getting random double floating-point numbers are considered
/// independent, therefore mock sequences can be established for both independently.  Similarly,
/// the very next int or double to return can be controlled independently.
///
/// The difference between the next random and a sequence is the next value is returned and then
/// cleared such that subsequent random requests will come from elsewhere, whereas sequences
/// are iterated and looped until manually cleared.
///
/// When both the next random and the mock sequence are cleared then random requests come from
/// the system RNG.
/// </summary>
public class MockRng : IRng
{
    private Random _random;
    private int? _nextInt;
    private int[] _intSequence;
    private int _intSequenceIndex;
    private double? _nextDouble;
    private double[] _doubleSequence;
    private int _doubleSequenceIndex;

    public MockRng()
    {
        _random = new Random();
    }

    public void setSeed(int? seedValue)
    {
        _random = new Random(seedValue);
    }

    public int randomIntRange(int min, int max)
    {
        if (_nextInt.HasValue)
        {
            var temp = _nextInt.Value;
            _nextInt = null;
            return temp;
        }

        if (_intSequence != null)
        {
            _intSequenceIndex %= _intSequence.Length;

            return _intSequence[_intSequenceIndex++];
        }

        return _random.Next(min, max + 1);
    }

    public double randomDouble()
    {
        if (_nextDouble.HasValue)
        {
            var temp = _nextDouble.Value;
            _nextDouble = null;
            return temp;
        }

        if (_doubleSequence != null)
        {
            _doubleSequenceIndex %= _doubleSequence.Length;

            return _doubleSequence[_doubleSequenceIndex++];
        }

        return _random.NextDouble();
    }

    public void setNextInt(int value)
    {
        _nextInt = value;
    }

    public void setIntSequence(int[] values)
    {
        if (values == null || values.Length == 0)
        {
            _intSequence = null;
            return;
        }

        _intSequence = values;
        _intSequenceIndex = 0;
    }

    public void clearMockInts()
    {
        _nextint = null;
        _intSequence = null;
    }

    public void setNextDouble(double value)
    {
        _nextDouble = value;
    }

    public void setDoubleSequence(double[] values)
    {
        if (values == null || values.Length == 0)
        {
            _doubleSequence = null;
            return;
        }

        _doubleSequence = values;
        _doubleSequenceIndex = 0;
    }

    public void clearMockDoubles()
    {
        _nextDouble = null;
        _doubleSequence = null;
    }
}
