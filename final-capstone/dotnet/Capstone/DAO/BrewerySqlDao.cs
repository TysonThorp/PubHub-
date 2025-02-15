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

                SqlCommand cmd = new SqlCommand("SELECT brewery_id, brewery_owner_id, brewery_name, email, phone, website, brewery_description, image, hours_operations, address, isActive, image_two, image_three " +
                    "FROM breweries " +
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

                SqlCommand cmd = new SqlCommand("SELECT brewery_id, brewery_name, brewery_owner_id, email, phone, website, brewery_description, image, hours_operations, address, isActive, image_two, image_three " +
                    "FROM breweries", conn);

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

                SqlCommand cmd = new SqlCommand("INSERT INTO breweries (brewery_owner_id, brewery_name, email, phone, website, brewery_description, image, hours_operations, address, isActive, image_two, image_three) " +
                    "OUTPUT INSERTED.brewery_id " +
                    "VALUES (@brewery_owner_id, @brewery_name, @email, @phone, @website, @brewery_description, @image, @hours_operations, @address, @isActive, @image_two, @image_three)", conn);

                cmd.Parameters.AddWithValue("@brewery_owner_id", brewery.BreweryOwnerId);
                cmd.Parameters.AddWithValue("@brewery_name", brewery.BreweryName);
                cmd.Parameters.AddWithValue("@email", brewery.EmailAddress);
                cmd.Parameters.AddWithValue("@phone", brewery.PhoneNumber);
                cmd.Parameters.AddWithValue("@website", brewery.Website);
                cmd.Parameters.AddWithValue("@brewery_description", brewery.Description);
                cmd.Parameters.AddWithValue("@image", brewery.Image);
                cmd.Parameters.AddWithValue("@hours_operations", brewery.HoursOfOperation);
                cmd.Parameters.AddWithValue("@address", brewery.Address);
                cmd.Parameters.AddWithValue("@isActive", brewery.IsActive);
                cmd.Parameters.AddWithValue("@image_two", brewery.ImageTwo);
                cmd.Parameters.AddWithValue("@image_three", brewery.ImageThree);

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

            SqlCommand cmd = new SqlCommand("DELETE FROM beers_by_brewery WHERE brewery_id = @breweryId " +
                                            "DELETE FROM breweries WHERE brewery_id = @breweryId", conn);
            cmd.Parameters.AddWithValue("@breweryId", breweryId);
            cmd.ExecuteNonQuery();
        }

        public Brewery UpdateBrewery(Brewery brewery)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE breweries " +
                    "SET brewery_owner_id = @brewery_owner_id, brewery_name = @brewery_name, email = @email, phone = @phone, website = @website, brewery_description = @brewery_description, image = @image, hours_operations = @hours_operations, address = @address, isActive = @isActive, image_two = @image_two, image_three = @image_three " +
                    "WHERE brewery_id = @brewery_id", conn);

                cmd.Parameters.AddWithValue("@brewery_id", brewery.BreweryId);
                cmd.Parameters.AddWithValue("@brewery_name", brewery.BreweryName);
                cmd.Parameters.AddWithValue("@brewery_owner_id", brewery.BreweryOwnerId);
                cmd.Parameters.AddWithValue("@email", brewery.EmailAddress);
                cmd.Parameters.AddWithValue("@phone", brewery.PhoneNumber);
                cmd.Parameters.AddWithValue("@website", brewery.Website);
                cmd.Parameters.AddWithValue("@brewery_description", brewery.Description);
                cmd.Parameters.AddWithValue("@image", brewery.Image);
                cmd.Parameters.AddWithValue("@hours_operations", brewery.HoursOfOperation);
                cmd.Parameters.AddWithValue("@address", brewery.Address);
                cmd.Parameters.AddWithValue("@isActive", brewery.IsActive);
                cmd.Parameters.AddWithValue("@image_two", brewery.ImageTwo);
                cmd.Parameters.AddWithValue("@image_three", brewery.ImageThree);

                cmd.ExecuteNonQuery();

            }
            catch (SqlException)
            {
                throw;
            }
            //return the actual updated item from the database here?
            return brewery;
        }

        private Brewery GetBreweryFromReader(SqlDataReader reader)
        {
            return new Brewery()
            {
                BreweryId = Convert.ToInt32(reader["brewery_id"]),
                BreweryOwnerId = Convert.ToInt32(reader["brewery_owner_id"]),
                BreweryName = Convert.ToString(reader["brewery_name"]),
                EmailAddress = Convert.ToString(reader["email"]),
                PhoneNumber = Convert.ToString(reader["phone"]),
                Website = Convert.ToString(reader["website"]),
                Description = Convert.ToString(reader["brewery_description"]),
                Image = Convert.ToString(reader["image"]),
                ImageTwo = Convert.ToString(reader["image_two"]),
                ImageThree = Convert.ToString(reader["image_three"]),
                Address = Convert.ToString(reader["address"]),
                HoursOfOperation = Convert.ToString(reader["hours_operations"]),
                IsActive = Convert.ToBoolean(reader["isActive"]),
            };
        }
    }
}
