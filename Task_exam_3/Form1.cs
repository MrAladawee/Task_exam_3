namespace Task_exam_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int user_id;

        private void auth_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            if(form2.ShowDialog() == DialogResult.OK)
            {
                auth_check.Text = "Вы авторизованы! \nВаш ID: " + form2.str;
                this.user_id = Convert.ToInt32(form2.str);
            }
            else
            {
                auth_check.Text = "Вы не авторизованы";
            }
        }

        private void reg_Click(object sender, EventArgs e)
        {
            var form3 = new Form3();
            form3.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dbh = DBHelper.GetInstance(
               "localhost",
               3306,
               "root",
               "",
               "mm_site2023"
               );
        }

        private void submit_Click(object sender, EventArgs e)
        {
            double[] coef = new double[9];
            double[] coef_res = new double[3];

            coef[0] = Convert.ToDouble(val_a.Text); 
            coef[1] = Convert.ToDouble(val_b.Text); 
            coef[2] = Convert.ToDouble(val_c.Text); 
            coef[3] = Convert.ToDouble(val_e.Text); 
            coef[4] = Convert.ToDouble(val_f.Text); 
            coef[5] = Convert.ToDouble(val_g.Text); 
            coef[6] = Convert.ToDouble(val_i.Text);
            coef[7] = Convert.ToDouble(val_j.Text);
            coef[8] = Convert.ToDouble(val_k.Text);

            coef_res[0] = Convert.ToDouble(val_d.Text);
            coef_res[1] = Convert.ToDouble(val_h.Text);
            coef_res[2] = Convert.ToDouble(val_l.Text);

            var result = Gauss.Solve(coef, coef_res);

            if (auth_check.Text != "Вы не авторизованы")
            {
                string result_string = Math.Round(result[0], 2).ToString() + " " + Math.Round(result[1], 2).ToString() + " " + Math.Round(result[2], 2).ToString();

                string coefs = coef[0].ToString() + " " + coef[1].ToString() + " " + coef[2].ToString() + " " +
                    coef[3].ToString() + " " + coef[4].ToString() + " " + coef[5].ToString() + " " + coef[6].ToString() + " " +
                    coef[7].ToString() + " " + coef[8].ToString();

                string coefs_res = coef_res[0].ToString() + " " + coef_res[1].ToString() + " " + coef_res[2].ToString();

                DBHelper.GetInstance().addResult(user_id, coefs, coefs_res, result_string);
                MessageBox.Show("Успешно!");

            }
            else
            {
                MessageBox.Show("Авторизуйся! Падаль");
            }
        }
    }
}