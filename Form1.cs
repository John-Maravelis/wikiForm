using Newtonsoft.Json;
using System.Net.Http;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace WikiForms
{
    public partial class Form1 : Form
    {
        DbHelper db = new DbHelper(); // Δημιουργία αντικειμένου
        SpeechSynthesizer synth = new SpeechSynthesizer();
        string currentArticleUrl = "";

        public Form1()
        {
            InitializeComponent();
            db.InitializeDatabase(); // Φτιάχνει το αρχείο .db αν δεν υπάρχει
            LoadFavorites(); // Φορτώνει τη λίστα στο ξεκίνημα

            dgvFavorites.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. Ορίζουμε τα ποσοστά (FillWeight)
            // Προσοχή: Τα ονόματα "Id", "Title", "Url" πρέπει να είναι ίδια με της Βάσης
            if (dgvFavorites.Columns.Count > 0)
            {
                // Ελέγχουμε αν υπάρχει η κάθε στήλη πριν της αλλάξουμε μέγεθος
                if (dgvFavorites.Columns.Contains("Id"))
                    dgvFavorites.Columns["Id"].FillWeight = 20;

                if (dgvFavorites.Columns.Contains("Title"))
                    dgvFavorites.Columns["Title"].FillWeight = 20;

                if (dgvFavorites.Columns.Contains("Url"))
                    dgvFavorites.Columns["Url"].FillWeight = 60;
            }
        }

        private void dgvFavorites_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadFavorites()
        {
            dgvFavorites.DataSource = db.GetFavorites();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string term = txtSearch.Text;
            string url = $"https://el.wikipedia.org/api/rest_v1/page/summary/{term}";

            // Βεβαιώσου ότι έχεις προσθέσει: using System.IO; ψηλά στον κώδικα

            using (HttpClient client = new HttpClient())
            {
                // 1. ΛΥΣΗ ΓΙΑ ΤΟ 403: Δηλώνουμε ταυτότητα User-Agent
                client.DefaultRequestHeaders.Add("User-Agent", "WikiFormsStudentProject/1.0");

                try
                {
                    var response = await client.GetStringAsync(url);
                    WikiArticle article = JsonConvert.DeserializeObject<WikiArticle>(response);

                    // Εμφάνιση κειμένου
                    rtbContent.Text = $"{article.title}\n\n{article.extract}";

                    // Αποθήκευση URL για το LinkLabel
                    currentArticleUrl = "https://el.wikipedia.org/wiki/" + article.title.Replace(" ", "_");
                    lnkFullArticle.Visible = true;

                    // --- ΔΙΑΧΕΙΡΙΣΗ ΕΙΚΟΝΑΣ ---
                    if (article.thumbnail != null)
                    {
                        try
                        {
                            // 2. ΛΥΣΗ ΓΙΑ ΤΟ 403 ΣΤΗΝ ΕΙΚΟΝΑ:
                            // Κατεβάζουμε την εικόνα με τον client (που έχει την ταυτότητα)
                            // και ΟΧΙ με το pbImage.Load() που τρώει πόρτα.
                            var stream = await client.GetStreamAsync(article.thumbnail.source);
                            pbImage.Image = Image.FromStream(stream);
                        }
                        catch
                        {
                            // Αν κάτι πάει στραβά στο κατέβασμα -> Φόρτωσε από φάκελο
                            LoadLocalImage();
                        }
                    }
                    else
                    {
                        // Αν δεν υπάρχει εικόνα στο άρθρο -> Φόρτωσε από φάκελο
                        LoadLocalImage();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Το λήμμα δεν βρέθηκε ή υπήρξε σφάλμα.");
                    LoadLocalImage(); // Και στο σφάλμα δείχνουμε το No Image
                }
            }
        }

        private void LoadLocalImage()
        {
            // Βρίσκουμε τον φάκελο που τρέχει το πρόγραμμα (bin\Debug)
            string executablePath = Application.StartupPath;

            // Συνθέτουμε τη διαδρομή: bin\Debug\Images\no_image.png
            // Προσοχή: Βεβαιώσου ότι ο φάκελος λέγεται "Images" και η εικόνα "no_image.png"
            string imagePath = Path.Combine(executablePath, "no_image.png");

            if (File.Exists(imagePath))
            {
                pbImage.Image = Image.FromFile(imagePath);
            }
            else
            {
                pbImage.Image = null; // Αν δεν το βρει ούτε εκεί, άστο κενό
            }
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            // Σταματάει ό,τι έλεγε πριν, για να μην μπερδεύονται οι φωνές
            synth.SpeakAsyncCancelAll();

            synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.Adult, 0, new System.Globalization.CultureInfo("el-GR")); // Προσπάθεια για Ελληνικά

            // Έλεγχος αν υπάρχει κείμενο για ανάγνωση
            if (!string.IsNullOrEmpty(rtbContent.Text))
            {
                synth.SpeakAsync(rtbContent.Text);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Παίρνουμε τον τίτλο από το TextBox ή το Label που δείχνει το αποτέλεσμα
            // Υποθέτω ότι έχεις κρατήσει τον τίτλο κάπου ή τον παίρνεις από το TextBox αναζήτησης
            string title = txtSearch.Text;

            // Το URL της Wikipedia είναι σταθερό + τον όρο αναζήτησης
            string url = "https://el.wikipedia.org/wiki/" + txtSearch.Text;

            if (!string.IsNullOrEmpty(title))
            {
                if (db.ArticleExists(url))
                {
                    MessageBox.Show("Το άρθρο υπάρχει ήδη στα αγαπημένα!");
                    return; // Σταματάει εδώ, δεν αποθηκεύει ξανά
                }

                db.AddFavorite(title, url);
                MessageBox.Show("Αποθηκεύτηκε!");
                LoadFavorites(); // Ανανανέωσε τη λίστα
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Έλεγχος: Έχει επιλέξει ο χρήστης κάποια γραμμή;
            if (dgvFavorites.SelectedRows.Count > 0)
            {
                // 2. Παίρνουμε την επιλεγμένη γραμμή
                DataGridViewRow selectedRow = dgvFavorites.SelectedRows[0];

                // 3. Βρίσκουμε το ID (είναι στο κελί "Id" ή στο κελί 0 αν είναι η πρώτη στήλη)
                // Προσοχή: Το όνομα "Id" πρέπει να ταιριάζει με αυτό που βάλαμε στο DbHelper (CREATE TABLE)
                int idToDelete = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                // 4. Καλούμε τη μέθοδο διαγραφής από την κλάση DbHelper
                db.DeleteFavorite(idToDelete);

                // 5. Ενημερώνουμε τον χρήστη και ανανεώνουμε τη λίστα
                MessageBox.Show("Η εγγραφή διαγράφηκε επιτυχώς!");
                LoadFavorites(); // Ξαναφορτώνουμε για να εξαφανιστεί η διαγραμμένη γραμμή
            }
            else
            {
                MessageBox.Show("Παρακαλώ επιλέξτε μια γραμμή για διαγραφή.");
            }
        }

        private void lnkFullArticle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentArticleUrl))
            {
                // Νέος τρόπος για .NET Core / .NET 6+
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = currentArticleUrl,
                    UseShellExecute = true // <--- Αυτό είναι το κλειδί! Λέει στα Windows να ανοίξουν τον browser.
                });
            }
        }


        private void btnSpeakStop_Click(object sender, EventArgs e)
        {
            synth.SpeakAsyncCancelAll(); // Σταματάει αμέσως την ομιλία
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
