using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace projet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(200, 100);
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                //Définition de la chaîne de connexion dans chaineConnexion
                string chaineConnexion = "datasource=localhost;username=root;password=;Initial Catalog=edbd";
                //Définition du texte de la requête sql dans query
                string Query = "UPDATE etudiant SET Nom_Etudiant='" + this.TB_Nom.Text + "'," +
                               "Prénom_Etudiant='" + this.TB_Prenom.Text + "'," +
                               "Département='" + this.TB_Département.Text + "'," +
                               "Niveau='" + this.TB_Niveau.Text + "'," +
                               "DateNaissance='" + this.dateTimePicker1.Text + "'," +
                               "CV_pdf='" + this.TB_CV_PDFs.Text + "'," +
                               "CV_Video='" + this.TB_CV_Videos.Text + "'," +
                               "CV_Photos='" + this.TB_CV_Photos.Text +
                               "' where NCIN = " + this.TB_NCin.Text;

                //Instanciation d'une nouvelle connexion dans conn  
                MySqlConnection Conn = new MySqlConnection(chaineConnexion);

                //Instanciation d'une nouvelle commande dans cmd
                MySqlCommand cmd = new MySqlCommand(Query, Conn);

                //Instanciation d'un nouveau data reader dr
                MySqlDataReader dr;
                Conn.Open();
                dr = cmd.ExecuteReader();

                BT_Modifier.Enabled = true;
                BT_Supprimer.Enabled = true;
                BT_Ajouter.Enabled = false;

                Conn.Close();
                MessageBox.Show("Etudiant modifié avec succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //Définition de la chaîne de connexion dans chaineConnexion
                string chaineConnexion = "datasource=localhost;username=root;password=;Initial Catalog=edbd";
                //Définition du texte de la requête sql dans query
                string Query = "DELETE FROM etudiant  where NCIN = " + this.TB_NCin.Text;

                //Instanciation d'une nouvelle connexion dans conn  
                MySqlConnection Conn = new MySqlConnection(chaineConnexion);

                //Instanciation d'une nouvelle commande dans cmd
                MySqlCommand cmd = new MySqlCommand(Query, Conn);

                //Instanciation d'un nouveau data reader dr
                MySqlDataReader dr;
                Conn.Open();
                dr = cmd.ExecuteReader();
                MessageBox.Show("Etudiant supprimé avec succès");
                this.TB_Nom.Text = "";
                this.TB_Prenom.Text = "";
                this.TB_Département.Text = "";
                this.TB_Niveau.Text = "";
                this.dateTimePicker1.Text = "";
                this.TB_CV_Videos.Text = "";
                this.TB_CV_PDFs.Text = "";
                this.TB_CV_Photos.Text = "";
                this.AcroPDF_CV.Visible = false;
                this.axWindowsMediaPlayer1.Ctlcontrols.stop();
                this.axWindowsMediaPlayer1.Visible = false;

                this.pictureBox3.Image = null;
                this.TB_CV_Videos.Text = null;
                BT_Modifier.Enabled = false;
                BT_Supprimer.Enabled = false;
                BT_Ajouter.Enabled = false;
                Conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        OpenFileDialog openFile = new OpenFileDialog();
        public string SinglePage { get; private set; }
        public string Fit { get; private set; }
        public string Fitb { get; private set; }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Définition de la chaîne de connexion dans chaineConnexion 
                string chaineConnexion = "datasource=localhost;username=root;password=;Initial Catalog=edbd";
                //Définition du texte de la requête sql dans query
                string Query = "SELECT * FROM etudiant WHERE NCIN =" + this.TB_NCin.Text;

                //Instanciation d'une nouvelle connexion dans conn  
                MySqlConnection Conn = new MySqlConnection(chaineConnexion);

                //Instanciation d'une nouvelle commande dans cmd
                MySqlCommand cmd = new MySqlCommand(Query, Conn);

                //Instanciation d'un nouveau data reader dr
                MySqlDataReader dr;
                Conn.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    this.TB_Nom.Text = dr.GetString(1);
                    this.TB_Prenom.Text = dr.GetString(2);
                    this.TB_Département.Text = dr.GetString(3);
                    this.TB_Niveau.Text = dr.GetString(4);
                    this.dateTimePicker1.Text = dr.GetDateTime(5).ToShortDateString();

                    this.TB_CV_PDFs.Text = dr.GetString(6);
                    this.TB_CV_Videos.Text = dr.GetString(7);
                    this.TB_CV_Photos.Text = dr.GetString(8);

                    this.axWindowsMediaPlayer1.Visible = true;

                    this.axWindowsMediaPlayer1.URL = dr.GetString(7);
                    this.AcroPDF_CV.Visible = false;
                    this.pictureBox3.Image = Image.FromFile(dr.GetString(8));

                    /* this.AcroPDF_CV.LoadFile(TB_CV_Name.Text);
                     this.AcroPDF_CV.setZoom(50);*/

                    BT_Modifier.Enabled = true;
                    BT_Supprimer.Enabled = true;
                    BT_Ajouter.Enabled = false;
                    Conn.Close();
                }
                else
                {
                    this.TB_Nom.Text = "";
                    this.TB_Prenom.Text = "";
                    this.TB_Département.Text = "";
                    this.TB_Niveau.Text = "";
                    this.dateTimePicker1.Text = "";
                    this.TB_CV_Videos.Text = "";
                    this.TB_CV_PDFs.Text = "";
                    this.TB_CV_Photos.Text = "";
                    this.axWindowsMediaPlayer1.URL = null;
                    this.AcroPDF_CV.Visible = false;
                    MessageBox.Show("Etudiant innexistant");
                    this.pictureBox3.Image = null;

                    BT_Modifier.Enabled = false;
                    BT_Supprimer.Enabled = false;
                    BT_Ajouter.Enabled = true;
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.TB_Nom.Text = "";
                this.TB_Prenom.Text = "";
                this.TB_Département.Text = "";
                this.TB_Niveau.Text = "";
                this.dateTimePicker1.Text = "";
                this.TB_CV_Videos.Text = "";
                this.axWindowsMediaPlayer1.Text = "";
                this.TB_CV_Photos.Text = "";
                this.TB_CV_PDFs.Text = "";
                this.pictureBox3.Image = null;
                this.axWindowsMediaPlayer1.URL = null;
                BT_Modifier.Enabled = false;
                BT_Supprimer.Enabled = false;
            }

            }

            private void button10_Click(object sender, EventArgs e)
        {
                // Définition de la chaîne de connexion dans chaineConnexion
            string chaineConnexion1 = "datasource=localhost;username=root;password=;Initial Catalog=edbd";
            //Définition du texte de la requête sql dans query
            string Query1 = "select * from etudiant where NCIN = " + this.TB_NCin.Text;

            //Instanciation d'une nouvelle connexion dans conn  
            MySqlConnection Conn1 = new MySqlConnection(chaineConnexion1);

            //Instanciation d'une nouvelle commande dans cmd
            MySqlCommand cmd1 = new MySqlCommand(Query1, Conn1);

            //Instanciation d'un nouveau data reader dr
            MySqlDataReader dr1;
            Conn1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                MessageBox.Show("NCIN existe déjà");
                Conn1.Close();
            }
            else
            {
                try
                {
                    //Définition de la chaîne de connexion dans chaineConnexion
                    string chaineConnexion = "datasource=localhost;username=root;password=;Initial Catalog=edbd";
                    //Définition du texte de la requête sql dans query
                    string Query = "INSERT INTO ETUDIANT(NCIN, Nom_Etudiant,Prénom_Etudiant,Département,Niveau,DateNaissance,CV_Pdf,CV_Video,CV_Photos) VALUES(" +
                        this.TB_NCin.Text + ",'" +
                        this.TB_Nom.Text + "','" +
                        this.TB_Prenom.Text + "','" +
                        this.TB_Département.Text + "','" +
                        this.TB_Niveau.Text + "','" +
                        this.dateTimePicker1.Text + "','" +
                        this.TB_CV_PDFs.Text + "','" +
                        this.TB_CV_Videos.Text + "','" +
                        this.TB_CV_Photos.Text + "');";

                    //Instanciation d'une nouvelle connexion dans conn  
                    MySqlConnection Conn = new MySqlConnection(chaineConnexion);

                    //Instanciation d'une nouvelle commande dans cmd
                    MySqlCommand cmd = new MySqlCommand(Query, Conn);

                    //Instanciation d'un nouveau data reader dr
                    MySqlDataReader dr;
                    Conn.Open();
                    dr = cmd.ExecuteReader();

                    BT_Modifier.Enabled = true;
                    BT_Supprimer.Enabled = true;
                    BT_Ajouter.Enabled = false;
                    Conn.Close();
                    MessageBox.Show("Etudiant ajouté.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problème d''ajout, Texte de l'erreur: " + ex.Message);
                }
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Height = BT_Expert.Height;
            panel2.Top = BT_Expert.Top;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            openFile.DefaultExt = "pdf";
            openFile.Filter = "Fichier MapInfoFormat (*.pdf)|*.pdf";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                TB_CV_PDFs.Text = openFile.SafeFileName;
            }

        }

        private void button19_Click(object sender, EventArgs e)
        {
            openFile.DefaultExt = "jpg";
            openFile.Filter = "Fichier MapInfoFormat (*.jpg)|*.jpg";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {

                /*TB_Photo_name.Text= openFile.FileName;*/
                TB_CV_Photos.Text = openFile.SafeFileName;
                pictureBox3.Image = Image.FromFile(openFile.SafeFileName);

            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            openFile.DefaultExt = "mp4";
            openFile.Filter = "Fichier MapInfoFormat (*.mp4)|*.mp4";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                TB_CV_Videos.Text = openFile.SafeFileName;
                this.axWindowsMediaPlayer1.URL = openFile.SafeFileName;
                this.axWindowsMediaPlayer1.Visible = true;
            }

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void BT_Aperçu_Vidéo_Click(object sender, EventArgs e)
        {
            this.AcroPDF_CV.Visible = false;
            this.axWindowsMediaPlayer1.Visible = true;
            this.axWindowsMediaPlayer1.URL = TB_CV_Videos.Text;

        }

        private void BT_Aperçu_CV_Click(object sender, EventArgs e)
        {
            this.axWindowsMediaPlayer1.Ctlcontrols.stop();
            this.axWindowsMediaPlayer1.Visible = false;
            this.AcroPDF_CV.Visible = true;
            this.AcroPDF_CV.LoadFile(TB_CV_PDFs.Text);

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Height = BT_Condidats.Height;
            panel2.Top = BT_Condidats.Top;
        }

        private void BT_projet_Click(object sender, EventArgs e)
        {
            panel2.Height = BT_projet.Height;
            panel2.Top = BT_projet.Top;
        }

        private void BT_Competition_Click(object sender, EventArgs e)
        {
            panel2.Height = BT_Competition.Height;
            panel2.Top = BT_Competition.Top;
        }

        private void BT_Resultat_Click(object sender, EventArgs e)
        {
            panel2.Height = BT_Resultat.Height;
            panel2.Top = BT_Resultat.Top;
        }

        private void BT_Statistique_Click(object sender, EventArgs e)
        {
            panel2.Height = BT_Statistique.Height;
            panel2.Top = BT_Statistique.Top;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            this.Hide();
            f.Show();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TB_NCin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
