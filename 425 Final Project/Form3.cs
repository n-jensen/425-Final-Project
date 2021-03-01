using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _425_Final_Project
{
    public partial class StartForm : Form
    {
        int pgChoice = 0;
        BSTApplication bstForm = new BSTApplication();
        CommiteeForm comForm = new CommiteeForm();
        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                questionLbl.Text = "Choose one please!";
            }
            else if (checkBox1.Checked == true)
            {
                bstForm.Show();
                this.Hide();
            }
            else if (checkBox2.Checked == true)
            {
                comForm.Show();
                this.Hide();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
