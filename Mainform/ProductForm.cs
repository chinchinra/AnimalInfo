using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mainform
{
    public partial class ProductForm : Form
    {



        private String name;
        private int price = new int();
        private ListViewItem[] listViewItems;
        private int totalPrice = 0;
        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            Init1();
            Init2();

        }
        public ListViewItem[] ListViewItems
        {
            get
            {
                return listViewItems;
            }
        }
        private void Init2()
        {
            listView2.Clear();
            ColumnHeader header1, header2, header3 ;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();

            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add(1);
            comboBox.Items.Add(2);
            comboBox.Items.Add(3);


            // Set the text, alignment and width for each column header.
            header1.Text = "상품명";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 100;

            header2.Text = "가격";
            header2.Width = 100;
            header2.TextAlign = HorizontalAlignment.Right;

            header3.Text = "phonenumber";
            header3.Width = 100;
            header3.TextAlign = HorizontalAlignment.Left;

            listView2.Columns.Add(header1);
            listView2.Columns.Add(header2);
            listView2.Columns.Add(header3);


            ListViewItem listViewItem;

            String strConn = "Server=localhost;Database=bbscontents;Uid=kang;Pwd=123456;";

            

            //정보 삽입을 위해 데이터 베이스 접속 후 삽입
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                String sqlComand = String.Format("select name,price from productinfo");
                MySqlCommand cmd = new MySqlCommand(sqlComand, conn);
                 
                MySqlDataReader mySql = cmd.ExecuteReader();

                if (mySql.HasRows)
                {
                    while (mySql.Read())
                    {
                        name = Convert.ToString(mySql["name"]);
                        price = Convert.ToInt32(mySql["price"]);
                        listViewItem = new ListViewItem(new String[] { name, String.Format("{0:#,###}", price) });
                        listView2.Items.Add(listViewItem);
                    }
                    mySql.Close();
                }
            }
        }
        private void Init1()
        {
            listView1.Clear();
            ColumnHeader header1, header2, header3;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "상품명";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 100;

            header2.Text = "가격";
            header2.Width = 100;
            header2.TextAlign = HorizontalAlignment.Right;

            header3.Text = "phonenumber";
            header3.Width = 100;
            header3.TextAlign = HorizontalAlignment.Left;

            listView1.Columns.Add(header1);
            listView1.Columns.Add(header2);
            listView1.Columns.Add(header3);

           

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            
            String me = listView2.SelectedItems[0].SubItems[0].Text;
            String me1 = listView2.SelectedItems[0].SubItems[1].Text;
            String me2 = "빈칸";
            String temp;

            temp = me1.Replace(",", "");

            totalPrice += Convert.ToInt32(temp);
            textBox1.Text = String.Format("{0:#,###}", totalPrice);
            ListViewItem listViewItem = new ListViewItem(new String[] { me, me1, me2 });
            listView1.Items.Add(listViewItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductFormAdd productFormAdd = new ProductFormAdd();
            productFormAdd.Owner = this;
            productFormAdd.ShowDialog();


            name = productFormAdd.Name;
            price = productFormAdd.Price;


            productFormAdd.Dispose();

            String strConn = "Server=localhost;Database=bbscontents;Uid=kang;Pwd=123456;";

            //정보 삽입을 위해 데이터 베이스 접속 후 삽입 , 상품 정보 삽입
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {

                conn.Open();
                String sqlComand = String.Format(" insert into productinfo(name,price)  values( '{0}' , {1})",name,price);
                MySqlCommand cmd = new MySqlCommand(sqlComand, conn);
                cmd.ExecuteNonQuery();
                
            }
            ListViewItem listViewItem = new ListViewItem(new String[] { name, Convert.ToString(price) });
            listView2.Items.Add(listViewItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                String temp = listView2.SelectedItems[0].SubItems[0].Text;
                listView2.SelectedItems[0].Remove();
                String strConn = "Server=localhost;Database=bbscontents;Uid=kang;Pwd=123456;";

                using (MySqlConnection conn = new MySqlConnection(strConn))
                {

                    conn.Open();
                    String sqlComand = String.Format(" delete from productinfo where name = '{0}'", name);
                    MySqlCommand cmd = new MySqlCommand(sqlComand, conn);
                    cmd.ExecuteNonQuery();

                }
            }
            catch(ArgumentOutOfRangeException exception)
            {
                MessageBox.Show(exception.Message + " 리스트 선택 필수");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if(listView1.Items.)
            listViewItems = new ListViewItem[listView1.Items.Count];
            for (int i = 0; i < listView1.Items.Count; i++)
            {

                String[] item = { listView1.Items[i].SubItems[0].Text, listView1.Items[i].SubItems[1].Text, listView1.Items[i].SubItems[2].Text };

                listViewItems[i] = new ListViewItem(item);

            }
            Close();
        }
    }
}
