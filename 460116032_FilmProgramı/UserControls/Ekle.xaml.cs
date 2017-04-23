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
using System.Windows.Threading;
using System.Xml;

namespace _460116032_FilmProgramı.UserControls
{
    /// <summary>
    /// Interaction logic for Ekle.xaml
    /// </summary>
    public partial class Ekle : UserControl
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";

        public Ekle()
        {
            InitializeComponent();

            txtVizyonYıl.Text = "2007";
            txtFilmAd.Text = "Zodiac";

        }

        #region YIL KISITI
        private void txtVizyonYıl_KeyDown(object sender, KeyEventArgs e)
        {
            bool durum = false;
            if (txtVizyonYıl.Text.Length < 4)
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = durum;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = durum;
            }
            else if (e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion

        #region ARA BAKALIM KODLARI
        XmlDocument xml = new XmlDocument();
        private void btnAraBakalım_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);

            string url = String.Format("http://www.omdbapi.com/?t={0}&y={1}&plot=full&r=xml", txtFilmAd.Text, txtVizyonYıl.Text);

            xml.Load(url);

            foreach (XmlElement item in xml.SelectNodes(@"root/movie"))
            {
                imgPoster.Source = new BitmapImage(new Uri(item.GetAttribute("poster"), UriKind.Absolute));
                txtFilmAdıGörüntü.Text = item.GetAttribute("title");
                txtYılGörüntü.Text = item.GetAttribute("year");
                txtYönetmen.Text = item.GetAttribute("director");
                txtRating.Text = item.GetAttribute("imdbRating");
                txtVotes.Text = item.GetAttribute("imdbVotes") + " " + "kişi oyladı.";
                txtOyuncular.Text = item.GetAttribute("actors") + "...";
                txtKategori.Text = item.GetAttribute("genre");
                txtKonu.Text = item.GetAttribute("plot");
            }
            stckLoad.Visibility = Visibility.Visible;
            stckBilgiler.Visibility = Visibility.Collapsed;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        #endregion

        #region TİMER LOADİNG OLAYLARI
        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer gelen = (DispatcherTimer)sender;

            if (xml.SelectNodes(@"root/movie").Count <= 0)
            {
                gelen.Stop();
                stckLoad.Visibility = Visibility.Collapsed;
                Hata h = new Hata();
                h.lblHataMesaj.Content = "Aranılan Film Malesef Bulunamadı!";
                h.ShowDialog();
                return;
            }

            if (imgPoster.Source.ToString() != "")
            {
                gelen.Stop();
                stckLoad.Visibility = Visibility.Collapsed;

                Storyboard sb1 = this.FindResource("stckGörün") as Storyboard;
                if (sb1 != null) { BeginStoryboard(sb1); }
                //stckBilgiler.Visibility = Visibility.Visible;
            }

        }
        #endregion

        #region LİSTEYE EKLE
        private void btnListeyeEkle_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection baglantı2 = new SqlConnection(sqlBağlantısı);
            baglantı2.Open();
            SqlCommand cmd1 = new SqlCommand("INSERT INTO [OzelListe] VALUES (@FilmAdı,@CıkısYılı,@Yönetmen,@Puan,@OylayanSayı,@Oyuncular,@Kategori,@Konu,@KullanıcıId,@PosterYol) ", baglantı2);
            cmd1.Parameters.AddWithValue("@FilmAdı", txtFilmAdıGörüntü.Text);
            cmd1.Parameters.AddWithValue("@CıkısYılı", txtVizyonYıl.Text);
            cmd1.Parameters.AddWithValue("@Yönetmen", txtYönetmen.Text);
            cmd1.Parameters.AddWithValue("@Puan", txtRating.Text);
            cmd1.Parameters.AddWithValue("@OylayanSayı", txtVotes.Text);
            cmd1.Parameters.AddWithValue("@Oyuncular", txtOyuncular.Text);
            cmd1.Parameters.AddWithValue("@Kategori", txtKategori.Text);
            cmd1.Parameters.AddWithValue("@Konu", txtKonu.Text);
            cmd1.Parameters.AddWithValue("@KullanıcıId", KullanıcıIdCek.id[0].kullanıcıId.ToString());
            cmd1.Parameters.AddWithValue("PosterYol", imgPoster.Source.ToString());
            cmd1.ExecuteNonQuery();
            baglantı2.Close();
            Basarılı b = new Basarılı();
            b.lblBasarıMesaj.Content = txtFilmAdıGörüntü.Text + " Başarıyla Listenize Eklendi!";
            b.ShowDialog();

        }
        #endregion
    }
}
