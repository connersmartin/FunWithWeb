using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FunWithWeb.Models;
using MySql.Data.MySqlClient;

namespace FunWithWeb.Controllers
{
    public class DataTestController : Controller
    {
        public const string cs = @"server=localhost;userid=root;
            password=mUR@D3ra73;database=datatest";
        // GET: DataTest
        public ActionResult Index()
        {

            List<DataTest> TestData = new List<DataTest>();

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {
                //open the connection
                conn = new MySqlConnection(cs);
                conn.Open();

                //sql query
                string stm = "SELECT * FROM datatest";
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                //while there is data
                while (rdr.Read())
                {
                    TestData.Add(new DataTest(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetInt32(5)));
                }

            }
            catch (MySqlException ex)
            {
                //TODO figure out how to display user friendly error messages
                throw;
            }
            finally //don't forget to close connections
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }

            //This is just test data.
          
            return View(TestData);
        }

        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(DataTest newDataTest)
        {
            if (ModelState.IsValid)
            {
                AddInfo(newDataTest);

                return RedirectToAction("Index");
            }
            else
            {
                return View(newDataTest);
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(DataTest newDataTest)
        {
            if (ModelState.IsValid)
            {
                EditInfo(newDataTest);

                return RedirectToAction("Index");
            }
            else
            {
                return View(newDataTest);
            }
        }


        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return null;
        }

        //Add the info
        public void AddInfo(DataTest newInfo)
        {

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO datatest (Artist, Album, TrackName, Drummer, Year) VALUES (@Artist, @Album, @TrackName, @Drummer, @Year)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Artist", newInfo.Artist);
                cmd.Parameters.AddWithValue("@Album", newInfo.Album);
                cmd.Parameters.AddWithValue("@TrackName", newInfo.TrackName);
                cmd.Parameters.AddWithValue("@Drummer", newInfo.Drummer);
                cmd.Parameters.AddWithValue("@Year", newInfo.Year);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                //TODO display sql error messages
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        //Edit the info maybe?

        public void EditInfo(DataTest editInfo)
        {
            MySqlConnection conn = null;
            MySqlTransaction tr = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                tr = conn.BeginTransaction();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;

                cmd.CommandText = "UPDATE datatest SET Artist = @Artist, Album = @Album, TrackName = @TrackName, Drummer = @Drummer, Year = @Year  WHERE Id=" + editInfo.ID;
                cmd.Parameters.AddWithValue("@Artist", editInfo.Artist);
                cmd.Parameters.AddWithValue("@Album", editInfo.Album);
                cmd.Parameters.AddWithValue("@TrackName", editInfo.TrackName);
                cmd.Parameters.AddWithValue("@Drummer", editInfo.Drummer);
                cmd.Parameters.AddWithValue("@Year", editInfo.Year);
                cmd.ExecuteNonQuery();

                tr.Commit();

            }
            catch (MySqlException ex)
            {
                try
                {
                    tr.Rollback();

                }
                catch (MySqlException ex1)
                {
                    Console.WriteLine("Error: {0}", ex1.ToString());
                }

                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        
        }
    }
}