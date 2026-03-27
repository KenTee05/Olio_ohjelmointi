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

            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sarakkeet täyttävät koko taulukon leveyden
            txtFileSize.Enabled = false; // FileSize-kenttä pois käytöstä alussa

            LoadBooks(); // Ladataan kirjat taulukkoon
            UpdateStatistics(); // Päivitetään tilastot näkyviin
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

        private void UpdateStatistics() // Päivittää näkyviin kirjaston tilastot
        {
            int totalBooks = dgvBooks.Rows.Count; // Lasketaan kaikkien rivien määrä
            int availableBooks = 0; // Muuttuja saatavilla olevien kirjojen määrälle
            int borrowedBooks = 0; // Muuttuja lainattujen kirjojen määrälle
            int eBooks = 0; // Muuttuja e-kirjojen määrälle

            foreach (DataGridViewRow row in dgvBooks.Rows) // Käydään kaikki taulukon rivit läpi
            {
                if (row.Cells[2].Value != null) // Tarkistetaan että Availability-sarakkeessa on arvo
                {
                    string availability = row.Cells[2].Value.ToString(); // Luetaan saatavuustieto

                    if (availability == "Available" || availability == "Available (E-Book)") // Jos kirja on saatavilla
                    {
                        availableBooks++; // Lisätään saatavilla olevien määrää
                    }

                    if (availability == "Borrowed") // Jos kirja on lainassa
                    {
                        borrowedBooks++; // Lisätään lainattujen määrää
                    }

                    if (availability.Contains("E-Book")) // Jos kyseessä on e-kirja
                    {
                        eBooks++; // Lisätään e-kirjojen määrää
                    }
                }
            }

            lblTotalBooks.Text = $"Total books: {totalBooks}"; // Päivitetään kaikkien kirjojen määrä labeliin
            lblAvailableBooks.Text = $"Available: {availableBooks}"; // Päivitetään saatavilla olevien määrä labeliin
            lblBorrowedBooks.Text = $"Borrowed: {borrowedBooks}"; // Päivitetään lainattujen määrä labeliin
            lblEBooks.Text = $"EBooks: {eBooks}"; // Päivitetään e-kirjojen määrä labeliin
        }



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

                string currentAvailability = dgvBooks.Rows[e.RowIndex].Cells[2].Value.ToString(); // Luetaan nykyinen saatavuustieto
                bool isEBook = currentAvailability.Contains("E-Book"); // Tarkistetaan onko kyseessä e-kirja

                if (isChecked) // Jos checkbox on valittu
                {
                    if (isEBook) // Jos kirja on e-kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Borrowed (E-Book)"; // E-kirja merkitään lainatuksi
                    }
                    else // Jos kirja on tavallinen kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Borrowed"; // Tavallinen kirja merkitään lainatuksi
                    }
                }
                else // Jos checkbox poistetaan
                {
                    if (isEBook) // Jos kirja on e-kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Available (E-Book)"; // E-kirja palautetaan saataville
                    }
                    else // Jos kirja on tavallinen kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Available"; // Tavallinen kirja palautetaan saataville
                    }
                }

                UpdateStatistics(); // Päivitetään tilastot muutoksen jälkeen
            }
        }



        private void btnAddBook_Click(object sender, EventArgs e) // Lisää kirja tai e-kirja
        {
            string title = txtTitle.Text.Trim(); // Luetaan kirjan nimi
            string author = txtAuthor.Text.Trim(); // Luetaan kirjailija

            if (title == "" || author == "") // Tarkistetaan että nimi ja kirjailija on syötetty
            {
                MessageBox.Show("Please enter title and author."); // Näytetään virheilmoitus
                return; // Lopetetaan suoritus
            }

            if (chkEBook.Checked) // Jos E-Book on valittu
            {
                double fileSize; // Muuttuja tiedostokoolle

                if (!double.TryParse(txtFileSize.Text.Trim(), out fileSize)) // Tarkistetaan että koko on kelvollinen numero
                {
                    MessageBox.Show("Please enter a valid file size for the E-Book."); // Näytetään virheilmoitus
                    return; // Lopetetaan suoritus
                }

                EBook newEBook = new EBook(title, author, fileSize); // Luodaan uusi EBook-olio
                library.AddBook(newEBook); // Lisätään e-kirja kirjastoon
                dgvBooks.Rows.Add(newEBook.Title, newEBook.Author, newEBook.Availability, false); // Lisätään e-kirja taulukkoon
            }
            else // Muuten lisätään tavallinen kirja
            {
                Book newBook = new Book(title, author); // Luodaan uusi Book-olio
                library.AddBook(newBook); // Lisätään tavallinen kirja kirjastoon
                dgvBooks.Rows.Add(newBook.Title, newBook.Author, newBook.Availability, false); // Lisätään tavallinen kirja taulukkoon
            }

            txtTitle.Clear(); // Tyhjennetään title-kenttä
            txtAuthor.Clear(); // Tyhjennetään author-kenttä
            txtFileSize.Clear(); // Tyhjennetään filesize-kenttä
            chkEBook.Checked = false; // Poistetaan E-Book-valinta

            UpdateStatistics(); // Päivitetään tilastot
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e) // Poista valitut -painikkeen tapahtuma
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete selected book(s)?", // Kysymys käyttäjälle
                "Confirm delete", // Ikkunan otsikko
                MessageBoxButtons.YesNo, // Näytetään Yes ja No painikkeet
                MessageBoxIcon.Question // Näytetään kysymysmerkki-ikoni
            );

            if (result == DialogResult.No) // Jos käyttäjä valitsee No
            {
                return; // Lopetetaan metodi, eikä poisteta mitään
            }

            bool deletedAny = false; // Muuttuja kertoo poistettiinko yhtään riviä

            for (int i = dgvBooks.Rows.Count - 1; i >= 0; i--) // Käydään rivit läpi lopusta alkuun
            {
                DataGridViewRow row = dgvBooks.Rows[i]; // Otetaan yksi rivi käsittelyyn

                if (row.Cells[3].Value != null && Convert.ToBoolean(row.Cells[3].Value) == true) // Jos checkbox on valittu
                {
                    dgvBooks.Rows.RemoveAt(i); // Poistetaan rivi taulukosta
                    deletedAny = true; // Merkitään että ainakin yksi poistettiin
                }
            }

            if (deletedAny) // Jos poistettiin vähintään yksi rivi
            {
                MessageBox.Show("Selected book(s) deleted."); // Näytetään onnistumisviesti
                UpdateStatistics();
            }
            else // Jos mitään ei poistettu
            {
                MessageBox.Show("No books selected."); // Näytetään ilmoitus
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
            UpdateStatistics();
            MessageBox.Show("Catalog loaded.");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) // Tämä suoritetaan aina kun hakukentän teksti muuttuu
        {
            string searchText = txtSearch.Text.Trim().ToLower(); // Luetaan hakukentän sisältö, poistetaan ylimääräiset välilyönnit ja muutetaan pieniksi kirjaimiksi

            foreach (DataGridViewRow row in dgvBooks.Rows) // Käydään kaikki taulukon rivit läpi
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null) // Tarkistetaan että Title- ja Author-soluissa on arvo
                {
                    string title = row.Cells[0].Value.ToString().ToLower(); // Luetaan kirjan nimi pienillä kirjaimilla
                    string author = row.Cells[1].Value.ToString().ToLower(); // Luetaan kirjailija pienillä kirjaimilla

                    if (searchText.Length >= 3 && (title.Contains(searchText) || author.Contains(searchText))) // Jos hakuteksti on vähintään 3 merkkiä ja löytyy nimestä tai kirjailijasta
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow; // Korostetaan löytynyt rivi keltaiseksi
                    }
                    else // Jos hakua ei löydy tai hakuteksti on liian lyhyt
                    {
                        row.DefaultCellStyle.BackColor = Color.White; // Palautetaan rivin normaali taustaväri
                    }
                }
            }
        }

        private void chkEBook_CheckedChanged(object sender, EventArgs e)
        {
            txtFileSize.Enabled = chkEBook.Checked; // FileSize-kenttä käytössä vain jos E-Book on valittu
        }
    }
}