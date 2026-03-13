public class EBook : Book
{
    public double FileSizeMB { get; set; }

    public EBook(string title, string author, double fileSizeMB)
        : base(title, author)
    {
        FileSizeMB = fileSizeMB;
    }

    public override string GetDescription()
    {
        return $"E-Book: {Title} by {Author}, Size: {FileSizeMB} MB";
    }
}
