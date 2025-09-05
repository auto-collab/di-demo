using demo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register the logging filter globally
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new DemoLoggingFilter());
});

builder.Services.AddSingleton<IDictionaryService, DictionarySevice>();

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.MapControllers();

app.Run();


// Service to be used for dependency injection
public interface IDictionaryService
{
    Boolean AddWord(string word, string defintion);
    Boolean RemoveWord(string word);
    string GetDefintion(string word);
}

public class DictionarySevice : IDictionaryService
{
    private readonly Dictionary<string, string> Words = new()
    {
        {"cheese", "A dairy product made from curdled or cultured milk." },
        {"apple", "A round fruit of a tree of the rose family." },
        {"banana", "A long curved fruit that grows in clusters and has soft pulpy flesh and yellow skin when ripe." }
    };

    public Boolean AddWord(string word, string definition)
    {
        if (!WordExists(word))
        {
            Words.Add(word, definition);
            return true;
        }
        else
        {
            return false;
        }
    }

    public string GetDefintion(string word)
    {
        return Words.GetValueOrDefault(word, "Word not found");
    }

    public Boolean RemoveWord(string word)
    {
        if (WordExists(word))
        {
            Words.Remove(word);
            return true;
        }
        else
        {
            Console.WriteLine($"{word} does not exist in dictionary!");
            return false;
        }
    }

    private Boolean WordExists(string word)
    {
        return Words.ContainsKey(word);
    }

}
