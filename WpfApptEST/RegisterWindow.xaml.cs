using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private string fileImage = "";
        public RegisterWindow()
        {
            InitializeComponent();
        }
        MyDataContext data = new MyDataContext();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.ShowDialog();
            var uriSource = new Uri(openDialog.FileName);
            image.Source = new BitmapImage(uriSource);
            fileImage = openDialog.FileName;
        }
        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        string IMAGE;
        ObservableCollection<User> collection = null;
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email.Text)) MessageBox.Show("you didn't input an email!");
            else if (string.IsNullOrWhiteSpace(name.Text)) MessageBox.Show("you didn't input a name!");
            else if (string.IsNullOrWhiteSpace(surname.Text)) MessageBox.Show("you didn't input a surname!");
            else if (string.IsNullOrWhiteSpace(password.Password)) MessageBox.Show("you didn't input a password!");
            else if (string.IsNullOrWhiteSpace(repeat_password.Password)) MessageBox.Show("you didn't input a password second time!");

            else
            {
                if (!regex.IsMatch(email.Text))
                {
                    MessageBox.Show("error in email!");
                }
                else
                {
                    if (password.Password.Length < 8) { MessageBox.Show("passdword's too short!"); }
                    else
                    {
                        if (password.Password != repeat_password.Password)
                        {

                            MessageBox.Show("passdwords don't match!");

                        }

                        else
                        {
                            String dir = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "images");
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
                            string fileSaveName = System.IO.Path.GetRandomFileName() + ".jpg";


                            Bitmap bmp = new Bitmap(fileImage);
                            string imageSave = "";
                            string imageSave2 = "";
                            imageSave2 = System.IO.Path.Combine(dir, fileSaveName);
                            var bmpSave2 = ImageWorker.CompressImage(bmp, 50, 50);
                            bmpSave2.Save(imageSave2, ImageFormat.Jpeg);
                            for (int i = 1; i < 6; i++)
                            {

                                var bmpSave = ImageWorker.CompressImage(bmp, i * 50, i * 50);

                                imageSave = System.IO.Path.Combine(dir, $"{i * 50}_" + fileSaveName);

                                bmpSave.Save(imageSave, ImageFormat.Jpeg);

                            }

                            data.Users.Add(new User
                            {
                                Name = name.Text,
                                Surname = surname.Text,
                                //Surname = surname.Text,
                                Email = email.Text,
                                Password = password.Password,

                                Image = imageSave2
                            });
                            IMAGE = imageSave2;
                            data.SaveChanges();


                        }
                    }
                }
            }
            
                                    
            //grid.ItemsSource = imageList;

            

        }
        

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            
            if (collection == null)
            {
                collection = new ObservableCollection<User>();
                grid.ItemsSource = collection;
            }
            List<string> imageList = new List<string>();
            for (int i = 0; i < data.Users.Count(); i++)
            {
                collection.Add(new User() { Id = i + 1, Name = data.Users.ToArray()[i].Name, Phone = data.Users.ToArray()[i].Phone, Email = data.Users.ToArray()[i].Email, Image = data.Users.ToArray()[i].Image, Surname = data.Users.ToArray()[i].Surname, Password = data.Users.ToArray()[i].Password });
                //grid.Items.Add(new Test() { ImageSource = new BitmapImage(new Uri(imageSave2)) });
                //imageList.Add(data.Users.ToArray()[i].Image);
            };
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            data.Users.Remove(data.Users.ToArray()[grid.SelectedIndex]);
            data.SaveChanges();

        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null)
            {
                if (grid.SelectedItem is User)
                {

                    if (string.IsNullOrWhiteSpace(email.Text)) MessageBox.Show("you didn't input an email!");
                    else if (string.IsNullOrWhiteSpace(name.Text)) MessageBox.Show("you didn't input a name!");
                    else if (string.IsNullOrWhiteSpace(surname.Text)) MessageBox.Show("you didn't input a surname!");
                    else if (string.IsNullOrWhiteSpace(password.Password)) MessageBox.Show("you didn't input a password!");
                    else if (string.IsNullOrWhiteSpace(repeat_password.Password)) MessageBox.Show("you didn't input a password second time!");

                    else
                    {
                        if (!regex.IsMatch(email.Text))
                        {
                            MessageBox.Show("error in email!");
                        }
                        else
                        {
                            if (password.Password.Length < 8) { MessageBox.Show("passdword's too short!"); }
                            else
                            {
                                if (password.Password != repeat_password.Password)
                                {

                                    MessageBox.Show("passdwords don't match!");

                                }

                                else
                                {
                                    //User us = new User();
                                    //us.Name = name.Text;
                                    //us.Surname = surname.Text;
                                    //us.Email = email.Text;
                                    //us.Password = password.Password;
                                    //us.Image = data.Users.ToArray()[0].Image;
                                    //data.Users.ToArray()[grid.SelectedIndex] = us;
                                    //data.Users.Add(us);
                                    int d = grid.SelectedIndex;
                                    data.Users.ToArray()[d].Name = name.Text;
                                    data.Users.ToArray()[d].Email = email.Text;
                                    data.Users.ToArray()[d].Surname = surname.Text;
                                    data.Users.ToArray()[d].Password = password.Password;
                                    data.Users.ToArray()[d].Image = fileImage;
                                    data.SaveChanges();

                                }
                            }
                        }
                    }

                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email.Text)) MessageBox.Show("you didn't input an email!");
            else if (string.IsNullOrWhiteSpace(name.Text)) MessageBox.Show("you didn't input a name!");
            else if (string.IsNullOrWhiteSpace(surname.Text)) MessageBox.Show("you didn't input a surname!");
            else if (string.IsNullOrWhiteSpace(password.Password)) MessageBox.Show("you didn't input a password!");
            else if (string.IsNullOrWhiteSpace(repeat_password.Password)) MessageBox.Show("you didn't input a password second time!");

            else
            {
                if (!regex.IsMatch(email.Text))
                {
                    MessageBox.Show("error in email!");
                }
                else
                {
                    if (password.Password.Length < 8) { MessageBox.Show("passdword's too short!"); }
                    else
                    {
                        if (password.Password != repeat_password.Password)
                        {

                            MessageBox.Show("passdwords don't match!");

                        }

                        else
                        {
                            String dir = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "images");
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
                            string fileSaveName = System.IO.Path.GetRandomFileName() + ".jpg";


                            Bitmap bmp = new Bitmap(fileImage);
                            string imageSave = "";
                            string imageSave2 = "";
                            imageSave2 = System.IO.Path.Combine(dir, fileSaveName);
                            var bmpSave2 = ImageWorker.CompressImage(bmp, 50, 50);
                            bmpSave2.Save(imageSave2, ImageFormat.Jpeg);
                            for (int i = 1; i < 6; i++)
                            {

                                var bmpSave = ImageWorker.CompressImage(bmp, i * 50, i * 50);

                                imageSave = System.IO.Path.Combine(dir, $"{i * 50}_" + fileSaveName);

                                bmpSave.Save(imageSave, ImageFormat.Jpeg);

                            }

                            data.Users.Add(new User
                            {
                                Name = name.Text,
                                Surname = surname.Text,
                                //Surname = surname.Text,
                                Email = email.Text,
                                Password = password.Password,

                                Image = imageSave2
                            });
                            IMAGE = imageSave2;
                            data.SaveChanges();


                        }
                    }
                }
            }
            collection.Clear();
            if (collection == null)
            {
                collection = new ObservableCollection<User>();
                grid.ItemsSource = collection;
            }
            List<string> imageList = new List<string>();
            for (int i = 0; i < data.Users.Count(); i++)
            {
                collection.Add(new User() { Id = i + 1, Name = data.Users.ToArray()[i].Name, Email = data.Users.ToArray()[i].Email, Image = data.Users.ToArray()[i].Image, Surname = data.Users.ToArray()[i].Surname, Password = data.Users.ToArray()[i].Password });
                //grid.Items.Add(new Test() { ImageSource = new BitmapImage(new Uri(imageSave2)) });
                //imageList.Add(data.Users.ToArray()[i].Image);
            };
        }
    }        
}