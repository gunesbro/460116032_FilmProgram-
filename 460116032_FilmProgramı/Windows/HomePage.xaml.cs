using _460116032_FilmProgramı;
using _460116032_FilmProgramı.UserControls;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace _460116032.Windows
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public string KullanıcıAd;
        public HomePage()
        {
            InitializeComponent();
            Panel.SetZIndex(cnvs, 999);
            KullanıcıAd = txtkAd.Text;
        }

        #region CİNSİYETE GÖRE İCON GETİRME + SAYFA TİTLE

        public string CinsiyetBilgi;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frmGezinti.Source = new Uri("/UserControls/Anasayfa.xaml",UriKind.Relative);
            if (CinsiyetBilgi == "Erkek")
            {
                imgCinsiyet.Source = new BitmapImage(new Uri("/Images/male.ico", UriKind.Relative));
            }
            else
            {
                imgCinsiyet.Source = new BitmapImage(new Uri("/Images/female.ico", UriKind.Relative));
            }

            this.Title = "GUNESBRO MOVİE PROJECT - " + imgCinsiyet.ToolTip;

        }

        #endregion


        #region SOL PANEL ÜSTÜNE GELİNEN BAŞLIĞIN RENGİ

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border gelenBorder = (Border)sender;
            gelenBorder.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border gelenBorder = (Border)sender;
            if (gelenBorder.Tag.ToString() == "brdrAra")
            {
                gelenBorder.BorderBrush = new SolidColorBrush(Colors.Green);
            }
            if (gelenBorder.Tag.ToString() == "brdrListe")
            {
                gelenBorder.BorderBrush = new SolidColorBrush(Colors.Maroon);
            }
            if (gelenBorder.Tag.ToString() == "brdrTop20")
            {
                var bc = new BrushConverter();
                gelenBorder.BorderBrush = (Brush)bc.ConvertFrom("#FF004B88");
            }
            if (gelenBorder.Tag.ToString() == "brdrAyarlar")
            {
                var bc = new BrushConverter();
                gelenBorder.BorderBrush = (Brush)bc.ConvertFrom("#FF340088");
            }
            if (gelenBorder.Tag.ToString() == "brdrAna")
            {
                var bc = new BrushConverter();
                gelenBorder.BorderBrush = (Brush)bc.ConvertFrom("#FFF5FF08");
            }
            if (gelenBorder.Tag.ToString() == "brdrVizyon")
            {
                var bc = new BrushConverter();
                gelenBorder.BorderBrush = (Brush)bc.ConvertFrom("#FF3D3038");
            }

        }

        #endregion


        #region SOL PANEL SAYFALAR ARASI YÖNLENDİRME

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stck.Children.Clear();
            StackPanel sp = (StackPanel)sender;
            frmGezinti.Source = new Uri("/UserControls/" + sp.Tag.ToString() + ".xaml", UriKind.Relative);
            
        }

        #endregion


        #region ARA USERCONTROL UNUN ÇALIŞTIRILMASI

        int sayac = 2;
        private void StackPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (sayac % 2 == 0)
            {
                Ara ara = new Ara();
                stck.Children.Add(ara);
            }
            else
            {
                stck.Children.Clear();
            }
            sayac++;
            
        }
        

        #endregion

    }
}
