using System;
using System.Collections.Generic;

public class Reference
{
    private string _book;
    private int _chapter;
    private string _verse;

    public Reference(string book, int chapter, string verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    public void Display()
    {
        Console.WriteLine($"{_book} {_chapter}:{_verse}");
    }
}

public class Scripture
{
    private Reference _reference;
    private string _scripture;
    private Random _random = new Random();
    private List<Word> _wordsList = new List<Word>();

    public Scripture(Reference reference, string scripture)
    {
        _reference = reference;
        _scripture = scripture;
    }

    public void Splitter()
    {
        string[] verse = _scripture.Split(' ');
        foreach (var word in verse)
        {
            _wordsList.Add(new Word(word));
        }
    }

    public void Hidder()
    {
        int index = _random.Next(_wordsList.Count);
        if (!_wordsList[index].IsHidden())
        {
            _wordsList[index].Hide();
        }
    }

    public void Display()
    {
        _reference.Display();
        foreach (var word in _wordsList)
        {
            Console.Write(word.GetDisplay() + " ");
        }
        Console.WriteLine();
    }

    public bool AllWordsHidden()
    {
        foreach (var word in _wordsList)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}

public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string word)
    {
        _text = word;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplay()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, "16");
        Scripture scripture = new Scripture(reference,
            "For God so loved the world, that he gave his only Son, that whoever believes in him should not perish but have everlasting life.");

        scripture.Splitter();

        while (true)
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine();
            Console.WriteLine("Press Enter to hide a word, or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit" || scripture.AllWordsHidden())
                break;

            scripture.Hidder();
        }
    }
}
