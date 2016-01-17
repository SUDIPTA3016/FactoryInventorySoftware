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
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data;

namespace FactoryInventorySoftware
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }
        SqlCommandBuilder bld;
        SqlDataAdapter adr;
        DataSet ds;

        String dbConnectionString = @"Data Source=test.db;Version=3;";
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                String Query = "insert into Cust_Renew values ('" + this.textBox3.Text + "', '" + this.textBox1.Text + "', '" + this.textBox2.Text + "', '" + this.datePicker1.Text + "', '" + this.textBox4.Text + "', '" + this.comboBox1.Text + "', '" + this.textBox5.Text + "', '" + this.comboBox2.Text + "','" + this.textBox6.Text + "','" + this.textBox7.Text + "','" + this.comboBox3.Text + "')";
                SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
                a.ExecuteNonQuery();

                MessageBox.Show("Successfull", "Submitted", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                sqliteCon.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
