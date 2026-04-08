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

            dgvBooks.SelectionMode = DataGridViewSelectionMode.CellSelect; // Valitaan yksi solu kerrallaan
            dgvBooks.DefaultCellStyle.SelectionBackColor = Color.White; // Estetään sininen valintaväri
            dgvBooks.DefaultCellStyle.SelectionForeColor = Color.Black; // Teksti pysyy mustana valittaessa
            dgvBooks.RowHeadersVisible = false; // Piilotetaan vasemman reunan riviotsikot
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sarakkeet täyttävät koko taulukon

            txtFileSize.Enabled = false; // FileSize-kenttä pois käytöstä alussa

            LoadBooks(); // Ladataan valmiit kirjat
            UpdateStatistics(); // Päivitetään tilastot näkyviin
        }

        private void LoadBooks() // Lisää valmiit kirjat kirjastoon ja taulukkoon
        {
            library.ClearBooks(); // Tyhjennetään lista ennen lisäämistä
            dgvBooks.Rows.Clear(); // Tyhjennetään taulukko ennen lisäämistä

            library.AddBook(new Book("The Name of the Wind", "Patrick Rothfuss")); // Lisätään tavallinen kirja
            library.AddBook(new Book("1984", "George Orwell")); // Lisätään tavallinen kirja
            library.AddBook(new Book("The Hobbit", "J.R.R. Tolkien")); // Lisätään tavallinen kirja
            library.AddBook(new Book("Dune", "Frank Herbert")); // Lisätään tavallinen kirja
            library.AddBook(new Book("Foundation", "Isaac Asimov")); // Lisätään tavallinen kirja

            library.AddBook(new EBook("Mistborn", "Brandon Sanderson", 4.5)); // Lisätään e-kirja
            library.AddBook(new EBook("The Way of Kings", "Brandon Sanderson", 6.1)); // Lisätään e-kirja
            library.AddBook(new EBook("Digital Fortress", "Dan Brown", 3.2)); // Lisätään e-kirja
            library.AddBook(new EBook("Snow Crash", "Neal Stephenson", 2.8)); // Lisätään e-kirja

            foreach (IReadable item in library.GetBooks()) // Käydään kirjat läpi rajapinnan avulla
            {
                Book book = (Book)item; // Muutetaan item takaisin Book-tyypiksi
                dgvBooks.Rows.Add(book.Title, book.Author, book.Availability, false); // Lisätään kirja taulukkoon
            }
        }

        private void chkEBook_CheckedChanged(object sender, EventArgs e) // Tämä suoritetaan kun E-Book-valinta muuttuu
        {
            txtFileSize.Enabled = chkEBook.Checked; // FileSize-kenttä on käytössä vain jos E-Book on valittu
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
                    if (isEBook) // Jos kyseessä on e-kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Borrowed (E-Book)"; // E-kirja merkitään lainatuksi
                    }
                    else // Jos kyseessä on tavallinen kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Borrowed"; // Tavallinen kirja merkitään lainatuksi
                    }
                }
                else // Jos checkbox poistetaan
                {
                    if (isEBook) // Jos kyseessä on e-kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Available (E-Book)"; // E-kirja palautetaan saataville
                    }
                    else // Jos kyseessä on tavallinen kirja
                    {
                        dgvBooks.Rows[e.RowIndex].Cells[2].Value = "Available"; // Tavallinen kirja palautetaan saataville
                    }
                }

                UpdateStatistics(); // Päivitetään tilastot muutoksen jälkeen
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) // Tämä suoritetaan aina kun hakukentän teksti muuttuu
        {
            string searchText = txtSearch.Text.Trim().ToLower(); // Luetaan hakukentän teksti pieniksi kirjaimiksi

            foreach (DataGridViewRow row in dgvBooks.Rows) // Käydään kaikki rivit läpi
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null) // Tarkistetaan että Title ja Author ovat olemassa
                {
                    string title = row.Cells[0].Value.ToString().ToLower(); // Luetaan kirjan nimi pienillä kirjaimilla
                    string author = row.Cells[1].Value.ToString().ToLower(); // Luetaan kirjailija pienillä kirjaimilla

                    if (searchText.Length >= 3 && (title.Contains(searchText) || author.Contains(searchText))) // Jos hakuteksti löytyy nimestä tai kirjailijasta
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow; // Korostetaan rivi keltaiseksi
                    }
                    else // Jos hakua ei löydy
                    {
                        row.DefaultCellStyle.BackColor = Color.White; // Palautetaan rivi valkoiseksi
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) // Search-painikkeen tapahtuma
        {
            txtSearch_TextChanged(sender, e); // Käytetään samaa logiikkaa kuin live searchissa
        }

        private void btnAddBook_Click(object sender, EventArgs e) // Lisää kirja tai e-kirja -painikkeen tapahtuma
        {
            string title = txtTitle.Text.Trim(); // Luetaan kirjan nimi tekstikentästä
            string author = txtAuthor.Text.Trim(); // Luetaan kirjoittaja tekstikentästä

            if (title == "" || author == "") // Tarkistetaan että nimi ja kirjoittaja on annettu
            {
                MessageBox.Show("Please enter title and author."); // Näytetään virheilmoitus
                return; // Lopetetaan metodin suoritus
            }

            if (chkEBook.Checked) // Jos käyttäjä valitsi E-Book vaihtoehdon
            {
                double fileSize; // Muuttuja tiedostokoolle

                if (!double.TryParse(txtFileSize.Text.Trim(), out fileSize)) // Tarkistetaan että koko on oikea numero
                {
                    MessageBox.Show("Please enter a valid file size for the E-Book."); // Näytetään virheilmoitus
                    return; // Lopetetaan metodin suoritus
                }

                EBook newEBook = new EBook(title, author, fileSize); // Luodaan uusi EBook-olio
                library.AddBook(newEBook); // Lisätään e-kirja kirjaston listaan
                dgvBooks.Rows.Add(newEBook.Title, newEBook.Author, newEBook.Availability, false); // Lisätään e-kirja taulukkoon
            }
            else // Jos käyttäjä lisää tavallisen kirjan
            {
                Book newBook = new Book(title, author); // Luodaan uusi Book-olio
                library.AddBook(newBook); // Lisätään kirja kirjaston listaan
                dgvBooks.Rows.Add(newBook.Title, newBook.Author, newBook.Availability, false); // Lisätään kirja taulukkoon
            }

            txtTitle.Clear(); // Tyhjennetään title-kenttä
            txtAuthor.Clear(); // Tyhjennetään author-kenttä
            txtFileSize.Clear(); // Tyhjennetään file size -kenttä
            chkEBook.Checked = false; // Poistetaan E-Book-valinta

            UpdateStatistics(); // Päivitetään tilastot lisäyksen jälkeen
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e) // Poista valitut -painikkeen tapahtuma
        {
            bool anySelected = false; // Muuttuja kertoo onko mitään valittu

            for (int i = 0; i < dgvBooks.Rows.Count; i++) // Käydään kaikki rivit läpi
            {
                if (dgvBooks.Rows[i].Cells[3].Value != null && Convert.ToBoolean(dgvBooks.Rows[i].Cells[3].Value) == true) // Jos checkbox on valittu
                {
                    anySelected = true; // Merkitään että löytyi valittu rivi
                    break; // Lopetetaan silmukka
                }
            }

            if (!anySelected) // Jos mitään ei ole valittu
            {
                MessageBox.Show("No books selected."); // Näytetään ilmoitus
                return; // Lopetetaan metodin suoritus
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete selected book(s)?", // Kysymys käyttäjälle
                "Confirm delete", // Ikkunan otsikko
                MessageBoxButtons.YesNo, // Kyllä / ei -painikkeet
                MessageBoxIcon.Question // Kysymysmerkki-ikoni
            );

            if (result == DialogResult.No) // Jos käyttäjä valitsee No
            {
                return; // Lopetetaan metodin suoritus
            }

            bool deletedAny = false; // Muuttuja kertoo poistettiinko rivejä

            for (int i = dgvBooks.Rows.Count - 1; i >= 0; i--) // Käydään rivit läpi lopusta alkuun
            {
                DataGridViewRow row = dgvBooks.Rows[i]; // Otetaan yksi rivi käsittelyyn

                if (row.Cells[3].Value != null && Convert.ToBoolean(row.Cells[3].Value) == true) // Jos checkbox on valittu
                {
                    dgvBooks.Rows.RemoveAt(i); // Poistetaan rivi taulukosta
                    deletedAny = true; // Merkitään että ainakin yksi poistettiin
                }
            }

            if (deletedAny) // Jos rivejä poistettiin
            {
                MessageBox.Show("Selected book(s) deleted."); // Näytetään onnistumisviesti
                UpdateStatistics(); // Päivitetään tilastot poistamisen jälkeen
            }
        }

        private void UpdateStatistics() // Päivittää näkyviin kirjaston tilastot
        {
            int totalBooks = dgvBooks.Rows.Count; // Lasketaan kaikkien rivien määrä
            int availableBooks = 0; // Muuttuja saatavilla olevien kirjojen määrälle
            int borrowedBooks = 0; // Muuttuja lainattujen kirjojen määrälle
            int eBooks = 0; // Muuttuja e-kirjojen määrälle

            foreach (DataGridViewRow row in dgvBooks.Rows) // Käydään kaikki rivit läpi
            {
                if (row.Cells[2].Value != null) // Tarkistetaan että Availability-sarakkeessa on arvo
                {
                    string availability = row.Cells[2].Value.ToString(); // Luetaan saatavuus

                    if (availability.StartsWith("Available")) // Jos kirja on saatavilla
                    {
                        availableBooks++; // Lisätään saatavilla olevien määrää
                    }

                    if (availability.StartsWith("Borrowed")) // Jos kirja on lainassa
                    {
                        borrowedBooks++; // Lisätään lainattujen määrää
                    }

                    if (availability.Contains("E-Book")) // Jos kyseessä on e-kirja
                    {
                        eBooks++; // Lisätään e-kirjojen määrää
                    }
                }
            }

            lblTotalBooks.Text = $"Total books: {totalBooks}"; // Päivitetään kaikkien kirjojen määrä
            lblAvailableBooks.Text = $"Available: {availableBooks}"; // Päivitetään saatavilla olevien määrä
            lblBorrowedBooks.Text = $"Borrowed: {borrowedBooks}"; // Päivitetään lainattujen määrä
            lblEBooks.Text = $"EBooks: {eBooks}"; // Päivitetään e-kirjojen määrä
        }

        private void btnSaveCatalog_Click(object sender, EventArgs e) // Tallenna katalogi tiedostoon
        {
            dgvBooks.EndEdit(); // Varmistetaan että viimeisin checkbox-muutos tallentuu

            using (StreamWriter writer = new StreamWriter("catalog.txt")) // Luodaan kirjoittaja tiedostoon
            {
                foreach (DataGridViewRow row in dgvBooks.Rows) // Käydään kaikki rivit läpi
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null &&
                        row.Cells[2].Value != null &&
                        row.Cells[3].Value != null) // Tarkistetaan että rivillä on kaikki arvot
                    {
                        string title = row.Cells[0].Value.ToString(); // Haetaan kirjan nimi
                        string author = row.Cells[1].Value.ToString(); // Haetaan kirjailija
                        string availability = row.Cells[2].Value.ToString(); // Haetaan saatavuus
                        string selected = row.Cells[3].Value.ToString(); // Haetaan checkboxin tila

                        writer.WriteLine($"{title};{author};{availability};{selected}"); // Kirjoitetaan tiedot yhdelle riville
                    }
                }
            }

            MessageBox.Show("Catalog saved to catalog.txt"); // Näytetään tallennusviesti
        }

        private void btnLoadCatalog_Click(object sender, EventArgs e) // Lataa katalogi tiedostosta
        {
            if (!File.Exists("catalog.txt")) // Tarkistetaan löytyykö tiedosto
            {
                MessageBox.Show("File not found."); // Näytetään virheilmoitus
                return; // Lopetetaan metodin suoritus
            }

            dgvBooks.Rows.Clear(); // Tyhjennetään taulukko ennen lataamista
            library.ClearBooks(); // Tyhjennetään myös kirjaston lista

            foreach (string line in File.ReadAllLines("catalog.txt")) // Luetaan kaikki tiedoston rivit
            {
                string[] parts = line.Split(';'); // Jaetaan rivi osiin puolipisteen kohdalta

                if (parts.Length == 4) // Varmistetaan että rivillä on 4 osaa
                {
                    bool selected = false; // Muuttuja checkboxin tilalle
                    bool.TryParse(parts[3], out selected); // Muunnetaan true/false booleaniksi

                    string title = parts[0]; // Luetaan title
                    string author = parts[1]; // Luetaan author
                    string availability = parts[2]; // Luetaan availability

                    if (availability.Contains("E-Book")) // Jos kyseessä on e-kirja
                    {
                        EBook ebook = new EBook(title, author, 0); // Luodaan e-kirja väliaikaisesti koolla 0
                        ebook.Availability = availability; // Palautetaan tallennettu saatavuus
                        library.AddBook(ebook); // Lisätään e-kirja kirjaston listaan
                    }
                    else // Jos kyseessä on tavallinen kirja
                    {
                        Book book = new Book(title, author); // Luodaan tavallinen kirja
                        book.Availability = availability; // Palautetaan tallennettu saatavuus
                        library.AddBook(book); // Lisätään kirja kirjaston listaan
                    }

                    dgvBooks.Rows.Add(title, author, availability, selected); // Lisätään tiedot taulukkoon
                }
            }

            UpdateStatistics(); // Päivitetään tilastot latauksen jälkeen
            MessageBox.Show("Catalog loaded."); // Näytetään latausviesti
        }

        private void dgvBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // Tämä suoritetaan kun käyttäjä tuplaklikkaa riviä
        {
            if (e.RowIndex >= 0 && e.RowIndex < library.GetBooks().Count) // Tarkistetaan että klikattu rivi on olemassa
            {
                IReadable readable = library.GetBooks()[e.RowIndex]; // Haetaan kirja rajapinnan kautta
                MessageBox.Show(readable.GetDescription(), "Book Info"); // Näytetään kirjan kuvaus
            }
        }
    }
}