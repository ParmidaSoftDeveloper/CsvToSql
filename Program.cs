using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CsvToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            string filePath = @"C:\Users\mammadkoma\Desktop\Hazard\aaa.csv";
            using (StreamReader sr = new StreamReader(File.OpenRead(filePath)))
            {
                string file = sr.ReadToEnd();
                lines = new List<string>(file.Split('\n'));
            }

            //string cs = @"Server= ;Database= ;User Id= ;Password= ;";
            string cs = @"Server=.\SQLEXPRESS;Database=test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            int i = 1;
            foreach (string item in lines)
            {
                string[] textpart = item.Split(',');
                var OID = textpart[0];
                var HAZARDTYPE = textpart[1].Replace("\r", "");
                UpdateRecords(OID, HAZARDTYPE, con);
                System.Console.WriteLine("Updated : " + (i++) + "    at : " + DateTime.Now);
            } 
            con.Close();
            System.Console.WriteLine("****** Finished ******");
            Console.Read();
        }

        private static void UpdateRecords(string OID, string HAZARDTYPE, SqlConnection con)
        {
            string com = $"UPDATE Test1 SET HAZARDTYPE='{HAZARDTYPE}' WHERE OID='{OID}'";
            SqlCommand comm = new SqlCommand(com);
            comm.Connection = con;
            comm.ExecuteNonQuery();
        }
    }
}