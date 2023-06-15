using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_exam_3
{
    public partial class Form2 : Form
    {

        public string str;
        public bool ok;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                var login = loginBox.Text;
                var password = passwordBox.Text;

                (str, ok) = DBHelper.GetInstance().checkUser(login, password);
                if (ok)
                {
                    MessageBox.Show("Успешно!");
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Неверные данные.");
                    this.DialogResult = DialogResult.No;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
