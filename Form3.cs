using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;

namespace FactoryInventorySoftware
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

        }
        CheckBox HeaderCheckBox;
        Boolean IsHeaderCheckBoxClicked = false;
        int TotalCheckedCheckBoxes = 0;

        int TotalCheckBoxes;
        //TotalCheckedCheckBoxes = 0;
        String dbConnectionString = @"Data Source=test.db;Version=3;";


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection con = new SQLiteConnection(dbConnectionString);
                con.Open();


                string sqlquery = "select Invoice_No, Cust_Id from Cust_Invoice";
                SQLiteCommand cmd = new SQLiteCommand(sqlquery, con);
                //cmd.CommandType = CommandType.Text;
                SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                // dt.Columns.Add("Invoice_No", typeof(int));
                //dt.Columns.Add("Cust_Id", typeof(string));
                //dt.Columns.Add("Invo_Date", typeof(DateTime));    
                adp.Fill(dt);
                bindingSource1.DataSource = dt;
                //dataGrid2.ItemsSource = dt.DefaultView;
                dataGridView1.DataSource = bindingSource1;

                //dataGridView1.DataBindings();
                //con.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            AddHeaderCheckBox();
            TotalCheckBoxes = dataGridView1.RowCount;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false; 
            HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
            HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dataGridView1_CurrentCellDirtyStateChanged);
            //  dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView1_CellPainting);
        }
        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();

            HeaderCheckBox.Size = new Size(15, 15);

            //Add the CheckBox into the DataGridView
            this.dataGridView1.Controls.Add(HeaderCheckBox);
        }
        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }
        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                HeaderCheckBoxClick((CheckBox)sender);
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsHeaderCheckBoxClicked)
                RowCheckBoxClick((DataGridViewCheckBoxCell)dataGridView1[e.ColumnIndex, e.RowIndex]);
        }
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            IsHeaderCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dataGridView1.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells["Select"]).Value = HCheckBox.Checked;

            dataGridView1.RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsHeaderCheckBoxClicked = false;
        }
        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modify Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                    TotalCheckedCheckBoxes++;
                else if (TotalCheckedCheckBoxes > 0)
                    TotalCheckedCheckBoxes--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                    HeaderCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                    HeaderCheckBox.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            List<DataGridViewRow> toRenew = new List<DataGridViewRow>();
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((Boolean)row.Cells[0].Value == true)
                {
                    toRenew.Add(row);
                }
            }
             
            */
            
            DataTable table = new System.Data.DataTable();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                table.Columns.Add(column.Name);
             foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((Boolean)row.Cells[0].Value == true)
                    {
                        DataRow datarw = table.NewRow();

                        for (int iCol = 1; iCol < dataGridView1.Columns.Count; iCol++)
                        {
                            datarw[iCol] = row.Cells[iCol].Value.ToString();
                        }

                        table.Rows.Add(datarw);
                    }
                }
                //return table;

             foreach (DataRow row1 in table.Rows)
             {

               // row1.Field<string>(1);

                SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
                try
                {
                    sqliteCon.Open();
                    String Query = "insert into Cust_Renew(Supply_No, Receive_Type, Cust_ID) values ('" + this.textBox1.Text + "', '" + this.textBox2.Text + "','"+row1.Field<string>(1)+"')";
                    SQLiteCommand a = new SQLiteCommand(Query, sqliteCon);
                    a.ExecuteNonQuery();

                   // MessageBox.Show("Successfully Added User Details", "Submitted", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    sqliteCon.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
             }
            
        }
    }
}
