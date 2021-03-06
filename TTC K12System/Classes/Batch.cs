﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using MySql.Connection;

namespace TTC_K12System.Classes
{
    class Batch
    {
        //PROPERTIES
        internal int ID { get; set; }
        internal short Number { get; set; }
        internal string Year { get; set; }

        internal static List<Batch> GetAll()
        {
            List<Batch> batches = new List<Batch>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(Builder.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM batches";
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Batch batch = new Batch();
                            batch.ID = rdr.GetInt32(0);
                            batch.Number = rdr.GetInt16(1);
                            batch.Year = rdr.GetString(2);
                            batches.Add(batch);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTrapper.Log(ex, LogOptions.PromptTheUser);
            }
            return batches;
        }

        internal static Batch GetByID(int ID)
        {
            Batch batch = new Batch();
            try
            {
                using (MySqlConnection con = new MySqlConnection(Builder.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM batches WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", ID);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            batch.ID = rdr.GetInt32(0);
                            batch.Number = rdr.GetInt16(1);
                            batch.Year = rdr.GetString(2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTrapper.Log(ex, LogOptions.PromptTheUser);
            }
            return batch;
        }

        internal void Save()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(Builder.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO batches (number, year) VALUES (@number, @year)";
                    cmd.Parameters.AddWithValue("number", Number);
                    cmd.Parameters.AddWithValue("year", Year);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (ID == 0) ID = Convert.ToInt32(cmd.LastInsertedId);
                }
            }
            catch (Exception ex)
            {
                ErrorTrapper.Log(ex, LogOptions.PromptTheUser);
            }
        }

        internal static Batch GetByNumber(short Number)
        {
            Batch batch = new Batch();
            try
            {
                using (MySqlConnection con = new MySqlConnection(Builder.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM batches WHERE number = @number";
                    cmd.Parameters.AddWithValue("number", Number);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            batch.ID = rdr.GetInt32(0);
                            batch.Number = rdr.GetInt16(1);
                            batch.Year = rdr.GetString(2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTrapper.Log(ex, LogOptions.PromptTheUser);
            }
            return batch;
        }

        private static short GetNextNumber(int ProgramID)
        {
            int number = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(Builder.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT MAX(number) FROM batches WHERE program_id = @program_id";
                    cmd.Parameters.AddWithValue("program_id", ProgramID);
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            number = rdr.GetInt16(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTrapper.Log(ex, LogOptions.LogToFile);
            }
            return Convert.ToInt16(number + 1);
        }

    }
}
