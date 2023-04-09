using System;

class Program
{
    static void Main(string[] args)
    {

        Random random = new Random();
        int randomNumber = random.Next(0, 5);

        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture("Genesis 1:1-2", "In the beginning, the Lord created the heaven and the earth. And the earth was without form, and void; and darkness was upon the face of the deep."),
            new Scripture("Psalm 24:7-10", "Lift up your heads, O ye gates; and be ye lift up, ye everlasting doors; and the King of glory shall come in. Who is this King of glory? The Lord strong and mighty, the Lord mighty in battle. Lift up your heads, O ye gates; even lift them up, ye everlasting doors; and the King of glory shall come in. Who is this King of glory? The Lord of hosts, he is the King of glory."),
            new Scripture("Isaiah 7:14", "Therefore, the Lord himself shall give you a sign—Behold, a virgin shall conceive, and shall bear a son, and shall call his name Immanuel."),
            new Scripture("Isaiah 53:3-5", "He is despised and rejected of men; a man of sorrows, and acquainted with grief; and we hid as it were our faces from him; he was despised, and we esteemed him not. Surely he has borne our griefs, and carried our sorrows; yet we did esteem him stricken, smitten of God, and afflicted. But he was wounded for our transgressions, he was bruised for our iniquities; the chastisement of our peace was upon him; and with his stripes we are healed."),
            new Scripture("Jeremiah 1:4-5", "Then the word of the Lord came unto me, saying, Before I formed thee in the belly I knew thee; and before thou camest forth out of the womb I sanctified thee, and I ordained thee a prophet unto the nations.")
        };

        Scripture scripture = scriptures[randomNumber];


        // Display the complete scripture and prompt the user to press "Enter" or type "quit".
        Console.Clear();
        Console.WriteLine(scripture.ToString());
        string input = UserInput.Prompt("Press Enter to begin, or type 'quit' to exit:");

        // Hide words one by one until all words are hidden or the user types "quit".
        while (!scripture.AllWordsHidden() && !input.Equals("quit"))
        {
            // Hide some random words and display the modified scripture.
            scripture.HideRandomWords(1);
            Console.Clear();
            Console.WriteLine(scripture.ToStringAllHidden());

            // Prompt the user to press "Enter" or type "quit".
            input = UserInput.Prompt("Press Enter to continue, or type 'quit' to exit:");
        }

        // Display the final scripture with all words hidden.
        Console.Clear();
        Console.WriteLine(scripture.ToStringAllHidden());
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }
}

class Scripture
{
    private string reference;
    private string text;
    private List<Word> words;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        this.words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string Reference { get { return reference; } }

    public string Text { get { return text; } }

    public void HideRandomWords(int count)
    {
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
        this.text = text;
        this.hidden = false;
    }

    public string Text { get { return text; } }

    public bool IsHidden() { return hidden; }

    public void Hide() { hidden = true; }

    public void Reveal() { hidden = false; }
}

class UserInput
{
    public static string Prompt(string message)
    {
        Console.Write(message + " ");
        return Console.ReadLine().Trim().ToLower();
    }
}
