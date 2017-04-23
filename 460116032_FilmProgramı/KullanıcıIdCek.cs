using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _460116032_FilmProgramı
{

    public class KullanıcıIdCek
    {
        public int kullanıcıId { get; set; }
        public static ObservableCollection<KullanıcıIdCek> id = new ObservableCollection<KullanıcıIdCek>();
    }
    
}
