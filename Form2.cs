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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
       
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = new Point(500, 100);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BT_Connexion_Click(object sender, EventArgs e)
        {
            try
            {
                //Définition de la chaîne de connexion dans chaineConnexion 
                string chaineConnexion = "datasource=localhost;username=root;password=;Initial Catalog=edbd";
                //Définition du texte de la requête sql dans query
                string Query = "SELECT * FROM utilisateur WHERE login ='" + this.TB_Login.Text + "'AND mdp='" + this.TB_MDP.Text + "'";

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
                        Form3 f = new Form3();
                        this.Hide();
                        f.Show();
                        Conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe ou login est invalide");
                        TB_Login.Text = "";
                        TB_MDP.Text = "";
                    }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.TB_Login.Text = "";
                this.TB_MDP.Text = "";
            }
        }

        private void TB_MDP_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}