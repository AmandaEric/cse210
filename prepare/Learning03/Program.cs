using System;

// class Program
// { }
    public class Fraction
    {
        private int _top;
        private int _bottom;

    // public Fraction(int top, int bottom)
    // {
    //     _top = top;
    //     _bottom = bottom;
    // }
    public Fraction(int top, int bottom)
    {
        if (bottom == 0)
        {
            Console.WriteLine("Denominator cannot be zero.");
        }

        this._top = top;
        this._bottom = bottom;
    }
    public int GetTop()
    {
        return _top;
    }
    public int GetBottom()
    {
        return _bottom;
    }
    public void SetTop(int value)
    {
        _top = value;
    }

    // 📤 Setter for denominator
    public void SetBottom(int value)
    {
        if (value == 0)
        {
             Console.WriteLine("Denominator cannot be zero.");
        }
        _bottom = value;
    }
    public string GetFormat()
    {
        return $"{_top}/{_bottom}";
    }
    }


