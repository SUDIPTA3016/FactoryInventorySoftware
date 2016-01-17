using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Drawing.Printing;


namespace FactoryInventorySoftware
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        //long count1 = 1;
        //long count2 = 2;
        String dbConnectionString = @"Data Source=test.db;Version=3;";
        private void button1_Click(object sender, EventArgs e)
        {

           // SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            try
            {

                SQLiteConnection con = new SQLiteConnection(dbConnectionString);
                con.Open();
                string sqlquery = "select * from Cust_Tab where Cust_ID = '" + this.textBox1.Text + "'";
                SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
                cmd.ExecuteNonQuery();
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.textBox2.Text = dr.GetValue(1).ToString();

                    this.textBox3.Text = dr.GetValue(2).ToString();
                    this.textBox4.Text = dr.GetValue(6).ToString();
                    //this.textBox5.Text = dr.GetValue(7).ToString();
                    //this.textBox6.Text = dr.GetValue(8).ToString();
                    //this.textBox7.Text = dr.GetValue(7).ToString();*/
                   
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
           // this.Hide();

            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
            con.Open();
            string sqlquery = "select Invoice_No from Cust_Invoice order by Invoice_No desc limit 1;";
            SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
            cmd.ExecuteNonQuery();
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                long q1 = Convert.ToInt64(dr.GetValue(0));
                f2.label20.Text = (q1+1).ToString();
                f2.label61.Text = (q1 + 2).ToString();
                //this.textBox10.Text = dr.GetValue(2).ToString();
               
            }
           

            f2.label63.Text = textBox5.Text.ToString();
            f2.label64.Text = textBox6.Text.ToString();
            f2.label65.Text = "40";
            f2.label66.Text = "40";
            int p11=int.Parse(textBox5.Text)*40;
            f2.label67.Text = p11.ToString();
            int p21 = int.Parse(textBox6.Text) * 40;
            f2.label68.Text = p21.ToString();
            int p31 = p11 + p21;
            f2.label71.Text = p31.ToString();
            int p41 = int.Parse(textBox5.Text) + int.Parse(textBox6.Text);
            f2.label69.Text = p41.ToString();
            f2.label62.Text = DateTime.Now.Month.ToString()+"/"+DateTime.Now.Day.ToString()+"/"+DateTime.Now.Year.ToString();
            f2.label72.Text = ConvertNumbertoWords(p31) + "ONLY";
            //f2.label61.Text = count1.ToString();
            //count1 = count1 + 2;
           // f2.Show();

            f2.label24.Text = textBox12.Text.ToString();
            f2.label25.Text = textBox13.Text.ToString();
            f2.label26.Text = "40";
            f2.label27.Text = "40";
            int p1 = int.Parse(textBox12.Text) * 40;
            f2.label30.Text = p1.ToString();
            int p2 = int.Parse(textBox13.Text) * 40;
            f2.label31.Text = p2.ToString();
            int p3 = p1 + p2;
            f2.label56.Text = p3.ToString();
            int p4 = int.Parse(textBox12.Text) + int.Parse(textBox13.Text);
            f2.label32.Text = p4.ToString();
            f2.label21.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            //f2.label20.Text = count1.ToString();
            int p5 = p1 + p2;
            f2.label56.Text = p5.ToString();
            f2.label33.Text = ConvertNumbertoWords(p5)+"ONLY";
            //count2 = count2 + 2;

            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                String Query = "insert into Cust_Invoice(Cust_Id,Invo_Voice) values('"+this.textBox8.Text+"','"+f2.label21.Text+"')";
                SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
                a.ExecuteNonQuery();

                String Query1 = "insert into Cust_Invoice(Cust_Id,Invo_Voice) values('" + this.textBox1.Text + "','" + f2.label62.Text + "')";
                SQLiteCommand a1 = new SQLiteCommand(Query1, sqliteCon);
                a1.ExecuteNonQuery();

                //MessageBox.Show("Successfull", "Successfully Added", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                
                sqliteCon.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }     


            f2.Show();
