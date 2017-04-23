using _460116032.Windows;
using _460116032_FilmProgramı.UserControls;
using System;
using System.Collections.Generic;
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

namespace _460116032_FilmProgramı
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";

        public MainWindow()
        {
            InitializeComponent();
            txtKullanıcıAd.Text = "gunesbro";
            psdSifre.Password = "123456";
        }


        #region ALTA AT - ÇIKIŞ YAP BUTONLARI
        private void cıkıs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Ekranı simge haline küçült.
            this.WindowState = WindowState.Minimized;
        }

        private void btnIptal_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion


        #region YENİ HESAP OLUŞTUR EKRANI AÇ
        private void btnYeniHesapOlustur_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb1 = this.FindResource("GirisYap_To_YeniHesap") as Storyboard;
            if (sb1 != null) { BeginStoryboard(sb1); }

        }

        private void btnGeriDön_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb1 = this.FindResource("YeniHesap_To_Giris") as Storyboard;
            if (sb1 != null) { BeginStoryboard(sb1); }

            txtAd.Text = ""; txtSoyad.Text = ""; txtYas.Text = "";
            txtYKullaniciAd.Text = ""; psdSifreOlus1.Password = "";
            psdSifreOlus2.Password = ""; rdErkek.IsChecked = false; rdKadin.IsChecked = false;

        }
        #endregion

        #region YAŞ KISITI
        private void txtYas_KeyDown(object sender, KeyEventArgs e)
        {
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


        #region YENİ KAYIT İŞLEMİ
        string cinsiyet;
        private void btnYeniKayit_Click(object sender, RoutedEventArgs e)
        {
            Hata hata = new Hata { Owner = this };

            if (txtAd.Text != "" && txtSoyad.Text != "" && txtYas.Text != "" && txtYKullaniciAd.Text != "" && psdSifreOlus1.Password != "" && psdSifreOlus2.Password != "" || rdErkek.IsSealed == true || rdKadin.IsSealed == true)
            {
                if (psdSifreOlus1.Password.Length >= 6)
                {
                    if (psdSifreOlus1.Password == psdSifreOlus2.Password)
                    {
                        try
                        {
                            SqlDataReader reader;
                            SqlConnection baglantı1 = new SqlConnection(sqlBağlantısı);
                            baglantı1.Open();
                            SqlCommand cmd = new SqlCommand("SELECT * FROM [Kullanıcı] WHERE KullanıcıAdı = @kAd ", baglantı1);
                            cmd.Parameters.AddWithValue("@kAd", txtYKullaniciAd.Text);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                hata.lblHataMesaj.Content = "Bu Kullanıcı Adı zaten Alınmış Durumda!";
                                hata.ShowDialog();
                            }
                            else
                            {
                                if (rdErkek.IsChecked == true)
                                {
                                    cinsiyet = "Erkek";
                                }
                                if (rdKadin.IsChecked == true)
                                {
                                    cinsiyet = "Kadın";
                                }

                                //2013
                                //string sqlBağlantısı = @"Server=(localdb)\v11.0;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";

                                try
                                {
                                    SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                                    baglantı.Open();
                                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [Kullanıcı] VALUES(@Ad,@Soyad,@Yas,@Cinsiyet,@KullanıcıAd,@Sifre)", baglantı);
                                    cmd1.Parameters.AddWithValue("@Ad", txtAd.Text);
                                    cmd1.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
                                    cmd1.Parameters.AddWithValue("@Yas", txtYas.Text);
                                    cmd1.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                                    cmd1.Parameters.AddWithValue("@KullanıcıAd", txtYKullaniciAd.Text);
                                    cmd1.Parameters.AddWithValue("@Sifre", psdSifreOlus1.Password);
                                    cmd1.ExecuteNonQuery();
                                    baglantı.Close();
                                    Basarılı islem = new Basarılı { Owner = this };
                                    islem.lblBasarıMesaj.Content = "Tebrikler,Kaydınız Başarıyla Gerçekleşti!";
                                    islem.ShowDialog();
                                }
                                catch (Exception h)
                                {
                                    //MessageBox.Show(h.ToString());
                                }

                            }
                            baglantı1.Close();


                        }
                        catch (Exception h)
                        {
                            //MessageBox.Show(h.ToString());
                        }

                        if (true)
                        {

                        }


                    }
                    else
                    {
                        hata.lblHataMesaj.Content = "Şifre Alanları Birbirileri ile Uyuşmuyor!";
                        hata.ShowDialog();
                    }
                }
                else
                {
                    hata.lblHataMesaj.Content = "Şifreniz en az 6 Karakterden Oluşmalıdır!";
                    hata.ShowDialog();
                }

            }
            else
            {
                hata.lblHataMesaj.Content = "Boş Alan Bırakmadığınızdan emin olun!";
                hata.ShowDialog();
            }
        }
        #endregion


        #region ŞİFRE KONTROLÜ
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

        public bool connectionCheck()
        {
            try
            {
                System.Net.Sockets.TcpClient kontrol_client = new System.Net.Sockets.TcpClient("www.google.com.tr", 80);
                kontrol_client.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("İNTERNET BAĞLANTINIZI KONTROL EDİN !","MOVİE PROJECT TEC-SUPPORT",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }

        #region SİSTEME GİRİŞ YAP
        private void btnGirisYap_Click(object sender, RoutedEventArgs e)
        {
            bool kontrol = connectionCheck();

            if (kontrol == true)
            {
                Hata hata = new Hata { Owner = this };

                try
                {

                    SqlDataReader reader;
                    SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
                    baglantı.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Kullanıcı] WHERE KullanıcıAdı = @kAd AND Sifre = @sifre", baglantı);
                    cmd.Parameters.AddWithValue("@kAd", txtKullanıcıAd.Text);
                    cmd.Parameters.AddWithValue("@sifre", psdSifre.Password);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        HomePage hp = new HomePage();
                        //if (hp.ShowDialog() == false)
                        //{
                        hp.CinsiyetBilgi = reader[4].ToString();
                        hp.imgCinsiyet.ToolTip = reader[5].ToString();
                        hp.txtkAd.Text = reader[5].ToString();

                        // COLLECTİON A ID ÇEK
                        KullanıcıIdCek.id.Add(new KullanıcıIdCek { kullanıcıId = int.Parse(reader[0].ToString()) });

                        Close();
                        hp.ShowDialog();

                        //}
                        //else
                        //{
                        //    hata.lblHataMesaj.Content = "Zaten Sisteme Giriş Yapmış Durumdasınız!";
                        //    hata.btnEkranKapat.Content = "Peki,Teşekkürler";
                        //    hata.ShowDialog();
                        //}
                    }
                    else
                    {
                        hata.lblHataMesaj.Content = "KullanıcıAdı veya Şifrenizi Hatalı Girdiniz!";
                        hata.ShowDialog();
                    }
                    baglantı.Close();


                }
                catch (Exception h)
                {
                    //MessageBox.Show(h.ToString());
                }
            }
            

        }

        #endregion
    }
}
