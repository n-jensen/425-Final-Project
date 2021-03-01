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

namespace _425_Final_Project
{
    public partial class VoteForm : Form
    {
        string final1;
        string final2;
        public VoteForm(string finalist1, string finalist2)
        {
            final1 = finalist1;
            final2 = finalist2;
            InitializeComponent();

            voteList.Items.Add(final1);
            voteList.Items.Add(final2);
        }

        private void voteBtn_Click(object sender, EventArgs e)
        {
            if (vote1Txt.Text != null && vote2Txt.Text != null & vote3Txt.Text != null)
            {
                int v1 = Convert.ToInt32(vote1Txt.Text);
                int v2 = Convert.ToInt32(vote2Txt.Text);
                int v3 = Convert.ToInt32(vote3Txt.Text);
                SendVotesToDB(v1, v2, v3);
                alertLbl.Text = "Thank you for voting!";
            }
            else
                voteBtn.Text = "Oops, try again.";
        }

        private void SendVotesToDB(int v1, int v2, int v3)
        {
            string Command = $"INSERT INTO CommiteeVotes (vote1, vote2, vote3) " +
                $"VALUES(@v1, @v2, @v3)";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=Votes;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    cmd.Parameters.AddWithValue("@v1", v1);
                    cmd.Parameters.AddWithValue("@v2", v2);
                    cmd.Parameters.AddWithValue("@v3", v3);

                    cmd.ExecuteNonQuery();
                    connect.Close();
                }
            }
        }
    }
}
