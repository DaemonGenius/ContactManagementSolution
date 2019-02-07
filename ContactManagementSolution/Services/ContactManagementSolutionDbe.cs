using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactManagementSolution.Services
{
    public class ContactManagementSolutionDbe
    {
        public ContactManagementSolutionDbe()
        {
            GetConnection();

        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=ContactManagementSolutionDbe;Integrated Security=True");
            connection.Open();
            return connection;
        }
    }
}