using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mainform
{
    public partial class SearchForm : Form
    {
        private String name;
        private String animalName;
        private String phoneNumber;
        private String searchSelect;
        public SearchForm()
        {
            InitializeComponent();
        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            String strConn = "Server=localhost;Database=bbscontents;Uid=kang;Pwd=123456;";

            ListInit();
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                MySqlCommand cmd = null;
                MySqlDataReader msdr = null;

                try
                {
                    conn.Open();
                    String sqlComand = String.Format(" select * from customerinfo where {0} = '{1}'",searchSelect,textBox2.Text);
                                                   
                    cmd = new MySqlCommand(sqlComand, conn);
                    msdr = cmd.ExecuteReader();
                    if (msdr.HasRows)
                    {
                        while (msdr.Read())
                        {

                            name = Convert.ToString(msdr["name"]);
                            animalName = Convert.ToString(msdr["animalName"]);
                            phoneNumber = Convert.ToString(msdr["phoneNumber"]);

                            ListViewItem listViewItem = new ListViewItem(new String[] { name, animalName, phoneNumber });
                            listView1.Items.Add(listViewItem);

                        }

                        
                    }
                    else
                    {
                        //데이터 없을때 예외처리
                        throw new Exception();
                    }
                }
                catch(SqlException odbcEx)
                {
                    MessageBox.Show(odbcEx.Message);
                }
                catch (Exception ex)
                {
                    // Handle generic ones here.  
                    MessageBox.Show("데이터 없음");
                }
                
                msdr.Close();

            }
           
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {

            ListInit();
            radioButton1.Checked = true;

        }

        //listview controll initialize
        public void ListInit()
        {
            listView1.Clear();
            ColumnHeader header1, header2, header3;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "name";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 100;

            header2.Text = "animalName";
            header2.Width = 100;
            header2.TextAlign = HorizontalAlignment.Left;

            header3.Text = "phonenumber";
            header3.Width = 100;
            header3.TextAlign = HorizontalAlignment.Left;

            listView1.Columns.Add(header1);
            listView1.Columns.Add(header2);
            listView1.Columns.Add(header3);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        public String Name {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public String AnimalName
        {
            get
            {
                return animalName;
            }
        }
        public String PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
        }

        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            searchSelect = "name";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            searchSelect = "animalName";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            searchSelect = "phoneNumber";
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                button1_Click(sender, e);
            }
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {           
            if (name.Equals("SearchForm"))
            {
                name = "";
                animalName = "";
                phoneNumber = "";
            }
        }
    }
}
