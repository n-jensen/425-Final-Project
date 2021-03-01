using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _425_Final_Project
{
    class Students
    {
        public int studentID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public long phoneNum { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string bDate { get; set; }
        public string status { get; set; }
        public double cgpa { get; set; }
        public double sgpa { get; set; }

        public int numCredits { get; set; }
       
        public Students(int s, string f, string l, long p, string e, string g, string b, string sts, double CGPA, double SGPA, int n)
        {
            studentID = s;
            fName = f;
            lName = l;
            phoneNum = p;
            email = e;
            gender = g;
            bDate = b;
            status = sts;
            cgpa = CGPA;
            sgpa = SGPA;
            numCredits = n;
        }
    }
}
