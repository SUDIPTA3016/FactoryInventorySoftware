using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Diagnostics;


namespace FactoryInventorySoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String dbConnectionString = @"Data Source=test.db;Version=3;";
        Window3 next;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                String Query = "select * from addWin where auser = '" + this.txt_username.Text + "' and apass = '" + this.txt_password.Password + "' ";
                SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
                a.ExecuteNonQuery();
                SQLiteDataReader dr = a.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                    next = new Window3();

                    this.Close();
                    next.Show();
                }
                else
                {
                    MessageBox.Show("Username and password is not correct", "Incorrect", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

          }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_username.Focus();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }


        private void txt_password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                button1_Click(null, null);
            }

        }

       


        
      
        
    }
}
