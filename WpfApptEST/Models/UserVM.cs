using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApptEST.Models
{
    public class UserVM :INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string name;
        public string Name 
        {
            get { return name; }
            set 
            {
                if(this.name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Changed");
                }
            }
        }
        private string image;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string ImageUrl 
        { 
            get { return image; }
            set
            {
                if (this.image != value)
                {
                    image = value;
                    NotifyPropertyChanged("Changed");
                }
            }
        }
        //public ImageSource Image
        //{
        //    get
        //    {
        //        var UriSource = new Uri(image);
        //        return new BitmapImage(UriSource);
        //    }

        //}
    }
    
}
