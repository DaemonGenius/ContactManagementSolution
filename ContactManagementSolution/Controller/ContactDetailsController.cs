﻿using ContactManagementSolution.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactManagementSolution.Controller
{
    public class ContactDetailsController : ApiController
    {
        ContactManagementSolution.Services.ContactManagementSolutionDbe connect = new Services.ContactManagementSolutionDbe();
        
        public List<ContactDetails> list = new List<ContactDetails>();
        public static int id;
        
        [HttpGet()]
        public IHttpActionResult GetAll()
        {   
            using (connect.GetConnection())
            {
                
                SqlCommand oCmd = new SqlCommand("spGetAll", connect.GetConnection());
                oCmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        ContactDetails contact = new ContactDetails();
                        contact.ID = Convert.ToInt32(oReader["ID"]);
                        contact.FName = oReader["FName"].ToString();
                        contact.LName = oReader["LName"].ToString();
                        contact.PhoneNumber = oReader["CellNumber"].ToString();
                        
                        
                        id++;
                        list.Add(contact);
                    }
                    
                }

            }
           

            return Ok(list);
        }


        [HttpPost()]
        public IHttpActionResult Post(ContactDetails contact)
        {
            using (connect.GetConnection())
            {
                string oString = "INSERT INTO ContactDetails(ID,FName,LName,CellNumber) Values (@param1,@param2,@param3,@param4)";
                using (SqlCommand cmd = new SqlCommand(oString, connect.GetConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@param1", id + 1);
                    cmd.Parameters.AddWithValue("@param2", contact.FName);
                    cmd.Parameters.AddWithValue("@param3", contact.LName);
                    cmd.Parameters.AddWithValue("@param4", contact.PhoneNumber);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            return Ok();
        }
        
        [HttpPut()]
        public bool Update(ContactDetails contact)
        {
            using (connect.GetConnection())
            {
                string query = "UPDATE ContactDetails SET FName = @FName, LName = @LName, CellNumber = @CellNumber Where ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, connect.GetConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", contact.ID);
                    cmd.Parameters.AddWithValue("@FName", contact.FName);
                    cmd.Parameters.AddWithValue("@LName", contact.LName);
                    cmd.Parameters.AddWithValue("@CellNumber", contact.PhoneNumber);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            return true;
        }
        [HttpDelete()]
        public bool Delete(int id)
        {
            string oString = "Delete from ContactDetails where ID=@id";
            using (connect.GetConnection())
            {

                using (SqlCommand cmd = new SqlCommand(oString, connect.GetConnection()))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            return true;
        }

        [HttpGet()]
        public IHttpActionResult GetContact(string name)
        {
            ContactDetails contact = new ContactDetails();
            using (connect.GetConnection())
            {
                string oString = "Select * FROM ContactDetails Where FName='" + name + "'";
                SqlCommand oCmd = new SqlCommand(oString, connect.GetConnection());
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {

                    while (oReader.Read())
                    {
                        contact.ID = Convert.ToInt32(oReader["ID"]);
                        contact.FName = oReader["FName"].ToString();
                        contact.LName = oReader["LName"].ToString();
                        contact.PhoneNumber = oReader["CellNumber"].ToString();

                    }

                }

            }
            return Ok(contact);
        }

        [HttpGet()]
        public IHttpActionResult Get(int id)
        {
            ContactDetails contact = new ContactDetails();
            using (connect.GetConnection())
            {
                string oString = "Select * FROM ContactDetails Where ID='"+id+"'";
                SqlCommand oCmd = new SqlCommand(oString, connect.GetConnection());
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                  
                    while (oReader.Read())
                    {
                        contact.ID = Convert.ToInt32(oReader["ID"]);
                        contact.FName = oReader["FName"].ToString();
                        contact.LName = oReader["LName"].ToString();
                        contact.PhoneNumber = oReader["CellNumber"].ToString();

                    }

                }

            }
            return Ok(contact);
        }

        //public List<ContactDetails> SortAlp()
        //{
        //    return SortAlp();
        //}
    }
}
