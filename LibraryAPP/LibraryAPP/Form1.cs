namespace LibraryAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Library library = new Library();

            library.AddBook(new Book("Clean Code", "Robert C. Martin"));
            library.AddBook(new Book("1984", "George Orwell"));
            library.AddBook(new EBook("Digital Fortress", "Dan Brown", 5.2));

            library.SaveToFile("books.txt");
            library.LoadFromFile("books.txt");

            string output = "";

            foreach (Book book in library.GetBooks())
            {
                output += book.GetDescription() + Environment.NewLine;
            }

            MessageBox.Show(output);
        }
    }
}
