using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactManagementSolution.Model
{

    public class ContactDetails
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }
        public ContactDetails(int id, string fname, string lname, string pNum)
        {
            this.ID = id;
            this.FName = fname;
            this.LName = lname;
            this.PhoneNumber = pNum;
        }
        public ContactDetails()
        {

        }


       

       

       




    }
}