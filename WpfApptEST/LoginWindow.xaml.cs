using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApptEST.Models;

namespace WpfApptEST
{
    
    public partial class LoginWindow : Window
    {
        MyDataContext data = new MyDataContext(); //connect to that file
        public LoginWindow()
        {
            //var users = data.User.ToList();
            //data.Users.Add(new User
            //{
            //    Name = "jorik"
            //});
            //data.SaveChanges();
            //var count = data.Users.Count();
            InitializeComponent();
        }
        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        ObservableCollection<User> collection = null;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email.Text)) { MessageBox.Show("you didn't fill EMAIL field !DO IT!"); }
            else if (string.IsNullOrWhiteSpace(password.Password.ToString())) { MessageBox.Show("you didn't fill PASSWORD field !DO IT!"); }
            else
            {
                if (!regex.IsMatch(email.Text))
                {
                    MessageBox.Show("error in email!");
                }
                else
                {
                    if (password.Password.Length < 8) { MessageBox.Show("error in password!"); }
                    else
                    {
                        int j = 0;
                        //int c = data.Users.Count();
                        //User[] arr = data.Users.ToArray();
                        grid.Visibility = Visibility.Visible;
                        add.Visibility = change.Visibility = delete.Visibility = Visibility.Visible;
                        for (int i = 0; i < data.Users.Count(); i++)
                        {
                            if (email.Text == data.Users.ToArray()[i].Email && password.Password == data.Users.ToArray()[i].Password)
                            {
                                if (collection == null)
                                {
                                    collection = new ObservableCollection<User>();
                                    grid.ItemsSource = collection;
                                }
                                List<string> imageList = new List<string>();
                                for (int h = 0; h < data.Users.Count(); h++)
                                {
                                    collection.Add(new User() { Id = h + 1, Name = data.Users.ToArray()[h].Name, Email = data.Users.ToArray()[h].Email, Image = data.Users.ToArray()[h].Image });
                                    //grid.Items.Add(new Test() { ImageSource = new BitmapImage(new Uri(imageSave2)) });
                                    //imageList.Add(data.Users.ToArray()[i].Image);
                                };

                            }


                            else
                            {
                                j += 1;
                            }

                        }

                        if (j == data.Users.Count())
                        {
                            MessageBox.Show("Oops! We don't have your account. Please sign in :)");

                        }


                    }
                }
            }
        }

        private void CreateAnAccount(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ListUsersWindow1 reg = new ListUsersWindow1();
            reg.ShowDialog();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
