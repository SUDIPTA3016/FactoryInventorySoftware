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
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Collections;
using System.Windows.Controls.Primitives;

using System.Diagnostics;
using System.Windows.Navigation;
using System.Windows.Interop;



namespace FactoryInventorySoftware
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        SqlCommandBuilder bld;
        SqlDataAdapter adr;
        DataSet ds;
        List<int> lstSelectedEmpNo;

        String dbConnectionString = @"Data Source=test.db;Version=3;";
        public Window3()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lstSelectedEmpNo = new List<int>();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
   try{
            sqliteCon.Open();
            String Query = "insert into Cust_Tab values ('" + this.textBox2.Text.Substring(0,4) + this.textBox5.Text + "', '" + this.comboBox3.Text + "', '" + this.textBox2.Text + "', '" + this.datePicker1.Text + "', '" + this.textBox4.Text + "', '" + this.textBox5.Text + "', '" + this.textBox8.Text + "', '" + this.textBox6.Text + "','" + this.comboBox1.Text + "')";
            SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
            a.ExecuteNonQuery();
            
            MessageBox.Show("Successfully Added User Details","Submitted",MessageBoxButton.OK,MessageBoxImage.Asterisk);
            sqliteCon.Close();
        }

   catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
    //this.datePicker1.Text = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss.fff tt");

           }

     

       

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            con.Open();
            string sqlquery = "select * from Cust_Tab where Cust_Name = '" + this.textBox7.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
            SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGrid1.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DateTime.Now.ToString("dd-MM-yyyy  hh-mm-ss-tt") + ".pdf";
            PdfPTable table = new PdfPTable(9);
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 20, 20, 62, 55);
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(path, System.IO.FileMode.Create));
            doc.Open();
            iTextSharp.text.Paragraph head = new iTextSharp.text.Paragraph("ORDER SUBMISSION" + Environment.NewLine, FontFactory.GetFont(FontFactory.TIMES, 16, iTextSharp.text.Font.BOLD));
            head.Alignment = Element.ALIGN_CENTER;
            doc.Add(head);
            iTextSharp.text.Paragraph head1 = new iTextSharp.text.Paragraph("     ", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.BOLD));
            head1.Alignment = Element.ALIGN_CENTER;
            doc.Add(head1);
            doc.AddTitle("ORDER SUBMISSION");
            for (int j = 1; j < 10; j++)
            {
                table.AddCell(new Phrase(dataGrid1.Columns[j].Header.ToString()));
            }
            table.HeaderRows = 1;
            IEnumerable itemsSource = dataGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    DataGridRow row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = FindVisualChild4<DataGridCellsPresenter>(row);
                        for (int i = 0; i < dataGrid1.Columns.Count; ++i)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                table.AddCell(new Phrase(txt.Text));
                            }
                        }
                    }
                }

                doc.Add(table);
                doc.Close();
            }
            MessageBox.Show("PDF file Created on Desktop", "PDF", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public static IEnumerable<T> FindVisualChildren4<T>(DependencyObject depObj)
               where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren4<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static childItem FindVisualChild4<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren4<childItem>(obj))
            {
                return child;
            }

            return null;
        }

        private void textBox7_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox1.Text = textBox2.Text.Substring(0,4) + textBox5.Text;
        }
