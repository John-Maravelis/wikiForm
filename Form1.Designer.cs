namespace WikiForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnSpeak = new Button();
            btnSave = new Button();
            rtbContent = new RichTextBox();
            pbImage = new PictureBox();
            dgvFavorites = new DataGridView();
            btnDelete = new Button();
            lnkFullArticle = new LinkLabel();
            btnSpeakStop = new Button();
            ((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFavorites).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(14, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(1106, 30);
            txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.FlatStyle = FlatStyle.System;
            btnSearch.Location = new Point(1138, 13);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(106, 33);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnSpeak
            // 
            btnSpeak.Location = new Point(14, 53);
            btnSpeak.Name = "btnSpeak";
            btnSpeak.Size = new Size(106, 33);
            btnSpeak.TabIndex = 2;
            btnSpeak.Text = "Read";
            btnSpeak.UseVisualStyleBackColor = true;
            btnSpeak.Click += btnSpeak_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(238, 53);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(153, 33);
            btnSave.TabIndex = 3;
            btnSave.Text = "Add to Favorites";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // rtbContent
            // 
            rtbContent.Location = new Point(14, 93);
            rtbContent.Name = "rtbContent";
            rtbContent.Size = new Size(865, 505);
            rtbContent.TabIndex = 4;
            rtbContent.Text = "";
            // 
            // pbImage
            // 
            pbImage.Location = new Point(914, 93);
            pbImage.Name = "pbImage";
            pbImage.Size = new Size(331, 424);
            pbImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbImage.TabIndex = 5;
            pbImage.TabStop = false;
            // 
            // dgvFavorites
            // 
            dgvFavorites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFavorites.Location = new Point(14, 658);
            dgvFavorites.MultiSelect = false;
            dgvFavorites.Name = "dgvFavorites";
            dgvFavorites.RowHeadersWidth = 51;
            dgvFavorites.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFavorites.Size = new Size(865, 244);
            dgvFavorites.TabIndex = 6;
            dgvFavorites.CellContentClick += dgvFavorites_CellContentClick;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(773, 908);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(106, 33);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lnkFullArticle
            // 
            lnkFullArticle.AutoSize = true;
            lnkFullArticle.Location = new Point(14, 601);
            lnkFullArticle.Name = "lnkFullArticle";
            lnkFullArticle.Size = new Size(197, 23);
            lnkFullArticle.TabIndex = 8;
            lnkFullArticle.TabStop = true;
            lnkFullArticle.Text = "Read more on Wikipedia";
            lnkFullArticle.Visible = false;
            lnkFullArticle.LinkClicked += lnkFullArticle_LinkClicked;
            // 
            // btnSpeakStop
            // 
            btnSpeakStop.Location = new Point(126, 53);
            btnSpeakStop.Name = "btnSpeakStop";
            btnSpeakStop.Size = new Size(106, 33);
            btnSpeakStop.TabIndex = 9;
            btnSpeakStop.Text = "Stop";
            btnSpeakStop.UseVisualStyleBackColor = true;
            btnSpeakStop.Click += btnSpeakStop_Click;
            // 
            // Form1
            // 
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1282, 953);
            Controls.Add(btnSpeakStop);
            Controls.Add(lnkFullArticle);
            Controls.Add(btnDelete);
            Controls.Add(dgvFavorites);
            Controls.Add(pbImage);
            Controls.Add(rtbContent);
            Controls.Add(btnSave);
            Controls.Add(btnSpeak);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Font = new Font("Segoe UI", 10F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "WikiForm";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFavorites).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnSpeak;
        private Button btnSave;
        private RichTextBox rtbContent;
        private PictureBox pbImage;
        private DataGridView dgvFavorites;
        private Button btnDelete;
        private LinkLabel lnkFullArticle;
        private Button btnSpeakStop;
    }
}
