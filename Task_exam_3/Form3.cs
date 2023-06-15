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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {

                var login = loginBox.Text;
                var password = passwordBox.Text;
                var name = nameBox.Text;

                if (DBHelper.GetInstance().addUser(login, password, name))
                {
                    MessageBox.Show("Успешно!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Пользователь не может быть добавлен, хуй знает почему, думайте.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
