namespace LibraryAPP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Button btnAddBook;
            lblTitle = new Label();
            dgvBooks = new DataGridView();
            txtSearch = new TextBox();
            btnSeach = new Button();
            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            label1 = new Label();
            label2 = new Label();
            btnDeleteSelected = new Button();
            btnSaveCatalog = new Button();
            btnLoadCatalog = new Button();
            btnAddBook = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();
            // 
            // btnAddBook
            // 
            btnAddBook.Location = new Point(486, 49);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.Size = new Size(94, 29);
            btnAddBook.TabIndex = 7;
            btnAddBook.Text = "Add Book";
            btnAddBook.UseVisualStyleBackColor = true;
            btnAddBook.Click += btnAddBook_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial Rounded MT Bold", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(309, 39);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Library catalogue";
            // 
            // dgvBooks
            // 
            dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBooks.Location = new Point(0, 85);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.RowHeadersWidth = 51;
            dgvBooks.Size = new Size(800, 365);
            dgvBooks.TabIndex = 2;
            dgvBooks.CellContentClick += dgvBooks_CellContentClick;
            dgvBooks.CurrentCellDirtyStateChanged += dgvBooks_CurrentCellDirtyStateChanged;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(0, 42);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(125, 27);
            txtSearch.TabIndex = 3;
            // 
            // btnSeach
            // 
            btnSeach.Location = new Point(131, 40);
            btnSeach.Name = "btnSeach";
            btnSeach.Size = new Size(94, 29);
            btnSeach.TabIndex = 4;
            btnSeach.Text = "Search";
            btnSeach.UseVisualStyleBackColor = true;
            btnSeach.Click += btnSearch_Click;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(355, 12);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(125, 27);
            txtTitle.TabIndex = 5;
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(355, 45);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(125, 27);
            txtAuthor.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(315, 15);
            label1.Name = "label1";
            label1.Size = new Size(38, 20);
            label1.TabIndex = 8;
            label1.Text = "Title";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(299, 49);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 9;
            label2.Text = "Author";
            // 
            // btnDeleteSelected
            // 
            btnDeleteSelected.Location = new Point(541, 4);
            btnDeleteSelected.Name = "btnDeleteSelected";
            btnDeleteSelected.Size = new Size(124, 39);
            btnDeleteSelected.TabIndex = 10;
            btnDeleteSelected.Text = "Delete Selected";
            btnDeleteSelected.UseVisualStyleBackColor = true;
            btnDeleteSelected.Click += btnDeleteSelected_Click;
            // 
            // btnSaveCatalog
            // 
            btnSaveCatalog.Location = new Point(682, 9);
            btnSaveCatalog.Name = "btnSaveCatalog";
            btnSaveCatalog.Size = new Size(118, 29);
            btnSaveCatalog.TabIndex = 11;
            btnSaveCatalog.Text = "Save Catalog";
            btnSaveCatalog.UseVisualStyleBackColor = true;
            btnSaveCatalog.Click += btnSaveCatalog_Click;
            // 
            // btnLoadCatalog
            // 
            btnLoadCatalog.Location = new Point(682, 45);
            btnLoadCatalog.Name = "btnLoadCatalog";
            btnLoadCatalog.Size = new Size(114, 29);
            btnLoadCatalog.TabIndex = 12;
            btnLoadCatalog.Text = "Load Catalog";
            btnLoadCatalog.UseVisualStyleBackColor = true;
            btnLoadCatalog.Click += btnLoadCatalog_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLoadCatalog);
            Controls.Add(btnSaveCatalog);
            Controls.Add(btnDeleteSelected);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAddBook);
            Controls.Add(txtAuthor);
            Controls.Add(txtTitle);
            Controls.Add(btnSeach);
            Controls.Add(txtSearch);
            Controls.Add(dgvBooks);
            Controls.Add(lblTitle);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitle;
        private DataGridView dgvBooks;
        private TextBox txtSearch;
        private Button btnSeach;
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private ContextMenuStrip contextMenuStrip1;
        private Label label1;
        private Label label2;
        private Button btnDeleteSelected;
        private Button btnSaveCatalog;
        private Button btnLoadCatalog;
    }
}
