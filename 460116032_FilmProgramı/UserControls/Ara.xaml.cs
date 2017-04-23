using _460116032.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _460116032_FilmProgramı.UserControls
{
    /// <summary>
    /// Interaction logic for Ara.xaml
    /// </summary>
    public partial class Ara : UserControl
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";

        public Ara()
        {
            InitializeComponent();
           
        }

        #region GİRİLEN TEXTE GÖRE ARA

        private void txtAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAra.Text != "" && txtAra.Text != "Araki Bulasın!")
            {
                DataTable dt = new DataTable();
                SqlDataReader reader;
                SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Film] WHERE UPPER(FilmAdı) LIKE '" + txtAra.Text.ToUpper().ToString() + "%'", baglantı);
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                cntrlAra.ItemsSource = dt.DefaultView;
                baglantı.Close();
            }
            else
            {
                cntrlAra.ItemsSource = "";
            }
        }

        #endregion


        #region ARAMA SONUC LİSTESİNE TIKLANDIĞINDA...

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txtDetayGit = (TextBlock)sender;
            DetaySayfa.id.Clear();
            DetaySayfa.id.Add(new DetaySayfa { FilmId = int.Parse(txtDetayGit.Tag.ToString()) });

            txtBaslik.Visibility = Visibility.Collapsed;
            spİcerik.Width = 1050;
            cntrlAra.Visibility = Visibility.Collapsed;
            txtAra.Visibility = Visibility.Collapsed;
            
            frm.Source = new Uri("/UserControls/Filmİcerik.xaml", UriKind.Relative);
        }

        #endregion
    }
}
