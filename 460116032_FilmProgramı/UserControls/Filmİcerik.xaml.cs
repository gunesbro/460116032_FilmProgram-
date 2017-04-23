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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _460116032_FilmProgramı.UserControls
{
    /// <summary>
    /// Interaction logic for Filmİcerik.xaml
    /// </summary>
    public partial class Filmİcerik : UserControl
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";

        public Filmİcerik()
        {
            InitializeComponent();

            #region FİLM BİLGİLERİNİ GETİR

            SqlDataReader reader;
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            //SqlCommand cmd = new SqlCommand("SELECT f.FilmAdı,f.FilmKonu,f.FilmThumb,f.FilmKategori,f.FilmFragman,o.OyuncuAd,o.OyuncuSoyad FROM [FilmdekiOyuncular] fo INNER JOIN [Film] f ON fo.FilmId=f.FilmId INNER JOIN[Oyuncu] o ON fo.OyuncuId = o.OyuncuId WHERE fo.FilmId = @film", baglantı);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Film] WHERE FilmId = @film", baglantı);
            cmd.Parameters.AddWithValue("@film", DetaySayfa.id[0].FilmId.ToString());
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtKategori.Text = reader[4].ToString();
                lblFilmAd.Content = reader[1].ToString();
                imgResim.Source = new BitmapImage(new Uri(reader[3].ToString(), UriKind.Relative));
                frmFragman.Source = new Uri(reader[5].ToString(), UriKind.Absolute);
                txtKonu.Text = reader[2].ToString();
            }
            baglantı.Close();

            #endregion

        }

        
        #region FRAGMAN-FİLM GÖRSELİ GEÇİŞİ

        int sayac = 2;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sayac % 2 == 0)
            {
                Storyboard sb1 = this.FindResource("fragmanİzle") as Storyboard;
                if (sb1 != null) { BeginStoryboard(sb1); }
                txtFragmanVeResim.Text = "FİLM GÖRSELİNİ GÖRÜNTÜLE";
            }
            else
            {
                Storyboard sb1 = this.FindResource("görselGöster") as Storyboard;
                if (sb1 != null) { BeginStoryboard(sb1); }
                txtFragmanVeResim.Text = "FİLMİN FRAGMANINI İZLE";
            }
            sayac++;

        }

        #endregion

        
        #region FİLMDE OYNAYAN OYUNCuLAR + Yorum Listeleme 

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlDataReader reader1;
                DataTable dt = new DataTable();
                SqlConnection baglantı1 = new SqlConnection(sqlBağlantısı);
                baglantı1.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT o.OyuncuAd,o.OyuncuSoyad FROM [FilmdekiOyuncular] fo INNER JOIN [Film] f ON fo.FilmId=f.FilmId INNER JOIN[Oyuncu] o ON fo.OyuncuId = o.OyuncuId WHERE fo.FilmId = @filmId", baglantı1);

                cmd1.Parameters.AddWithValue("@filmId", DetaySayfa.id[0].FilmId.ToString());

                reader1 = cmd1.ExecuteReader();
                dt.Load(reader1);
                cntrlAdSoyad.ItemsSource = dt.DefaultView;
                baglantı1.Close();
            }
            catch (Exception)
            {

                throw;
            }


            try
            {
                YorumListe();

            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        
        #region YorumListe METHODU

        private void YorumListe()
        {
            SqlDataReader reader;
            DataTable dt1 = new DataTable();
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT y.YorumMetin,k.KullanıcıAdı FROM [Yorum] y INNER JOIN [Film] f ON f.FilmId=y.YorumFilmId INNER JOIN[Kullanıcı] k ON y.YorumYazanId =k.KullaniciId WHERE y.YorumFilmId = @filmId", baglantı);
            cmd.Parameters.AddWithValue("@filmId", DetaySayfa.id[0].FilmId.ToString());
            reader = cmd.ExecuteReader();
            dt1.Load(reader);
            if (dt1.Rows.Count <= 0)
            {
                txtYorumYok.Visibility = Visibility.Visible;
            }
            else
            {
                txtYorumYok.Visibility = Visibility.Collapsed;
            }
            cntrlYorum.ItemsSource = dt1.DefaultView;
            baglantı.Close();
        }

        #endregion


        #region YORUM GÖNDER

        private void btnGönder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txtYorum.Text != "")
            {
                Storyboard sb1 = this.FindResource("yorumFx") as Storyboard;
                if (sb1 != null) { BeginStoryboard(sb1); }
                try
                {
                    SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                    baglantı.Open();
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [Yorum] VALUES(@Ym,@yy,@yf)", baglantı);
                    cmd1.Parameters.AddWithValue("@Ym", txtYorum.Text);
                    cmd1.Parameters.AddWithValue("@yy", KullanıcıIdCek.id[0].kullanıcıId.ToString());
                    cmd1.Parameters.AddWithValue("@yf", DetaySayfa.id[0].FilmId.ToString());
                    cmd1.ExecuteNonQuery();
                    baglantı.Close();



                    YorumListe();
                }
                catch (Exception h)
                {
                    //MessageBox.Show(h.ToString());
                }
            }
            else
            {
                Hata h = new Hata();
                h.lblHataMesaj.Content = "Hiç Bişey Yazmamışsın be Kardeşim!!";
                h.ShowDialog();
            }
            
        }

        #endregion
    }
}
