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
    public partial class CommiteeForm : Form
    {
        Students winner;
        List<Students> applicantList = new List<Students>(100);
        int voteRequest;

        List<Students> gpaFinalistList;
        List<Students> sgpaFinalistList;
        List<Students> jrFinalistList;
        List<Students> femaleFinalistList;
        List<Students> yngFinalistList;
        List<Students> intrvwFinalistList;

        List<Students> finalists = new List<Students>();

        public CommiteeForm()
        {
            InitializeComponent();

            SetVisibility(false);
            winner = new Students(0, "", "", 0, "", "","", "", 0,0,0);

            voteRequest = 0;

            gpaFinalistList = new List<Students>();
            sgpaFinalistList = new List<Students>();
            jrFinalistList = new List<Students>();
            femaleFinalistList = new List<Students>();
            yngFinalistList = new List<Students>();
            intrvwFinalistList = new List<Students>();

            GetAllUsers();
        }

        private void SetVisibility(bool visible)
        {
            questionLbl.Visible = visible;
            label13.Visible = visible;
            label12.Visible = visible;
            label11.Visible = visible;
            label10.Visible = visible;

            voteList.Visible = visible;
            vote1Txt.Visible = visible;
            vote2Txt.Visible = visible;
            vote3Txt.Visible = visible;

            voteBtn.Visible = visible;
        }

        private void FillListUsingStudents(ListBox lbox, List<Students> slist)
        {
            foreach (Students s in slist)
            {
                lbox.Items.Add(s.studentID.ToString() + "   " + s.fName.Trim()
                    + "   " + s.lName.Trim() + "   " + s.phoneNum.ToString()
                    + "   " + s.email.Trim() + "   " + s.gender.Trim()
                    + "   " + s.bDate.Trim() + "   " + s.status.Trim()
                    + "   " + s.cgpa.ToString() + "   " + s.numCredits.ToString());
            }

        }

        private void FillListUsingStrings(ListBox lbox, List<string> slist)
        {
            foreach (string s in slist)
            {
                lbox.Items.Add(s);
            }
        }

        private void winnerButton_Click(object sender, EventArgs e)
        {

            if (voteRequest == 0)
            {

                CalculateScholarshipWinner();
                List<string> finalistNotes = GetFinalists();
                FillListUsingStudents(appListbox, applicantList);
                FillListUsingStrings(finalistList, finalistNotes);
                if (voteRequest == 1)//voteRequest changes after calculation
                {
                    SetVisibility(true);
                    string votingS1 = yngFinalistList[0].studentID.ToString() + "   " + yngFinalistList[0].fName.Trim()
                        + "   "  + yngFinalistList[0].lName.Trim() + "   " + yngFinalistList[0].phoneNum.ToString() 
                        + "   " + yngFinalistList[0].email.Trim() + "   " + yngFinalistList[0].gender.Trim()
                        + "   " + yngFinalistList[0].bDate.Trim() + "   "  + yngFinalistList[0].status.Trim()
                        + "   " + yngFinalistList[0].cgpa.ToString() + "   " + yngFinalistList[0].numCredits.ToString();
                    string votingS2 = yngFinalistList[1].studentID.ToString() + "   " + yngFinalistList[1].fName.Trim()
                        + "   " + yngFinalistList[1].lName.Trim() + "   " + yngFinalistList[1].phoneNum.ToString()
                        + "   " + yngFinalistList[1].email.Trim() + "   " + yngFinalistList[1].gender.Trim()
                        + "   " + yngFinalistList[1].bDate.Trim() + "   " + yngFinalistList[1].status.Trim()
                        + "   " + yngFinalistList[1].cgpa.ToString() + "   " + yngFinalistList[1].numCredits.ToString();
                    voteList.Items.Add(votingS1);
                    voteList.Items.Add(votingS2);
                }
                else if(voteRequest == 0)
                {
                    winnerBox.Text = winner.studentID.ToString() + "   " + winner.fName.Trim() + "   " + winner.lName.Trim() + "   " 
                        + winner.phoneNum.ToString() + "   " + winner.email.Trim() + "   " + winner.gender.Trim() + "   " + winner.bDate.Trim() + "   " 
                        + winner.status.Trim() + "   " + winner.cgpa.ToString() + "   " + winner.numCredits.ToString();
                    GenerateEmails();
                    ProcessAward();
                
                }
            }            
        }

        private void GetAllUsers()
        {
            int sID = 0;
            string fn = "";
            string ln = "";
            long pn = 0;
            string eml = "";
            string gndr = "";
            string DOB = "";
            string stat = "";
            double cGPA = 0.0;
            double sGPA = 0.0;
            int cred = 0;
            int indexr = 0;

            string Command = $"SELECT * from Applicants";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=BST;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        sID = Convert.ToInt32(rdr["studentID"]);
                        fn = rdr["fName"].ToString();
                        ln = rdr["lName"].ToString();
                        pn = Convert.ToInt64(rdr["phoneNum"].ToString());
                        eml = rdr["email"].ToString();
                        gndr = rdr["gender"].ToString();
                        DOB = rdr["dob"].ToString();
                        stat = rdr["status"].ToString();
                        cGPA = Convert.ToDouble(rdr["cumulativeGPA"]);
                        cred = Convert.ToInt32(rdr["creditHrs"]);
                        applicantList.Add(new Students(sID, fn, ln, pn, eml, gndr, DOB, stat, cGPA, cGPA, cred));
                    }
                    connect.Close();
                }
            }
        }

        private Students FindApplicantWithId(int id)
        {
            Students found = new Students(0,"","",0,"","","","",0,0,0);
            foreach(Students s in applicantList)
            {
                if (s.studentID == id)
                    found = s;
            }
            return found;
        }

        private void CalculateScholarshipWinner()
        {
            if (voteRequest == 0)
            {
                double tempGpa = 0.0;
                for (int i = 0; i < applicantList.Count; i++)
                {//find highest gpa
                    if (applicantList[i].cgpa > tempGpa)
                        tempGpa = applicantList[i].cgpa;
                }
                for (int i = 0; i < applicantList.Count; i++)
                {//collect gpa finalists who all have highest gpa
                    if (applicantList[i].cgpa == tempGpa)
                        gpaFinalistList.Add(applicantList[i]);
                }

                double tempSGPA = 0;
                //if only 1 gpa finalist:
                if (gpaFinalistList.Count == 1)
                    winner = applicantList[0];
                else if (gpaFinalistList.Count > 1)
                {   //find highest sgpa
                    for (int i = 0; i < gpaFinalistList.Count; i++)
                    {
                        if (gpaFinalistList[i].sgpa > tempSGPA)
                            tempSGPA = gpaFinalistList[i].sgpa;
                    }
                    //then collect sgpa finalists
                    for (int i = 0; i < gpaFinalistList.Count; i++)
                    {
                        if (gpaFinalistList[i].sgpa == tempSGPA)
                            sgpaFinalistList.Add(gpaFinalistList[i]);
                    }
                }

                //if only 1 sgpafinalist
                if (sgpaFinalistList.Count == 1)
                    winner = sgpaFinalistList[0];
                else if (sgpaFinalistList.Count > 1)
                {
                    //find jr, fill jrFinalistList
                    for (int i = 0; i < sgpaFinalistList.Count; i++)
                    {
                        if (sgpaFinalistList[i].status == "Junior")
                            jrFinalistList.Add(sgpaFinalistList[i]);
                    }
                }
                else if (sgpaFinalistList.Count == 0)
                {
                    for (int i = 0; i < gpaFinalistList.Count; i++)
                    {
                        if (gpaFinalistList[i].status == "Junior")
                            jrFinalistList.Add(gpaFinalistList[i]);
                    }
                }

                //if only 1 jr finalist
                if (jrFinalistList.Count == 1)
                    winner = jrFinalistList[0];
                else if (jrFinalistList.Count > 1)
                {
                    //fill femaleFinalistList
                    for (int i = 0; i < jrFinalistList.Count; i++)
                    {
                        if (jrFinalistList[i].gender == "Female")
                            femaleFinalistList.Add(jrFinalistList[i]);
                    }
                }
                else if (jrFinalistList.Count == 0 && sgpaFinalistList.Count ==0)
                {
                    for (int i = 0; i < gpaFinalistList.Count; i++)
                    {
                        if (gpaFinalistList[i].gender == "Female")
                            femaleFinalistList.Add(gpaFinalistList[i]);
                    }
                }
                else if (jrFinalistList.Count == 0)
                {
                    for (int i = 0; i < sgpaFinalistList.Count; i++)
                    {
                        if (sgpaFinalistList[i].gender.Contains("Female"))
                            femaleFinalistList.Add(sgpaFinalistList[i]);
                    }
                }
                //if only 1 female
                if (femaleFinalistList.Count == 1)
                    winner = femaleFinalistList[0];
                else if (femaleFinalistList.Count > 1)
                {
                    GetYoungest(femaleFinalistList, yngFinalistList);
                    voteRequest = 1;
                }

            }


        }

        private void GetYoungest(List<Students> femaleFinalistList, List<Students> yngFinalistList)
        {
            int tempYngst = 1997;
            int tempYngstMnth = 1;

            int day1 = 0;
            int yngrday2 = 0;

            //find youngest 2
            //voteRequest = 1;
            for (int i = 0; i < femaleFinalistList.Count; i++)
            {
                int yr = Convert.ToInt32(femaleFinalistList[i].bDate.Substring(6));
                if (yr > tempYngst)
                    tempYngst = yr;
            }
            List<Students> yngsYrs = new List<Students>();
            for (int i = 0; i < femaleFinalistList.Count; i++)
            {//get all students with youngest year
                int yr = Convert.ToInt32(femaleFinalistList[i].bDate.Substring(6));
                if (yr == tempYngst)
                    yngsYrs.Add(femaleFinalistList[i]);
            }
            List<Students> yngsMnths = new List<Students>();
            if (yngsYrs.Count == 1 || yngsYrs.Count == 2)
            {
                foreach (Students y in yngsYrs)
                    yngFinalistList.Add(y);
            }
            else if (yngsYrs.Count > 2)
            {//find youngest month
                for (int i = 0; i < yngsYrs.Count; i++)
                {
                    int mnth = Convert.ToInt32(yngsYrs[i].bDate.Substring(0, 2));
                    if (mnth > tempYngstMnth)
                        tempYngstMnth = mnth;
                }
                for (int i = 0; i < yngsYrs.Count; i++)
                {
                    int mnth = Convert.ToInt32(yngsYrs[i].bDate.Substring(0, 2));
                    if (tempYngstMnth == mnth)
                        yngsMnths.Add(yngsYrs[i]);
                }
                if (yngsMnths.Count == 1 || yngsMnths.Count == 2)
                {
                    foreach (Students y in yngsMnths)
                        yngFinalistList.Add(y);
                }
                else if (yngsMnths.Count > 2)
                {//find youngest day
                    for (int i = 0; i < yngsMnths.Count; i++)
                    {
                        int day = Convert.ToInt32(yngsMnths[i].bDate.Substring(3, 2));
                        if (day > day1)
                        {
                            day1 = day;
                            if (day > yngrday2)
                            {
                                day1 = yngrday2;
                                yngrday2 = day;
                            }
                        }
                    }
                    if (day1 != 0 && yngrday2 != 0)
                    {
                        foreach (Students f in yngsMnths)
                        {
                            int day = Convert.ToInt32(f.bDate.Substring(3, 2));
                            if (day == day1 || day == yngrday2)
                                yngFinalistList.Add(f);
                        }
                    }
                    else
                        day1 = yngrday2 = -1;
                }
            }

            

        }

        private List<string> GetFinalists()
        {
            if (yngFinalistList.Count != 0)
                finalists = yngFinalistList;
            else if (femaleFinalistList.Count != 0)
                finalists = femaleFinalistList;
            else if (jrFinalistList.Count != 0)
                finalists = jrFinalistList;
            else if (sgpaFinalistList.Count != 0)
                finalists = sgpaFinalistList;
            else if (gpaFinalistList.Count != 0)
                finalists = gpaFinalistList;

            List<string> finalistsString = new List<string>();
            for (int i = 0; i < finalists.Count; i++)
            {
                Students f = finalists[i];
                finalistsString.Add( f.studentID.ToString() + "   " + f.fName.Trim() + "   " 
                    + f.lName.Trim() + "   " + f.phoneNum.ToString() + "   " 
                    + f.email.Trim() + "   " + f.gender.Trim() + "   " 
                    + f.bDate.Trim() + "   " + f.status.Trim() + "   " 
                    + f.cgpa.ToString() + "   " + f.numCredits.ToString());
            }

            return finalistsString;
        }

        private List<int> GetVotes()
        {
            List<int> votes = new List<int>();
            string Command = $"SELECT * from CommiteeVotes";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=Votes;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while(rdr.Read())
                    {
                        votes.Add(Convert.ToInt32(rdr["vote1"]));
                        votes.Add(Convert.ToInt32(rdr["vote2"]));
                        votes.Add(Convert.ToInt32(rdr["vote3"]));
                    }
                    connect.Close();
                }
            }
            return votes;
        }

        private void GenerateEmails()
        {
            List<string> emails = new List<string>();
            string winnerEmail = "";
            for (int i = 0; i < applicantList.Count; i++)
            {
                string name = applicantList[i].fName;
                string email = applicantList[i].email;
                if (applicantList[i].studentID != winner.studentID)
                    emails.Add("(to: " + email + ") Hello, " + name + ", this email is to inform you that you did not meet the criteria to win the B.S.T. Smart Scholarship. Thank you and try again next semester!");
                else
                    winnerEmail = ("(to: " + email + ") Hello, " + name + ", this email is to inform you that you have met the criteria to win the B.S.T. Smart Scholarship. Your award will be equal to the cost of the tuition you last paid. Congratulations!");
            }
            foreach (string e in emails)
                uaBox.Items.Add(e);
            wBox.Items.Add(winnerEmail);
        }

        private void ProcessAward()
        {
            double dollars = 0.0;
            string Command = $"SELECT * from Registrar WHERE studentID = @id";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=Registrar;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    int wid = winner.studentID;
                    cmd.Parameters.Add("@id", wid);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.Read())
                        dollars = Convert.ToDouble(rdr["billAmtPaid"]);
                    connect.Close();
                }
            }

            moneyBox.Items.Add("$"+dollars.ToString());
        }

        private void voteBtn_Click(object sender, EventArgs e)
        {
            if (vote1Txt.Text != null && vote2Txt.Text != null & vote3Txt.Text != null)
            {
                int v1 = Convert.ToInt32(vote1Txt.Text);
                int v2 = Convert.ToInt32(vote2Txt.Text);
                int v3 = Convert.ToInt32(vote3Txt.Text);
                SendVotesToDB(v1, v2, v3);
                voteBtn.Text = "Thank you for voting!";
            }
            else
                voteBtn.Text = "Oops, try again.";


            if (voteRequest == 1)
            {

                List<int> votes = GetVotes();
                int tempVote1 = votes[0];
                int tempVote2 = 0;
                int countTemp1 = 0;
                int countTemp2 = 0;
                int winnerId = 0;
                foreach (int v in votes)
                {//gets the 2 voted ids
                    if (tempVote1 != v && tempVote2 != v)
                        tempVote2 = v;
                }
                foreach (int v in votes)
                {//counts the votes
                    if (v == tempVote1)
                        countTemp1++;
                    else if (v == tempVote2)
                        countTemp2++;
                }

                if (countTemp1 > countTemp2)
                    winnerId = tempVote1;
                else if (countTemp2 > countTemp1)
                    winnerId = tempVote2;
                winner = FindApplicantWithId(winnerId);
                string temp = winner.studentID.ToString() + " " + winner.fName.Trim() + " " + winner.lName.Trim() + " "
                        + winner.phoneNum.ToString() + " " + winner.email.Trim() + " " + winner.gender.Trim() + " " + winner.bDate.Trim() + " "
                        + winner.status.Trim() + " " + winner.cgpa.ToString() + " " + winner.numCredits.ToString();
                winnerBox.Items.Add(temp);
                GenerateEmails();
                ProcessAward();

            }

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

