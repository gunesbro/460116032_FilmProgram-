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
using System.Xml;

namespace _460116032_FilmProgramı.UserControls
{
    /// <summary>
    /// Interaction logic for Top20.xaml
    /// </summary>
    public partial class Top20 : UserControl
    {
        string sqlBağlantısı = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=|DataDirectory|Db.mdf;";

        public Top20()
        {
            InitializeComponent();



            DataTable dt = new DataTable();
            SqlDataReader reader;
            SqlConnection baglantı = new SqlConnection(sqlBağlantısı);
            baglantı.Open();
            SqlCommand cmd = new SqlCommand("SELECT t.Top20Id,f.FilmAdı,f.FilmKonu,f.FilmKategori,f.FilmThumb,f.FilmCıkısYılı,f.FilmOrjinalAd,imdbRating FROM [Top20] t INNER JOIN [Film] f ON t.FilmId = f.FilmId", baglantı);
            cmd.Parameters.AddWithValue("@girenKullanıcı", KullanıcıIdCek.id[0].kullanıcıId.ToString());
            reader = cmd.ExecuteReader();
            dt.Load(reader);

            cntrlTop20.ItemsSource = dt.DefaultView;

            baglantı.Close();
        }
    }
}
