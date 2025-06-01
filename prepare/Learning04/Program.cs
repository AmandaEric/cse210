using System;
using System.Collections.Generic;

public class ScriptureMemorizer
{
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
        private Random random = new Random();
        private List<Word> wordsList = new List<Word>();

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
                wordsList.Add(new Word(word));
            }
        }

        public void Hidder()
        {
            var hiddenCount = 0;
            while (hiddenCount < wordsList.Count)
            {
                int index = random.Next(wordsList.Count);
                if (!wordsList[index].IsHidden())
                {
                    wordsList[index].Hide();
                    hiddenCount++;
                    break;
                }
            }
        }

        public void Display()
        {
            foreach (var word in wordsList)
            {
                Console.Write(word.GetDisplay() + " ");
            }
            Console.WriteLine();
        }

        public bool AllWordsHidden()
        {
            foreach (var word in wordsList)
            {
                if (!word.IsHidden())
                    return false;
            }
            return true;
        }
    }

    public class Word
    {
        private string text;
        private bool isHidden;

        public Word(string word)
        {
            text = word;
            isHidden = false;
        }

        public void Hide()
        {
            isHidden = true;
        }

        public bool IsHidden()
        {
            return isHidden;
        }

        public string GetDisplay()
        {
            return isHidden ? new string('_', text.Length) : text;
        }
    }

    public static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, "16");
        Scripture scripture = new Scripture(reference,
            "For God so loved the world, that he gave his only Son, that whoever believes in him should not perish but have everlasting life.");

        scripture.Splitter();

        while (true)
        {

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



