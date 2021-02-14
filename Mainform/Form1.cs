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
    public partial class Form1 : Form
    {

        private static String name;
        private static String animalName;
        private static String phoneNumber;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void menu2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Owner = this;
            registrationForm.ShowDialog();
           
            registrationForm.Dispose();
        }

        private void menu1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            SearchForm searchForm = new SearchForm();
            searchForm.Owner = this;
            searchForm.ShowDialog();

            name = searchForm.Name;
            animalName = searchForm.AnimalName;
            phoneNumber = searchForm.PhoneNumber;

            //ListViewItem listViewItem = new ListViewItem(new String[] { name, animalName, phoneNumber });
            //listView1.Items.Add(listViewItem);

            textBox1.Text = name+" , "+animalName+" , "+phoneNumber;

            searchForm.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init1();
            Init2();
            splitContainer2.SplitterDistance = 400; 
        }
        public void Init1()
        {
           
            ColumnHeader header1, header2, header3;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "상품 및 서비스 명";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 120;

            header2.Text = "가격";
            header2.Width = 100;
            header2.TextAlign = HorizontalAlignment.Left;

            header3.Text = "수량";
            header3.Width = 40;
            header3.TextAlign = HorizontalAlignment.Left;

            listView1.Columns.Add(header1);
            listView1.Columns.Add(header2);
            listView1.Columns.Add(header3);
        }
        public void Init2()
        {
            ColumnHeader header1, header2, header3;
            header1 = new ColumnHeader();
            header2 = new ColumnHeader();
            header3 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "날짜";
            header1.TextAlign = HorizontalAlignment.Left;
            header1.Width = 120;

            header2.Text = "내용";
            header2.Width = 150;
            header2.TextAlign = HorizontalAlignment.Left;

            header3.Text = "금액";
            header3.Width = 100;
            header3.TextAlign = HorizontalAlignment.Left;

            listView2.Columns.Add(header1);
            listView2.Columns.Add(header2);
            listView2.Columns.Add(header3);
        }

        private void 상품ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.ShowDialog();

            // 입력이 아닌 닫기로 대화 상자 종료시 예외처리
            try
            {
                foreach (ListViewItem listViewItem in productForm.ListViewItems)
                {
                    listView1.Items.Add(listViewItem);
                }
            }
            catch (NullReferenceException exception) { }

            productForm.Dispose();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.SelectedItems[0].Remove();
        }

        private void 예약ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
