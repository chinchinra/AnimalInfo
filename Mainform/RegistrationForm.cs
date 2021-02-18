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
    public partial class RegistrationForm : Form
    {
        //성별을 위한 변수
        String animalGender;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            animalGender = "수컷";
        }

        

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            animalGender = "암컷";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strConn = "Server=localhost;Database=bbscontents;Uid=kang;Pwd=123456;";

            // 정보 삽입을 위해 데이터 베이스 접속 후 삽입
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {

                conn.Open();
                String sqlComand = String.Format(" insert into customerinfo(name,phoneNumber,animalBirth,animalType," +
                                                    "animalGender,creationDate,animalName) " +
                                                    "values( '{0}' , '{1}' ,'{2}' ,'{3}','{4}','{5}','{6}')",
                                                    textBox1.Text, textBox2.Text, textBox4.Text, 
                                                    textBox5.Text,animalGender,DateTime.Now.ToString("yyyy-MM-dd"), textBox3.Text);
                MySqlCommand cmd = new MySqlCommand(sqlComand, conn);
                cmd.ExecuteNonQuery();
                // 삽입 후 모두 컨트롤 비워줌
                //textBox1.Clear();
                //textBox2.Clear();
                //textBox3.Clear();
                //textBox4.Clear();
                //textBox5.Clear();

                //animalGender = null;

                
            }
            //대화상자 종료
            MessageBox.Show("생성 완료");
            this.Close();
        }

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
