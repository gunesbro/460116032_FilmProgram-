using _460116032.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace _460116032_FilmProgramı.UserControls
{
    /// <summary>
    /// Interaction logic for Anasayfa.xaml
    /// </summary>
    public partial class Anasayfa : UserControl
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";
        DispatcherTimer timer = new DispatcherTimer();

        public Anasayfa()
        {
            
            InitializeComponent();

            #region FİLMLERİ LİSTELE

            DataTable dt = new DataTable();
            SqlDataReader reader;
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Film] ", baglantı);
            //cmd.Parameters.AddWithValue("@Kategori", "Çizgiroman Uyarlaması");
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            if (dt.Rows.Count > -1)
            {
                txtDetayGit.Tag = dt.Rows[0]["FilmId"].ToString();

                txtFilmAdı.Text = dt.Rows[0]["FilmAdı"].ToString();
                txtKategori.Text = dt.Rows[0]["FilmKategori"].ToString();
                txtKonu.Text = dt.Rows[0]["FilmKonu"].ToString().Substring(0, 300) + "...";
                frmFragman.Source = new Uri(dt.Rows[0]["FilmFragman"].ToString(), UriKind.Absolute);
                cntrlCizgiRomanUyarlama.ItemsSource = dt.DefaultView;

            }
            baglantı.Close();

            #endregion

            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        #region YÜKLENİYOR TİMER

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer t = (DispatcherTimer)sender;
            if (frmFragman.Source.ToString() != "")
            {
                t.Stop();
                cntrlLoading.Visibility = Visibility.Collapsed;
                frmFragman.Visibility = Visibility.Visible;
            }
            
        }
        #endregion


        #region TIKLANAN FİLMİ YAN PANELDE GÖRÜNTÜLE
        private void imgFilm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frmFragman.Visibility = Visibility.Hidden;
            cntrlLoading.Visibility = Visibility.Visible;


            Image gelen = (Image)sender;
            SqlDataReader reader;
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Film] WHERE FilmAdı=@film", baglantı);
            cmd.Parameters.AddWithValue("@film", gelen.Tag);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                frmFragman.Source = new Uri(reader[5].ToString(), UriKind.Absolute);
                Storyboard sb1 = this.FindResource("YanPanelFx") as Storyboard;
                if (sb1 != null) { BeginStoryboard(sb1); }
                txtFilmAdı.Text = reader[1].ToString();
                txtKategori.Text = reader[4].ToString();

                txtDetayGit.Tag = reader[0].ToString();
                txtKonu.Text = reader[2].ToString().Substring(0, 300) + "...";

            }

            baglantı.Close();

            timer.Start();
            
        }
        #endregion


        #region LİSTEYE EKLE
        private void btnListeEkle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //HATALIIIIIIIIIIIIIIIIIIIII
            Image gelen = (Image)sender;

            SqlDataReader reader;
            DataTable dt = new DataTable();
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Listem] WHERE FilmId != @film", baglantı);
            cmd.Parameters.AddWithValue("@film", gelen.Tag.ToString());
            reader = cmd.ExecuteReader();
            //dt.Rows.Remove(dt.Rows[0]);
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                SqlConnection baglantı2 = new SqlConnection(sqlBağlantısı);
                baglantı2.Open();
                SqlCommand cmd1 = new SqlCommand("INSERT INTO [Listem] VALUES (@FilmId,@KullanıcıId) ", baglantı2);
                cmd1.Parameters.AddWithValue("@FilmId", gelen.Tag);
                cmd1.Parameters.AddWithValue("@KullanıcıId", KullanıcıIdCek.id[0].kullanıcıId.ToString());
                cmd1.ExecuteNonQuery();
                baglantı2.Close();
                dt.Rows.Remove(dt.Rows[0]);
                Basarılı b = new Basarılı();
                b.lblBasarıMesaj.Content = "Seçtiğiniz Film Başarıyla Listenize Eklendi!";
                b.ShowDialog();
                
            }
            else
            {
                Hata h = new Hata();
                h.lblHataMesaj.Content = "Seçtiğiniz Film Zaten Listenizde Var!";
                h.ShowDialog();
                
            }
            baglantı.Close();
        }

        #endregion


        #region FİLM SAYFASINA GİT

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DetaySayfa.id.Clear();
            DetaySayfa.id.Add(new DetaySayfa { FilmId = int.Parse(txtDetayGit.Tag.ToString()) });
            //MessageBox.Show(txtDetayGit.Tag.ToString());
            //HomePage h = new HomePage() { };
            //h.frmGezinti.Source = new Uri("/UserControls/Filmİcerik.xaml", UriKind.Relative);

            icerikAc();
        }

        public void icerikAc()
        {
            grd.Children.Clear();
            Filmİcerik fr = new Filmİcerik();
            grd.Children.Add(fr);
            
        }

        #endregion
    }
}
