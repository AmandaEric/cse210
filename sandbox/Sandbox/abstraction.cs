using System;
class OddNumberCounter
{
    List<int> numbers;

    OddNumberCounter(List<int> inputNumbers)
    {
        numbers = inputNumbers;
    }

    bool IsOdd(int number)
    {
        return number % 2 != 0;
    }

    int CountOdds()
    {
        int count = 0;
        foreach (int number in numbers)
        {
            if (IsOdd(number))
            {
                count++;
            }
        }
        return count;
    }
}