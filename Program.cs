using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


class Program
{
    static void Main(string[] args)
    {
        string filename = GetFileName();
        List<Scripture> scriptures = LoadScripturesFromFile(filename);

        Random random = new Random();
        int index = random.Next(scriptures.Count);
        Scripture scripture = scriptures[index];

        UserInterface.DisplayMainMenu();
        int difficulty = UserInterface.GetMenuOption() * 2;
        Thread.Sleep(100);

        // Display the complete scripture and prompt the user to press "Enter" or type "quit".
        Console.Clear();
        Console.WriteLine(scripture.ToString());
        string input = UserInterface.Prompt("Press Enter to begin, or type 'quit' to exit:");

        // Hide words one by one until all words are hidden or the user types "quit".
        while (!scripture.AllWordsHidden() && !input.Equals("quit"))
        {
            // Hide some random words and display the modified scripture.
            scripture.HideRandomWords(difficulty);
            Console.Clear();
            Console.WriteLine(scripture.ToStringAllHidden());

            if (scripture.AllWordsHidden())
            {
                // Display the final scripture with all words hidden.
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();
            }
            else
            {
                // Prompt the user to press "Enter" or type "quit".
                input = UserInterface.Prompt("Press Enter to continue, or type 'quit' to exit:");
            }
        }
    }

    public static string GetFileName()
    {
        string fileName = "";
        bool isValidFileName = false;

        while (!isValidFileName)
        {
            Console.Write("Enter the path to the JSON file containing the scriptures: ");
            fileName = Console.ReadLine();

            // Check if the file name is valid
            try
            {
                new FileInfo(fileName);
                isValidFileName = true;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid file name. Please enter a valid file name.");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("File path is too long. Please enter a shorter file name.");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Invalid file name. Please enter a valid file name.");
            }
        }

        return fileName;
    }

    public static List<Scripture> LoadScripturesFromFile(string filename)
    {
        List<Scripture> scriptures = new List<Scripture>();
        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string json = reader.ReadToEnd();
                scriptures = JsonSerializer.Deserialize<List<Scripture>>(json);
            }
            Console.WriteLine($"Scriptures loaded from file: {filename}");
        }
        catch
        {
            Console.WriteLine($"Error loading scriptures from file: {filename}");
        }
        return scriptures;
    }
}

class Scripture
{
    public string reference { get; set; }

    public string text { get; set; }

    private List<Word> words;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        this.words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        if (words.Count - CountWordsHidden() < count)
        {
            count = words.Count - CountWordsHidden();
        }

        int hidden = 0;
        Random rand = new Random();
        while (hidden < count)
        {
            int index = rand.Next(words.Count);
            if (!words[index].IsHidden())
            {
                words[index].Hide();
                hidden++;
            }
        }
    }

    public bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden());
    }

    public int CountWordsHidden()
    {
        int count = 0;
        foreach (Word word in words)
        {
            if (word.IsHidden())
            {
                count++;
            }
        }
        return count;
    }

    public override string ToString()
    {
        return $"{reference}\n{text}";
    }

    public string ToStringAllHidden()
    {
        return $"{reference}\n{string.Join(" ", words.Select(word => word.IsHidden() ? "****" : word.Text))}";
    }
}

class Word
{
    private string text;
    private bool hidden;

    public Word(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Text cannot be null or empty.", nameof(text));
        }
        this.text = text;
        this.hidden = false;
    }

    public string Text { get { return text; } }

    public bool IsHidden() { return hidden; }

    public void Hide() { hidden = true; }

    public void Reveal() { hidden = false; }
}


class UserInterface
{
    public static void DisplayMainMenu()
    {
        int DELAY = 50;

        Console.WriteLine("\nScripture Memorizer App");
        Thread.Sleep(DELAY);
        Console.WriteLine("=======================");
        Thread.Sleep(DELAY);
        Console.WriteLine("On a scale from 1 to 3, choose the level of difficulty for the game?");
        Console.WriteLine("Where 1 is easy, and 3 is hard.");
    }

    public static int GetMenuOption()
    {
        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            // check if input is a valid number between 1 and 3
            if (int.TryParse(input, out int number) && number >= 1 && number <= 3)
            {
                return int.Parse(input);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
            }
        }
    }

    public static string Prompt(string message)
    {
        Console.Write(message + " ");
        return Console.ReadLine().Trim().ToLower();
    }
}
