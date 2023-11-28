using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        foreach (var scripture in new List<Scripture>
        {
            new Scripture("Moses 1:39", "For behold, this is my work and my glory-to bring to pass the immortality and eternal life of man."),
            new Scripture("Moses 7:18", "And the Lord called his people Zion, because they were of one heart and one mind, and dwelt in righteousness; and there was no poor among them."),
            new Scripture("John 3:5", "Jesus answered, Verily, verily, I say unto thee, Except a man be born of water and of the Spirit, he cannot enter into the kingdom of God."),
            new Scripture("John 14:15", "If ye love me, keep my commandments."),
            new Scripture("Philippians 4:13", "I can do all things through Christ which strengtheneth me."),
            new Scripture("James 1:5", "If any of you lack wisdom, let him ask of God, that giveth to all men liberally, and upbraideth not; and it shall be given him."),
            new Scripture("Amos 3:7", "Surely the Lord God will do nothing, but he revealeth his secret unto his servants the prophets."),
            new Scripture("2 Nephi 2:25", "Adam fell that men might be; and men are, that they might have joy."),
            new Scripture("Mosiah 2:17", "And behold, I tell you these things that ye may learn wisdom; that ye may learn that when ye are in the service of your fellow beings ye are only in the service of your God."),
            new Scripture("2 Nephi 32:3", "Angels speak by the power of the Holy Ghost; wherefore, they speak the words of Christ. Wherefore, I said unto you, feast upon the words of Christ; for behold, the words of Christ will tell you all things what ye should do."),
            new Scripture("Alma 32:21", "And now as I said concerning faith—faith is not to have a perfect knowledge of things; therefore if ye have faith ye hope for things which are not seen, which are true."),
            new Scripture("Alma 37:35", "O, remember, my son, and learn wisdom in thy youth; yea, learn in thy youth to keep the commandments of God."),
            new Scripture("Moroni 10:5", "And by the power of the Holy Ghost ye may know the truth of all things."),
            new Scripture("Doctrine and Covenants 82:10", " I, the Lord, am bound when ye do what I say; but when ye do not what I say, ye have no promise.")
        })
        {
            PracticeScripture(scripture);
        }
    }

    static void PracticeScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine(scripture.GetFullScripture());
        Console.WriteLine(scripture.GetHiddenScripture());
        Console.WriteLine(scripture.GetChecklist());

        while (!scripture.AllWordsHidden && !scripture.IsMemorized())
        {
            Console.WriteLine("Press Enter to continue, type 'quit' to exit, or type 'memorized' to mark the entire scripture as memorized:");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
            {
                break;
            }
            else if (userInput.ToLower() == "memorized")
            {
                scripture.MarkAsMemorized();
                Console.Clear();
                Console.WriteLine("Congratulations! You've memorized the entire scripture.");
                break;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Invalid input. Please press Enter, type 'quit', or type 'memorized'.");
                    continue;
                }

                scripture.HideRandomWord();
                Console.Clear();
                Console.WriteLine(scripture.GetHiddenScripture());
                Console.WriteLine(scripture.GetChecklist());
            }
        }
    }
}

class Scripture
{
    private string reference;
    private List<Word> words;
    private Random random;
    private bool memorized;

    public bool AllWordsHidden => words.All(word => word.Hidden);
    public bool IsMemorized() => memorized;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        words = text.Split(' ').Select(word => new Word(word)).ToList();
        random = new Random();
    }

    public string GetFullScripture()
    {
        return $"{reference}\n\n{string.Join(" ", words.Select(w => w.Text))}";
    }

    public string GetHiddenScripture()
    {
        return $"{reference}\n\n{string.Join(" ", words.Select(w => w.Hidden ? "* * * * * * * " : w.Text))}";
    }

    public string GetChecklist()
    {
        return $"{reference}\n\n{string.Join(" ", words.Select(w => w.Hidden ? "* * * * * * * " : w.Text))}";
    }

    public void HideRandomWord()
    {
        List<Word> visibleWords = words.Where(word => !word.Hidden).ToList();
        if (visibleWords.Any())
        {
            Word wordToHide = visibleWords[random.Next(visibleWords.Count)];
            wordToHide.Hide();
        }
    }

    public void MarkAsMemorized()
    {
        memorized = true;
    }
}

class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }

    public void Hide()
    {
        Hidden = true;
    }
}
