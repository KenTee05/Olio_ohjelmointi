using System;
using System.Windows.Forms;

namespace LibraryAPP
{
    public partial class Form1 : Form
    {
        Library library = new Library();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvBooks.AllowUserToAddRows = false;

            dgvBooks.ColumnCount = 3;

            dgvBooks.Columns[0].Name = "Title";
            dgvBooks.Columns[1].Name = "Author";
            dgvBooks.Columns[2].Name = "Availability";

            // Lisätään checkbox sarake
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Selected";
            checkColumn.HeaderText = "✔";

            dgvBooks.Columns.Add(checkColumn);

            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadBooks();
        }

        private void LoadBooks()
        {
            library.AddBook(new Book("The Name of the Wind", "Patrick Rothfuss"));
            library.AddBook(new Book("1984", "George Orwell"));
            library.AddBook(new Book("The Hobbit", "J.R.R. Tolkien"));
            library.AddBook(new Book("Dune", "Frank Herbert"));
            library.AddBook(new Book("Foundation", "Isaac Asimov"));

            library.AddBook(new EBook("Mistborn", "Brandon Sanderson", 4.5));
            library.AddBook(new EBook("The Way of Kings", "Brandon Sanderson", 6.1));
            library.AddBook(new EBook("Digital Fortress", "Dan Brown", 3.2));
            library.AddBook(new EBook("Snow Crash", "Neal Stephenson", 2.8));

            foreach (Book book in library.GetBooks())
            {
                dgvBooks.Rows.Add(book.Title, book.Author, book.Availability, false);
            }
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                bool currentValue = Convert.ToBoolean(dgvBooks.Rows[e.RowIndex].Cells[3].Value);
                dgvBooks.Rows[e.RowIndex].Cells[3].Value = !currentValue;
            }
        }
    }
}