/*
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            con.Open();
            string sqlquery = "select * from Cust_Tab where Cust_ID = '" + this.textBox8.Text + "'";
            SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
            cmd.ExecuteNonQuery();
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                this.textBox9.Text = dr.GetValue(1).ToString();
                this.textBox10.Text = dr.GetValue(2).ToString();
                this.textBox11.Text = dr.GetValue(0).ToString();
                this.textBox12.Text = dr.GetValue(8).ToString();
                this.textBox14.Text = dr.GetValue(6).ToString();
                this.textBox15.Text = dr.GetValue(7).ToString();
            }
            
            con.Close();



        }*/

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                String Query = "update Jar_Info set " + this.comboBox4.Text + "=" + this.comboBox4.Text + " +'" + this.textBox16.Text + "'";
                SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
                a.ExecuteNonQuery();

                MessageBox.Show("Successfully Added New Jars", "Successfully Added ", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                sqliteCon.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            con.Open();
            string sqlquery = "select * from Jar_Info";
            SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
            cmd.ExecuteNonQuery();
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                this.label17.Content = "Transparent Jar";
                this.label19.Content = "Blue Jar";
                this.label18.Content= dr.GetValue(0).ToString();
                this.label20.Content = dr.GetValue(1).ToString();
                //this.label22.Content = "Total Jar";
                this.label24.Content = " Damage Transparent Jar";
                this.label26.Content = "Damage Blue Jar";
                /*double tran = (double)dr.GetValue(0);
                double blue = (double)dr.GetValue(1);
                double qtn;
                qtn= tran+blue;
                this.label23.Content = qtn.ToString();*/
                this.label25.Content = dr.GetValue(2).ToString();
                this.label27.Content = dr.GetValue(3).ToString();    

            }

            con.Close();

        }

        private void button8_Click_1(object sender, RoutedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            try
            {

                con.Open();
                string sqlquery = "select * from Cust_Tab where Cust_Type = '" + this.comboBox2.Text + "'";
                SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
                SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_edit4(object sender, RoutedEventArgs e)
        {
            Window2 w2 = new Window2();
            w2.Show();
            string PK_ID;
            var id1 = (DataRowView)dataGrid1.SelectedItem;
            PK_ID = Convert.ToString(id1.Row["Cust_ID"].ToString());

            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            con.Open();
            string sqlquery = "select * from Cust_Tab where Cust_ID='" + PK_ID + "' ";
            SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
            SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            w2.textBox1.Text = dt.Rows[0]["Cust_Name"].ToString();
            w2.textBox2.Text = dt.Rows[0]["Cust_Phn"].ToString();
            w2.textBox3.Text = dt.Rows[0]["Cust_ID"].ToString();
            con.Close();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
             SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            con.Open();
            string sqlquery = "select sum(Supply_No) from Cust_Renew where Renew_Date='"+this.datePicker2.ToString().Substring(0,10)+"' and Supply_Type='Transparent'";
            SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
            cmd.ExecuteNonQuery();
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                this.textBox9.Text = dr.GetValue(0).ToString();    

            }
            
            string sqlquery1 = "select sum(Supply_No) from Cust_Renew where Renew_Date='" + this.datePicker2.ToString().Substring(0, 10) + "' and Supply_Type='Blue'";
            SQLiteCommand cmd1 = new SQLiteCommand(sqlquery1, con);
            cmd1.ExecuteNonQuery();
            SQLiteDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                this.textBox10.Text = dr1.GetValue(0).ToString();

            }

            string sqlquery2 = "select sum(Receive_No) from Cust_Renew where Renew_Date='" + this.datePicker2.ToString().Substring(0, 10) + "' and Receive_Type='Transparent'";
            SQLiteCommand cmd2 = new SQLiteCommand(sqlquery2, con);
            cmd2.ExecuteNonQuery();
            SQLiteDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                this.textBox11.Text = dr2.GetValue(0).ToString();

            }

            string sqlquery3 = "select sum(Receive_No) from Cust_Renew where Renew_Date='" + this.datePicker2.ToString().Substring(0, 10) + "' and Receive_Type='Blue'";
            SQLiteCommand cmd3 = new SQLiteCommand(sqlquery3, con);
            cmd3.ExecuteNonQuery();
            SQLiteDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                this.textBox12.Text = dr3.GetValue(0).ToString();

            }

            con.Close();

        

        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            {
                SQLiteConnection con = new SQLiteConnection(dbConnectionString);
                con.Open();
                string sqlquery = "select Invoice_No, Cust_Id from Cust_Invoice where Invo_Voice='"+this.datePicker3.SelectedDate.ToString().Substring(0,10)+"'";
                SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
                SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                //dt.Columns.Add("Invoice_No", typeof(int));
               // dt.Columns.Add("Cust_Id", typeof(string));
                //dt.Columns.Add("Invo_Date", typeof(DateTime));    
                adp.Fill(dt);
                dataGrid2.ItemsSource = dt.DefaultView;
                con.Close();
            }
            

        }


        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            FrameworkElement element = dataGrid1.Columns[1].GetCellContent(e.Row);
            if (element.GetType() == typeof(CheckBox))
            {
                if (((CheckBox)element).IsChecked == true)
                {
                    FrameworkElement cellCustId =
                    dataGrid1.Columns[2].GetCellContent(e.Row);
                    int CustId = Convert.ToInt32(((TextBlock)cellCustId).Text);
                    lstSelectedEmpNo.Add(CustId);
                }
            }
        }

       
       

         private void button11_Click_1(object sender, RoutedEventArgs e)
         {
             try
             {

                 if (lstSelectedEmpNo.Count > 0)
                 {
                     int count = 0;
                     foreach (int eno in lstSelectedEmpNo)
                     {

                         count++;
                     }
                     MessageBox.Show(count + "Row's Selected");
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
         }

         private void button12_Click(object sender, RoutedEventArgs e)
         {
             Form3 form = new Form3();
             form.Show();
         }
        
       
            
    }
}

       