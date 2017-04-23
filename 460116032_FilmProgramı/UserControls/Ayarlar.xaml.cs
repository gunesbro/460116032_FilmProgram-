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
    /// Interaction logic for Ayarlar.xaml
    /// </summary>
    public partial class Ayarlar : UserControl
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";
        DataTable dt = new DataTable();
        string secim;

        public Ayarlar()
        {
            InitializeComponent();

            Doldur();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Kartvizit();
        }

        #region KUTULARI DOLDUR
        private void Doldur()
        {
            SqlDataReader reader;
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Kullanıcı] WHERE KullaniciId = @girenKullanıcı", baglantı);
            cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                txtKullanıcıAd.Content = dt.Rows[0]["KullanıcıAdı"].ToString();
                txtAd.Text = dt.Rows[0]["Ad"].ToString();
                txtSoyad.Text = dt.Rows[0]["Soyad"].ToString();
                txtYas.Text = dt.Rows[0]["Yas"].ToString();

                if (dt.Rows[0]["Cinsiyet"].ToString() == "Erkek")
                {
                    rdErkek.IsChecked = true;
                }
                else
                {
                    rdKadin.IsChecked = true;
                }

            }
            baglantı.Close(); ;
        }
        #endregion


        #region YAŞ KISITI
        private void txtYas_KeyDown(object sender, KeyEventArgs e)
        {
            lblYas.Content = txtYas.Text;
            bool durum = false;
            if (txtYas.Text.Length < 2)
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = durum; //NumPad te bulunan rakamlar.
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = durum; //Harf grubunun üstünde bulunan rakamlar. 
            }
            else if (e.Key == Key.Tab)
            {
                e.Handled = false; //Textboxlar arası TAB ile geçiş yapabilmek için.
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion


        #region ŞİFRE KONTROL
        private void psdSifreOlus2_KeyDown(object sender, KeyEventArgs e)
        {
            sifreKontrol();
        }

        private void psdSifreOlus2_KeyUp(object sender, KeyEventArgs e)
        {
            sifreKontrol();

        }


        private void sifreKontrol()
        {
            if (psdSifreOlus2.Password != "")
            {
                if (psdSifreOlus1.Password != psdSifreOlus2.Password)
                {
                    psdSifreOlus1.BorderBrush = new SolidColorBrush(Colors.Red);
                    psdSifreOlus2.BorderBrush = new SolidColorBrush(Colors.Red);

                }
                else
                {
                    psdSifreOlus1.BorderBrush = new SolidColorBrush(Colors.Green);
                    psdSifreOlus2.BorderBrush = new SolidColorBrush(Colors.Green);

                }
            }

        }
        #endregion


        #region HESAP BİLGİ GÜNCELLE
        string cinsiyet;
        private void btnGüncelle_Click(object sender, RoutedEventArgs e)
        {
            if (dt.Rows[0]["KullanıcıAdı"].ToString() != txtAd.Text || dt.Rows[0]["Soyad"].ToString() != txtSoyad.Text || dt.Rows[0]["Yas"].ToString() != txtYas.Text)
            {
                if (psdEskiSifre.Password.ToString() == dt.Rows[0]["Sifre"].ToString())
                {
                    if (rdErkek.IsChecked == true)
                    {
                        cinsiyet = "Erkek";
                    }
                    if (rdKadin.IsChecked == true)
                    {
                        cinsiyet = "Kadın";
                    }
                    if (psdSifreOlus1.Password != "" && psdSifreOlus2.Password != "")
                    {
                        if (psdSifreOlus1.Password.Count() < 6)
                        {
                            if (psdSifreOlus1.Password.ToString() == psdSifreOlus2.Password.ToString())
                            {
                                try
                                {
                                    SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                                    baglantı.Open();
                                    SqlCommand cmd1 = new SqlCommand("UPDATE [Kullanıcı] SET Sifre = @Sifre", baglantı);
                                    cmd1.Parameters.AddWithValue("@Sifre", psdSifreOlus1.Password);
                                    cmd1.ExecuteNonQuery();
                                    baglantı.Close();
                                    Basarılı islem = new Basarılı();
                                    islem.lblBasarıMesaj.Content = "Tebrikler,Güncelleme İşlemi Gerçekleşti!";
                                    islem.ShowDialog();
                                    Doldur();
                                    Kartvizit();
                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                            }
                            else
                            {
                                Hata h = new Hata();
                                h.lblHataMesaj.Content = "Yeni Şifre Alanları Uyuşmuyor!";
                                h.ShowDialog();
                            }
                        }
                        else
                        {
                            Hata h = new Hata();
                            h.lblHataMesaj.Content = "Şifreniz En az 6 Karakterden Oluşmalı!";
                            h.ShowDialog();
                        }

                    }
                    try
                    {
                        SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                        baglantı.Open();
                        SqlCommand cmd1 = new SqlCommand("UPDATE [Kullanıcı] SET Ad = @Ad, Soyad = @Soyad, Yas = @Yas, Cinsiyet = @Cinsiyet", baglantı);
                        cmd1.Parameters.AddWithValue("@Ad", txtAd.Text);
                        cmd1.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                        cmd1.Parameters.AddWithValue("@Yas", txtYas.Text);
                        cmd1.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                        cmd1.ExecuteNonQuery();
                        baglantı.Close();
                        Basarılı islem = new Basarılı();
                        islem.lblBasarıMesaj.Content = "Tebrikler,Güncelleme İşlemi Gerçekleşti!";
                        islem.ShowDialog();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                else
                {
                    Hata h = new Hata();
                    h.lblHataMesaj.Content = "Aktif olan Şifreni Yanlış Girdin!";
                    h.ShowDialog();
                }
            }
            else
            {
                Hata h = new Hata();
                h.lblHataMesaj.Content = "Değişiklik Yapmadın Neyi Kaydediyorsun?";
                h.ShowDialog();
            }
        }
        #endregion


        #region KARTVİZİT İŞLEMLERİ
        private void Kartvizit()
        {
            lblKullanıcıAd.Content += " " + txtKullanıcıAd.Content;
            if (rdErkek.IsChecked == true)
            {
                secim = "male";
            }
            else
            {
                secim = "female";
            }
            elipsResim.Source = new BitmapImage(new Uri("/460116032_FilmProgramı;component/Images/" + secim.ToString() + ".ico", UriKind.Relative));

            lblAdSoyad.Content = txtAd.Text + " " + txtSoyad.Text;
            lblYas.Content = txtYas.Text;


            try
            {
                DataTable dt = new DataTable();
                SqlDataReader reader;
                SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Listem] WHERE KullanıcıId = @girenKullanıcı", baglantı);
                cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                txtFilmSayı.Text = dt.Rows.Count.ToString();
                baglantı.Close();

            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                DataTable dt = new DataTable();
                SqlDataReader reader;
                SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                baglantı.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Yorum] WHERE YorumYazanId = @girenKullanıcı", baglantı);
                cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                txtYorum.Text = dt.Rows.Count.ToString();
                baglantı.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtAd_KeyDown(object sender, KeyEventArgs e)
        {
            lblAdSoyad.Content = txtAd.Text + " " + txtSoyad.Text;
        }

        private void txtAd_KeyUp(object sender, KeyEventArgs e)
        {
            lblAdSoyad.Content = txtAd.Text + " " + txtSoyad.Text;
        }

        private void txtSoyad_KeyDown(object sender, KeyEventArgs e)
        {
            lblAdSoyad.Content = txtAd.Text + " " + txtSoyad.Text;

        }

        private void txtSoyad_KeyUp(object sender, KeyEventArgs e)
        {
            lblAdSoyad.Content = txtAd.Text + " " + txtSoyad.Text;

        }

        private void txtYas_KeyUp(object sender, KeyEventArgs e)
        {
            lblYas.Content = txtYas.Text;
        }
        

        private void rdErkek_Checked(object sender, RoutedEventArgs e)
        {
            elipsResim.Source = new BitmapImage(new Uri("/460116032_FilmProgramı;component/Images/" + "male" + ".ico", UriKind.Relative));

        }

        private void rdKadin_Checked(object sender, RoutedEventArgs e)
        {
            elipsResim.Source = new BitmapImage(new Uri("/460116032_FilmProgramı;component/Images/" + "female" + ".ico", UriKind.Relative));

        }
        #endregion
    }
}
