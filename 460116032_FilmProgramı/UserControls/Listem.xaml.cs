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
    /// Interaction logic for Listem.xaml
    /// </summary>
    public partial class Listem : UserControl
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";

        public Listem()
        {
            InitializeComponent();

            Listele();

        }

        #region ÖZEL LİSTE
        private void OzelListe()
        {
            DataTable dt = new DataTable();
            SqlDataReader reader;
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [OzelListe] WHERE KullanıcıId = @girenKullanıcı", baglantı);
            cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                spListeBos.Visibility = Visibility.Collapsed;
                cntrlOzelList.ItemsSource = dt.DefaultView;
            }
            else
            {
                cntrlOzelList.ItemsSource = dt.DefaultView;
                spListeBos.Visibility = Visibility.Visible;
            }


            baglantı.Close();
        }
        #endregion


        #region LİSTEDEKİ FİLMLERİ GÖSTER -(Listele Methodu)
        private void Listele()
        {
            
            DataTable dt = new DataTable();
            SqlDataReader reader;
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT f.FilmId, f.FilmAdı, k.KullaniciId, k.KullanıcıAdı,FilmThumb,f.imdbRating FROM [Listem] l INNER JOIN [Film] f ON l.FilmId = f.FilmId INNER JOIN [Kullanıcı] k ON l.KullanıcıId = k.KullaniciId WHERE l.KullanıcıId = @girenKullanıcı", baglantı);
            cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                spListeBos.Visibility = Visibility.Collapsed;
                txtKullanıcıAd.Content = dt.Rows[0]["KullanıcıAdı"].ToString();
                cntrlListem.ItemsSource = dt.DefaultView;
            }
            else
            {
                spListeBos.Visibility = Visibility.Visible;
            }


            baglantı.Close();
        }
        #endregion


        #region LİSTEDEN ÇIKAR

        private void btnListeSil_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult m = new MessageBoxResult();
            m = MessageBox.Show("Seçili Filmi Listenden Çıkarmaya Emin misin?", "Film Programı", MessageBoxButton.YesNo);
            if (m == MessageBoxResult.Yes)
            {
                Image gelen = (Image)sender;
                SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [Listem] WHERE KullanıcıId = @girenKullanıcı AND FilmId = @film", baglantı);
                cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
                cmd.Parameters.AddWithValue("@film", gelen.Tag.ToString());
                cmd.ExecuteNonQuery();

                Listele();
            }
            else
            {

               
            }
        }

        #endregion


        #region RESME TIKLANDIĞINDA İNTERNETTE ARAMA

        private void imgFilm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image gelen = (Image)sender;
            System.Diagnostics.Process.Start("https://www.google.com.tr/?gfe_rd=cr&ei=9bfvWO64Iaiz8we-mZCADA#safe=active&q=" + gelen.Tag.ToString() +" izle");

        }

        #endregion


        #region Listeler Arası Geçiş
        private void lblListem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (spListeBos.Visibility == Visibility.Visible)
            {
                spListeBos.Visibility = Visibility.Collapsed;
            }
            scrlListem.Visibility = Visibility.Visible;
            scrlOzelListe.Visibility = Visibility.Collapsed;
        }

        private void lblOzelListe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            if (spListeBos.Visibility == Visibility.Visible)
            {
                spListeBos.Visibility = Visibility.Collapsed;
            }
            OzelListe();
            scrlListem.Visibility = Visibility.Collapsed;
            scrlOzelListe.Visibility = Visibility.Visible;
        }
        #endregion


        #region ÖZEL LİSTEDEN ÇIKAR
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult m = new MessageBoxResult();
            m = MessageBox.Show("Seçili Filmi Listenden Çıkarmaya Emin misin?", "Film Programı", MessageBoxButton.YesNo);
            if (m == MessageBoxResult.Yes)
            {
                TextBlock gelen = (TextBlock)sender;
                SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM [OzelListe] WHERE KullanıcıId = @girenKullanıcı AND OzelListeId = @film", baglantı);
                cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
                cmd.Parameters.AddWithValue("@film", gelen.Tag.ToString());
                cmd.ExecuteNonQuery();

                OzelListe();
            }
            else
            {


            }
        }
        #endregion


        #region ÖZEL LİSTE RESME TIKLANDIĞINDA İNTERNETTE ARAMA
        private void imgPoster_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image gelen = (Image)sender;
            System.Diagnostics.Process.Start("https://www.google.com.tr/?gfe_rd=cr&ei=9bfvWO64Iaiz8we-mZCADA#safe=active&q=" + gelen.Tag.ToString() + " izle");

        }
        #endregion
    }
}
