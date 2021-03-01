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
    public partial class BSTApplication : Form
    {
        int studentID;
        long phoneNum;
        int numCredits;
        double gpa;
        string fName;
        string lName;
        string email;
        string gender;
        string bDate;
        string status;

        int pcounter;
        int dcounter;
        

        public BSTApplication()
        {
            InitializeComponent();
            studentID = 0;
            phoneNum = 0;
            numCredits = 0;
            gpa = 0.0;
            fName = "";
            lName = "";
            email = "";
            gender = "";
            bDate = "";
            status = "";

            pcounter = 0;
            dcounter = 0;

            statusCombo.Items.Add("Freshman");
            statusCombo.Items.Add("Sophomore");
            statusCombo.Items.Add("Junior");
            statusCombo.Items.Add("Senior");

            genderCombo.Items.Add("Female");
            genderCombo.Items.Add("Male");
            genderCombo.Items.Add("Other");

        }


        private void EntryConditions(int errorCount, string errors)
        {
            //id
            if (idText.Text != null && idText.Text.All(char.IsDigit) && idText.Text.Length <= 30)
                studentID = Int32.Parse(idText.Text);
            else
            {
                errorCount++;
                errors = errors + "error: phone number not 10 characters ";
            }

            //credit hrs
            if (creditTxt.Text != null && Convert.ToDouble(creditTxt.Text) < 21.0 && Convert.ToDouble(creditTxt.Text) > 0.0)
                numCredits = Int32.Parse(creditTxt.Text);
            else
            {
                errorCount++;
                errors = errors + "error: credits invalid ";
            }

            //gpa
            if (gpaText.Text != null && Convert.ToDouble(gpaText.Text) < 5.0 && Convert.ToDouble(gpaText.Text) > 0.0)
                gpa = Convert.ToDouble(gpaText.Text);
            else
            {
                errorCount++;
                errors = errors + "error: GPA invalid ";
            }

            //phone #
            if (phoneText.Text.Length == 10 && phoneText.Text.All(char.IsDigit))
                phoneNum = Int64.Parse(phoneText.Text);
            else
            {
                errorCount++;
                errors = errors + "error: phone number not 10 characters ";
            }

            //fname
            if (fnameText.Text != null)
                fName = fnameText.Text;
            else
            {
                errorCount++;
                errors = errors + "error: field left empty ";
            }

            //lname
            if (lnameText.Text != null)
                lName = lnameText.Text;
            else
            {
                errorCount++;
                errors = errors + "error: field left empty ";
            }

            //gender
            if (genderCombo.Text != null)
                gender = genderCombo.Text.ToString();
            else
            {
                errorCount++;
                errors = errors + "error: field left empty ";
            }

            //status
            if (statusCombo.SelectedItem != null)
                status = statusCombo.SelectedItem.ToString();
            else
            {
                errorCount++;
                errors = errors + "error: field left empty ";
            }

            //email
            if (emailTxt.Text != null && emailTxt.Text.Length > 5 && emailTxt.Text.Substring(emailTxt.Text.Length - 4) == ".com")
                email = emailTxt.Text;
            else
            {
                errorCount++;
                errors = errors + "error: email must be longer than 5 characters ";
            }

            //bday
            if (bdayText.Text.Length == 10 && bdayText.Text[2] == '/' && bdayText.Text[5] == '/')
                bDate = bdayText.Text;
            else
            {
                errorCount++;
                errors = errors + "error:invalid birthday ";
            }
        }

        private bool FindApplicant(string ID)
        {
            bool found = false;
            string Command = $"SELECT * from Applicants WHERE studentID = @studentID";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=BST;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    cmd.Parameters.AddWithValue("@studentID", ID);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                        found = true;
                    connect.Close();
                }
            }
            return found;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            int errorCount = 0;
            string errors = "";

            bool oldApplicant = FindApplicant(idText.Text);
            if (oldApplicant == false)
            {
                //sees how many errors were made in entry
                EntryConditions(errorCount, errors);
                if (errorCount != 0)
                    alertText.Text = errors.ToString();
                else //if no errors
                {
                    bool ver = Verify(studentID);
                    bool eligibility = CheckEligibility(bDate, gpa, numCredits);
                    if (eligibility == true && ver == true)
                    {
                        SaveApplicantToApplicants();
                        SaveApplicantEligibility();
                        alertText.Text = "Your application has been saved!";
                    }
                    else if (eligibility == false && ver == false)
                    {
                        alertText.Text = "Sorry, you are not eligible, and it seems you are not in the system. ";
                        alertTxt2.Text = "An email has been sent to you with eligibility requirements.";
                    }
                    else if (eligibility == false)
                        alertText.Text = "Sorry, you are not eligible to apply. An email has been sent to you with eligibility conditions.";
                    else if (ver == false)
                        alertText.Text = "It seems you are not in the system.";
                }
            }
            else //if reusing the last ID#
                alertText.Text = "ID already belongs to an applicant";
        }

        //verifies
        private bool Verify(int studentID)
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
            int cred = 0;
            bool inSystem = true;

            string Command = $"SELECT * from Registrar WHERE studentID = {studentID}";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=Registrar;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentID);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
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
                        //studentID, fName, lName, phoneNum, email, gender, bDate, status, gpa, numCredits
                    }
                    else
                        Console.WriteLine("Error reading for verification");
                    
                    connect.Close();
                }
            }
            if (sID.ToString().Contains(studentID.ToString()) == false || studentID.ToString().Contains(sID.ToString()) == false)
                inSystem = false;
            if (fn.Trim().Contains(fName) == false || fName.Contains(fn.Trim()) == false)
                inSystem = false;
            if (ln.Trim().Contains(lName) == false || lName.Contains(ln.Trim()) == false)
                inSystem = false;
            if (pn.ToString().Contains(phoneNum.ToString()) == false || phoneNum.ToString().Contains(pn.ToString()) == false)
                inSystem = false;
            if (eml.Trim().Contains(email) == false || email.Contains(eml.Trim()) == false)
                inSystem = false;
            if (gndr.Trim().Contains(gender) == false || gender.Contains(gndr.Trim()) == false)
                inSystem = false;
            if (DOB.Trim().Contains(bDate) == false || bDate.Contains(DOB.Trim()) == false)
                inSystem = false;
            if (stat.Trim().Contains(status) == false || status.Contains(stat.Trim()) == false)
                inSystem = false;
            if (cGPA.ToString().Contains(gpa.ToString()) == false || gpa.ToString().Contains(cGPA.ToString()) == false)
                inSystem = false;
            if (cred.ToString().Contains(numCredits.ToString()) == false || numCredits.ToString().Contains(cred.ToString()) == false)
                inSystem = false;


            return inSystem;
            
        }
        
        private bool CheckEligibility(string bDate, double gpa, int numCredits)
        {
            bool elig = false;
            //@ most 23 y/o, min 3.2 gpa, @ least 12 hrs
            if (Convert.ToInt32(bDate.Substring(6)) >= 1997 && gpa >= 3.2 && numCredits >=12)
                elig =  true;
            else
                elig =  false;
            return elig;
        }
        private void SaveApplicantToApplicants()
        {
            string elig = "F";
            //send aplicant to BST db
            string Command = $"INSERT INTO Applicants (studentID, fName, lName, phoneNum, email, gender, dob, status, cumulativeGPA, creditHrs, eligibility) " +
                $"VALUES (@studentID, @fName, @lName, @phoneNum, @email, @gender, @bDate, @status, @gpa, @numCredits, @elig)";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=BST;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    cmd.Parameters.Add("@studentID", studentID);
                    cmd.Parameters.Add("@fName", fName);
                    cmd.Parameters.Add("@lName", lName);
                    cmd.Parameters.Add("@phoneNum", phoneNum);
                    cmd.Parameters.Add("@email", email);
                    cmd.Parameters.Add("@gender", gender);
                    cmd.Parameters.Add("@bDate", bDate);
                    cmd.Parameters.Add("@status", status);
                    cmd.Parameters.Add("@gpa", gpa);
                    cmd.Parameters.Add("@numCredits", numCredits);
                    cmd.Parameters.Add("@elig", elig);

                    int cmdResult = cmd.ExecuteNonQuery();
                    if (cmdResult < 0)
                        Console.WriteLine("INSERT error");
                    connect.Close();
                }
            }
        }

        private void SaveApplicantEligibility()
        {
            string Command = $"UPDATE Applicants SET eligibility = @elig WHERE studentID = @studentID";
            string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Database=BST;Trusted_Connection=True";
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                connect.Open();
                using (SqlCommand cmd = new SqlCommand(Command, connect))
                {
                    cmd.Parameters.Add("@elig", "T");
                    cmd.Parameters.Add("@studentID", studentID);
                    int cmdResult = cmd.ExecuteNonQuery();
                    if (cmdResult < 0)
                        Console.WriteLine("UPDATE error");
                    connect.Close();
                }
            }
        }





        private void phoneText_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void bdayText_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
