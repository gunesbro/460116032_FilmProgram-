using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460116032_FilmProgramı
{
    public class DetaySayfa
    {
        public int FilmId { get; set; }
        public static ObservableCollection<DetaySayfa> id = new ObservableCollection<DetaySayfa>();

    }
}
