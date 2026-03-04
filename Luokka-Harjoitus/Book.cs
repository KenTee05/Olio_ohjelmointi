public class Book : IReadable
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book() { }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public override string ToString()
    {
        return $"{Title};{Author}";
    }
    public virtual string GetDescription()
    {
        return $"Book: {Title} by {Author}";
    }
}
