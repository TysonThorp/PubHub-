﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class BrewerySqlDao : IBreweryDao
    {
        private readonly string connectionString;

        public BrewerySqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Brewery GetBrewery(int breweryId)
        {
            Brewery returnBrewery = null;

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT brewery_id, brewery_owner_id, email, phone, website, brewery_description, hours_operatiobs, address, isActive " +
                    "FROM users" +
                    "WHERE brewery_id = @breweryId", conn);

                cmd.Parameters.AddWithValue("@breweryId", breweryId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    returnBrewery = GetBreweryFromReader(reader);
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnBrewery;
        }

        public List<Brewery> GetAllBreweries()
        {
            List<Brewery> breweryList = new List<Brewery>();

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT brewery_id, brewery_owner_id, email, phone, website, brewery_description, hours_operatiobs, address, isActive " +
                    "FROM users" +
                    "WHERE brewery_id = @breweryId", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    breweryList.Add(GetBreweryFromReader(reader));
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return breweryList;
        }

        public Brewery AddBrewery(Brewery brewery)
        {
            Brewery returnBrewery = null;
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Breweries (brewery_owner_id, email, phone, website, brewery_description, hours_operations, address, isActive) " +
                    "VALUES (@brewery_owner_id, @email, @phone, @website, @brewery_description, @hours_operations, @address, @isActive OUTPUT INSERTED.brewery_id", conn);

                cmd.Parameters.AddWithValue("@brewery_owner_id", brewery.BreweryOwnerID);
                cmd.Parameters.AddWithValue("@email", brewery.EmailAddress);
                cmd.Parameters.AddWithValue("@phone", brewery.PhoneNumber);
                cmd.Parameters.AddWithValue("@website", brewery.Website);
                cmd.Parameters.AddWithValue("@brewery_description", brewery.Description);
                cmd.Parameters.AddWithValue("@hours_operations", brewery.HoursOfOperation);
                cmd.Parameters.AddWithValue("@address", brewery.Address);
                cmd.Parameters.AddWithValue("@isActive", brewery.IsActive);

                int newBreweryId = Convert.ToInt32(cmd.ExecuteScalar());
                returnBrewery = GetBrewery(newBreweryId);
                
            }
            catch (SqlException)
            {
                throw;
            }
            return returnBrewery;
        }

        public void DeleteBrewery(int breweryId)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Brewery WHERE brewery_id = @breweryId", conn);
            cmd.Parameters.AddWithValue("@brewery_id", breweryId);
            cmd.ExecuteNonQuery();
        }

        public Brewery UpdateBrewery(int breweryId, Brewery brewery)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Breweries " +
                    "SET brewery_owner_id = @brewery_owner_id, email = @email, phone = @phone, website = @website, brewery_description = @brewery_description, hours_operations = @hours_operations, address = @address, isActive = @isActive" +
                    "WHERE brewery_id = @brewery_id conn)");

                cmd.Parameters.AddWithValue("@brewery_id", breweryId);
                cmd.Parameters.AddWithValue("@brewery_owner_id", brewery.BreweryOwnerID);
                cmd.Parameters.AddWithValue("@email", brewery.EmailAddress);
                cmd.Parameters.AddWithValue("@phone", brewery.PhoneNumber);
                cmd.Parameters.AddWithValue("@website", brewery.Website);
                cmd.Parameters.AddWithValue("@brewery_description", brewery.Description);
                cmd.Parameters.AddWithValue("@hours_operations", brewery.HoursOfOperation);
                cmd.Parameters.AddWithValue("@address", brewery.Address);
                cmd.Parameters.AddWithValue("@isActive", brewery.IsActive);

                cmd.ExecuteNonQuery();

            }
            catch (SqlException)
            {
                throw;
            }
            //update this?
            return brewery;
        }

        private Brewery GetBreweryFromReader(SqlDataReader reader)
        {
            return new Brewery()
            {
                BreweryID = Convert.ToInt32(reader["brewery_id"]),
                BreweryOwnerID = Convert.ToInt32(reader["brewery_owner_id"]),
                BreweryName = Convert.ToString(reader["brewery_name"]),
                PhoneNumber = Convert.ToString(reader["phone"]),
                EmailAddress = Convert.ToString(reader["email"]),
                Address = Convert.ToString(reader["address"]),
                HoursOfOperation = Convert.ToString(reader["hours_operations"]),
                IsActive = Convert.ToBoolean(reader["isActive"]),
            };
        }
    }
}