using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mainform
{
    public partial class ProductFormAdd : Form
    {

        private String name;
        private int price;

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public ProductFormAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            name = textBox1.Text;
            price = Convert.ToInt32(textBox2.Text);


            String strConn = "Server=localhost;Database=bbscontents;Uid=kang;Pwd=123456;";

            //정보 삽입을 위해 데이터 베이스 접속 후 삽입
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {

                conn.Open();
                String sqlComand = String.Format("select name from productinfo where name = '{0}'",name);
                MySqlCommand cmd = new MySqlCommand(sqlComand, conn);

                MySqlDataReader mySql = cmd.ExecuteReader();

                if (mySql.HasRows)
                {
                    MessageBox.Show("이미 존재하는 상품");
                }
                else
                {
                    mySql.Close();
                    Close();
                }

            }


            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
