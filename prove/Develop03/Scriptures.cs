using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private string _reference;
    private string _text;
    private List<Word> _words;

    public Scripture(string reference, string text)
    {
        _reference = reference;
        _text = text;
        _words = _text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string GetReference()
    {
        return _reference;
    }

    public string GetText()
    {
        return _text;
    }

    public bool HideRandomWord()
    {
        var visibleWords = _words.Where(word => !word.IsHidden).ToList();
        if (visibleWords.Count == 0)
            return false;

        Random random = new Random();
        int index = random.Next(visibleWords.Count);
        visibleWords[index].Hide();
        return true;
    }

    public bool AllWordsHidden()
    {
        return _words.All(word => word.IsHidden);
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{_reference}:");
        foreach (var word in _words)
        {
            if (word.IsHidden)
                Console.Write("***** ");
            else
                Console.Write(word.Text + " ");
        }
        Console.WriteLine("\n\nPress Enter to continue or type 'quit' to exit.");
    }
}

public class Word
{
    public string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        while (!scripture.AllWordsHidden())
        {
            scripture.Display();

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            if (!scripture.HideRandomWord())
                break;
        }
    }
}
