using System;
using System.Drawing;
using System.IO;
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
            dgvBooks.Columns.Clear();

            dgvBooks.ColumnCount = 3;
            dgvBooks.Columns[0].Name = "Title";
            dgvBooks.Columns[1].Name = "Author";
            dgvBooks.Columns[2].Name = "Availability";

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Selected";
            checkColumn.HeaderText = "✔";
            dgvBooks.Columns.Add(checkColumn);

            // UI fix: ei sinistä riviä
            dgvBooks.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvBooks.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvBooks.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvBooks.RowHeadersVisible = false;

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

        // ONLY checkbox toimii

        private void dgvBooks_CurrentCellDirtyStateChanged(object sender, EventArgs e) // Tämä varmistaa, että checkbox päivittyy heti yhdellä klikkauksella
        {
            if (dgvBooks.IsCurrentCellDirty) // Tarkistetaan onko nykyistä solua muutettu
            {
                dgvBooks.CommitEdit(DataGridViewDataErrorContexts.Commit); // Tallennetaan muutos heti
            }
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e) // Tämä suoritetaan vain kun klikataan solun sisältöä, esim. checkboxia
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3) // Tarkistetaan että klikattiin checkbox-saraketta
            {
                bool isChecked = false; // Luodaan muuttuja checkboxin tilaa varten

                if (dgvBooks.Rows[e.RowIndex].Cells[3].Value != null) // Jos checkbox-solussa on arvo
                {
                    isChecked = Convert.ToBoolean(dgvBooks.Rows[e.RowIndex].Cells[3].Value); // Luetaan checkboxin tila
                }

                if (isChecked) // Jos checkbox on valittu
                {
                    dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Borrowed"; // Muutetaan saatavuus lainatuksi
                }
                else // Jos checkbox ei ole valittu
                {
                    dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Available"; // Muutetaan saatavuus saatavilla olevaksi
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (searchText.Length < 3)
            {
                MessageBox.Show("Write at least 3 letters to search.");
                return;
            }

            foreach (DataGridViewRow row in dgvBooks.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    string title = row.Cells[0].Value.ToString().ToLower();
                    string author = row.Cells[1].Value.ToString().ToLower();

                    if (title.Contains(searchText) || author.Contains(searchText))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();

            if (title == "" || author == "")
            {
                MessageBox.Show("Please enter title and author.");
                return;
            }

            Book newBook = new Book(title, author);
            library.AddBook(newBook);

            dgvBooks.Rows.Add(newBook.Title, newBook.Author, newBook.Availability, false);

            txtTitle.Clear();
            txtAuthor.Clear();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            bool deletedAny = false;

            for (int i = dgvBooks.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dgvBooks.Rows[i];

                if (row.Cells[3].Value != null && Convert.ToBoolean(row.Cells[3].Value) == true)
                {
                    dgvBooks.Rows.RemoveAt(i);
                    deletedAny = true;
                }
            }

            if (deletedAny)
            {
                MessageBox.Show("Selected book(s) deleted.");
            }
            else
            {
                MessageBox.Show("No books selected.");
            }
        }

        private void btnSaveCatalog_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("catalog.txt"))
            {
                foreach (DataGridViewRow row in dgvBooks.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null &&
                        row.Cells[2].Value != null &&
                        row.Cells[3].Value != null)
                    {
                        string title = row.Cells[0].Value.ToString();
                        string author = row.Cells[1].Value.ToString();
                        string availability = row.Cells[2].Value.ToString();
                        string selected = row.Cells[3].Value.ToString();

                        writer.WriteLine($"{title};{author};{availability};{selected}");
                    }
                }
            }

            MessageBox.Show("Catalog saved.");
        }

        private void btnLoadCatalog_Click(object sender, EventArgs e)
        {
            if (!File.Exists("catalog.txt"))
            {
                MessageBox.Show("No saved catalog found.");
                return;
            }

            dgvBooks.Rows.Clear();

            string[] lines = File.ReadAllLines("catalog.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                if (parts.Length == 4)
                {
                    string title = parts[0];
                    string author = parts[1];
                    string availability = parts[2];
                    bool selected = bool.Parse(parts[3]);

                    dgvBooks.Rows.Add(title, author, availability, selected);
                }
            }

            MessageBox.Show("Catalog loaded.");
        }
    }
}