using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApptEST.Models
{
    public class User :INotifyPropertyChanged
    {
        public int Id { get; set; } // identificator
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
      
        //public ImageSource Image2 { get; set; } // file name
        public string Image { get; set; } // file name
        //public string Password { get; set; } // save as a code
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}