/*
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                String Query = "insert into Cust_Tab values ('" + this.textBox2.Text.Substring(0, 4) + this.textBox4.Text + "', '" + this.comboBox3.Text + "', '" + this.textBox2.Text + "', '" + this.datePicker1.Text + "', '" + this.textBox3.Text + "', '" + this.textBox5.Text + "', '" + this.textBox4.Text + "', '" + this.textBox6.Text + "','" + this.comboBox1.Text + "')";
                SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
                a.ExecuteNonQuery();

                MessageBox.Show("Successfull", "Submitted", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                sqliteCon.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   */        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
                con.Open();
                string sqlquery = "select sum(Supply_No) from Cust_Renew where Renew_Date > '" + this.dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and Supply_Type='Transparent'";
                SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
                cmd.ExecuteNonQuery();
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.textBox5.Text = dr.GetValue(0).ToString();
                                                          
                }
                string sqlquery1 = "select sum(Supply_No) from Cust_Renew where Renew_Date > '" + this.dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and Supply_Type='Blue'";
                SQLiteCommand cmd1 = new SQLiteCommand(sqlquery1, con);
                cmd1.ExecuteNonQuery();
                SQLiteDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    this.textBox6.Text = dr1.GetValue(0).ToString();
        
                }
                con.Close();
                int p1;
                int p2;
                if (string.IsNullOrWhiteSpace(this.textBox5.Text))
                {
                    this.textBox5.Text = "0";
                }
                if (string.IsNullOrWhiteSpace(this.textBox6.Text))
                {
                    this.textBox6.Text = "0";
                }
                p1 = Int32.Parse(this.textBox5.Text);
                p2 = Int32.Parse(this.textBox6.Text);
                int p3 = (p1 * 40) + (p2 * 40);
                this.textBox7.Text = p3.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
              try
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
                    this.textBox11.Text = dr.GetValue(6).ToString();
                    //this.textBox5.Text = dr.GetValue(7).ToString();
                    //this.textBox6.Text = dr.GetValue(8).ToString();
                    //this.textBox7.Text = dr.GetValue(7).ToString();*/
                   
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(dbConnectionString);
                con.Open();
                string sqlquery = "select sum(Supply_No) from Cust_Renew where Renew_Date > '" + this.dateTimePicker2.Value.ToString("MM/dd/yyyy") + "' and Supply_Type='Transparent'";
                SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
                cmd.ExecuteNonQuery();
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.textBox12.Text = dr.GetValue(0).ToString();
                                                          
                }
                string sqlquery1 = "select sum(Supply_No) from Cust_Renew where Renew_Date > '" + this.dateTimePicker2.Value.ToString("MM/dd/yyyy") + "' and Supply_Type='Blue'";
                SQLiteCommand cmd1 = new SQLiteCommand(sqlquery1, con);
                cmd1.ExecuteNonQuery();
                SQLiteDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    this.textBox13.Text = dr1.GetValue(0).ToString();
        
                }
                con.Close();
                int p1;
                int p2;
                if (string.IsNullOrWhiteSpace(this.textBox12.Text))
                {
                    this.textBox12.Text = "0";
                }
                if (string.IsNullOrWhiteSpace(this.textBox13.Text))
                {
                    this.textBox13.Text = "0";
                }
                p1 = Int32.Parse(this.textBox12.Text);
                p2 = Int32.Parse(this.textBox13.Text);
                int p3 = (p1 * 40) + (p2 * 40);
                this.textBox14.Text = p3.ToString();
        }

        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
       

        }
        }
        /*
        private void button3_Click(object sender, EventArgs e)
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            try
            {
                sqliteCon.Open();
                String Query = "insert into Cust_Tab values ('" + this.textBox2.Text.Substring(0, 4) + this.textBox4.Text + "', '" + this.comboBox3.Text + "', '" + this.textBox2.Text + "', '" + this.datePicker1.Text + "', '" + this.textBox3.Text + "', '" + this.textBox5.Text + "', '" + this.textBox4.Text + "', '" + this.textBox6.Text + "','" + this.comboBox1.Text + "')";
                SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
                a.ExecuteNonQuery();

                MessageBox.Show("Successfull", "Submitted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                sqliteCon.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        

        

       
      
    

       
    

