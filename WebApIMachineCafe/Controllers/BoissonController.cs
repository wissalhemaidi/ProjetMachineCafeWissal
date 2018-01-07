using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApIMachineCafe.Models;

namespace WebApIMachineCafe.Controllers
{
    public class BoissonController : ApiController
    {
        SqlConnection conn = new SqlConnection();
        // GET api/<controller>
       
        public IEnumerable<Boisson> Get()
        {
           List<Boisson> Lboisson=new List<Boisson>() ;
            
            //conn.ConnectionString = "Server=[vulcain];Database=[MachineCafeBD]";
             conn.ConnectionString = "Data Source =(LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\gh\\source\\repos\\MachineCafe\\WebApIMachineCafe\\App_Data\\MachineCafeBD.mdf; Integrated Security = True";
            conn.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from T_boisson", conn);
            myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                Boisson boisson = new Boisson();
                boisson.id = int.Parse(myReader["id"].ToString());
                boisson.lib_Boisson = myReader["boisson"].ToString();
                Lboisson.Add(boisson);

            }

            conn.Close();
            
            return Lboisson;
        }
      
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}