using System; // Perustoiminnot, esim. Convert
using System.Drawing; // Värit ja graafiset asetukset
using System.IO; // Tiedostojen käsittely
using System.Windows.Forms; // Windows Forms -käyttöliittymä

namespace LibraryAPP // Sovelluksen nimiavaruus
{
    public partial class Form1 : Form // Form1-lomake perii Form-luokan
    {
        Library library = new Library(); // Luodaan kirjasto-olio kirjojen säilyttämistä varten

        public Form1() // Lomakkeen konstruktori
        {
            InitializeComponent(); // Alustaa lomakkeen komponentit
        }

        private void Form1_Load(object sender, EventArgs e) // Tämä suoritetaan, kun lomake avautuu
        {
            dgvBooks.AllowUserToAddRows = false; // Estetään käyttäjää lisäämästä rivejä suoraan taulukkoon
            dgvBooks.Columns.Clear(); // Poistetaan mahdolliset vanhat sarakkeet

            dgvBooks.ColumnCount = 3; // Luodaan kolme perussaraketta
            dgvBooks.Columns[0].Name = "Title"; // Ensimmäinen sarake: kirjan nimi
            dgvBooks.Columns[1].Name = "Author"; // Toinen sarake: kirjoittaja
            dgvBooks.Columns[2].Name = "Availability"; // Kolmas sarake: saatavuus

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn(); // Luodaan checkbox-sarake
            checkColumn.Name = "Selected"; // Sarakkeen nimi
            checkColumn.HeaderText = "✔"; // Sarakkeen otsikko
            dgvBooks.Columns.Add(checkColumn); // Lisätään checkbox-sarake taulukkoon

            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sarakkeet täyttävät koko taulukon leveyden

            LoadBooks(); // Ladataan kirjat taulukkoon
        }

        private void LoadBooks() // Lisää valmiit kirjat kirjastoon ja taulukkoon
        {
            library.AddBook(new Book("The Name of the Wind", "Patrick Rothfuss")); // Lisätään tavallinen kirja
            library.AddBook(new Book("1984", "George Orwell")); // Lisätään tavallinen kirja
            library.AddBook(new Book("The Hobbit", "J.R.R. Tolkien")); // Lisätään tavallinen kirja
            library.AddBook(new Book("Dune", "Frank Herbert")); // Lisätään tavallinen kirja
            library.AddBook(new Book("Foundation", "Isaac Asimov")); // Lisätään tavallinen kirja

            library.AddBook(new EBook("Mistborn", "Brandon Sanderson", 4.5)); // Lisätään e-kirja
            library.AddBook(new EBook("The Way of Kings", "Brandon Sanderson", 6.1)); // Lisätään e-kirja
            library.AddBook(new EBook("Digital Fortress", "Dan Brown", 3.2)); // Lisätään e-kirja
            library.AddBook(new EBook("Snow Crash", "Neal Stephenson", 2.8)); // Lisätään e-kirja

            foreach (Book book in library.GetBooks()) // Käydään kaikki kirjat läpi
            {
                dgvBooks.Rows.Add(book.Title, book.Author, book.Availability, false); // Lisätään kirja taulukkoon
            }
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e) // Kun käyttäjä klikkaa taulukon solua
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3) // Tarkistetaan, että klikattiin checkbox-saraketta oikealla rivillä
            {
                bool currentValue = false; // Oletusarvo checkboxille

                if (dgvBooks.Rows[e.RowIndex].Cells[3].Value != null) // Jos solussa on arvo
                {
                    currentValue = Convert.ToBoolean(dgvBooks.Rows[e.RowIndex].Cells[3].Value); // Muunnetaan arvo true/false-muotoon
                }

                bool newValue = !currentValue; // Vaihdetaan checkboxin tila päinvastaiseksi
                dgvBooks.Rows[e.RowIndex].Cells[3].Value = newValue; // Tallennetaan uusi tila soluun

                if (newValue) // Jos checkbox valittiin
                {
                    dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Borrowed"; // Kirja merkitään lainatuksi
                }
                else // Jos checkbox poistettiin
                {
                    dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Available"; // Kirja merkitään saatavilla olevaksi
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) // Hakupainikkeen tapahtuma
        {
            string searchText = txtSearch.Text.Trim().ToLower(); // Luetaan hakuteksti, poistetaan välilyönnit ja muutetaan pieniksi kirjaimiksi

            if (searchText.Length < 3) // Tarkistetaan, että hakusana on tarpeeksi pitkä
            {
                MessageBox.Show("Write at least 3 letters to search."); // Näytetään virheilmoitus
                return; // Lopetetaan metodin suoritus
            }

            foreach (DataGridViewRow row in dgvBooks.Rows) // Käydään kaikki taulukon rivit läpi
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null) // Tarkistetaan, että nimi ja kirjoittaja ovat olemassa
                {
                    string title = row.Cells[0].Value.ToString().ToLower(); // Luetaan nimi pienillä kirjaimilla
                    string author = row.Cells[1].Value.ToString().ToLower(); // Luetaan kirjoittaja pienillä kirjaimilla

                    if (title.Contains(searchText) || author.Contains(searchText)) // Jos hakuteksti löytyy nimestä tai kirjoittajasta
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow; // Korostetaan rivi keltaiseksi
                    }
                    else // Jos hakua ei löydy
                    {
                        row.DefaultCellStyle.BackColor = Color.White; // Palautetaan rivin taustaväri valkoiseksi
                    }
                }
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e) // Lisää kirja -painikkeen tapahtuma
        {
            string title = txtTitle.Text.Trim(); // Luetaan kirjan nimi tekstikentästä
            string author = txtAuthor.Text.Trim(); // Luetaan kirjoittaja tekstikentästä

            if (title == "" || author == "") // Tarkistetaan, että molemmat kentät on täytetty
            {
                MessageBox.Show("Please enter title and author."); // Näytetään virheilmoitus
                return; // Lopetetaan metodin suoritus
            }

            Book newBook = new Book(title, author); // Luodaan uusi Book-olio
            library.AddBook(newBook); // Lisätään kirja kirjaston listaan

            dgvBooks.Rows.Add(newBook.Title, newBook.Author, newBook.Availability, false); // Lisätään kirja näkyviin taulukkoon

            txtTitle.Clear(); // Tyhjennetään title-kenttä
            txtAuthor.Clear(); // Tyhjennetään author-kenttä
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e) // Poista valitut -painikkeen tapahtuma
        {
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

            MessageBox.Show("Catalog saved to catalog.txt");
        }

        private void btnLoadCatalog_Click(object sender, EventArgs e)
        {
            if (!File.Exists("catalog.txt"))
            {
                MessageBox.Show("File not found.");
                return;
            }

            dgvBooks.Rows.Clear();

            foreach (string line in File.ReadAllLines("catalog.txt"))
            {
                string[] parts = line.Split(';');

                if (parts.Length == 4)
                {
                    bool selected = false;
                    bool.TryParse(parts[3], out selected);

                    dgvBooks.Rows.Add(parts[0], parts[1], parts[2], selected);
                }
            }

            MessageBox.Show("Catalog loaded.");
        }
    }
}