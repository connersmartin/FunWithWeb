using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace FunWithWeb.Models.DataAccess
{
    public class DataAccess
    {
        public const string cs = @"server=localhost;userid=root;
            password=mUR@D3ra73;database=datatest";

        //View info

        public List<DataTest> ViewInfo(int id, string viewAction)
        {
            MySqlConnection conn = null;
            MySqlDataReader rdr = null;
            List<DataTest> viewData = new List<DataTest>();
            string stm = "";

            try
            {
                //open the connection
                conn = new MySqlConnection(cs);
                conn.Open();

                //sql query

                if (viewAction == "all")
                {
                    stm = "SELECT * FROM datatest";
                }
                else
                {
                    stm = "SELECT * FROM datatest WHERE ID = " + id;

                }
                MySqlCommand cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();

                //while there is data
                while (rdr.Read())
                {
                    viewData.Add(new DataTest(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetInt32(5)));
                }

            }
            catch (MySqlException ex)
            {
                if (viewData.Count == 0)
                {
                    DataTest testData = new DataTest(999999999, "Chick Corea", "Beneath The Mask", "One of us is over 40", "Dave Weckl", 1991);
                    viewData.Add(testData);
                }
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
            
            return viewData;
        }

        //Add info 
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
                
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //Edit info

        public void EditInfo(DataTest editInfo, string editAction)
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

                if (editAction == "delete")
                {
                    cmd.CommandText = "DELETE from datatest  WHERE Id=" + editInfo.ID;
                }
                else
                {
                    cmd.CommandText = "UPDATE datatest SET Artist = @Artist, Album = @Album, TrackName = @TrackName, Drummer = @Drummer, Year = @Year  WHERE Id=" + editInfo.ID;
                    cmd.Parameters.AddWithValue("@Artist", editInfo.Artist);
                    cmd.Parameters.AddWithValue("@Album", editInfo.Album);
                    cmd.Parameters.AddWithValue("@TrackName", editInfo.TrackName);
                    cmd.Parameters.AddWithValue("@Drummer", editInfo.Drummer);
                    cmd.Parameters.AddWithValue("@Year", editInfo.Year);
                }

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