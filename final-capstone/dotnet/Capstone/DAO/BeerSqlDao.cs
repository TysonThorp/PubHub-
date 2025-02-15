﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
namespace Capstone.DAO
{
    public class BeerSqlDao : IBeerDao
    {
        private readonly string connectionString;

        public BeerSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Beer AddBeer(Beer beer)
        {
            Beer returnBeer = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO beers (beer_name, description, image, abv, beer_type) OUTPUT INSERTED.beer_id VALUES(@beer_name, @description, @image, @abv, @beer_type)", conn);

                    cmd.Parameters.AddWithValue("@beer_name", beer.BeerName);
                    cmd.Parameters.AddWithValue("@description", beer.Description);
                    cmd.Parameters.AddWithValue("@image", beer.Image);
                    cmd.Parameters.AddWithValue("@abv", beer.ABV);
                    cmd.Parameters.AddWithValue("@beer_type", beer.BeerType);

                    int returnBeerId = Convert.ToInt32(cmd.ExecuteScalar());
                    returnBeer = GetBeerById(returnBeerId);

                    if(returnBeerId != 0 && beer.BreweryId != 0)
                    {
                        SqlCommand cmd2 = new SqlCommand("INSERT INTO beers_by_brewery (beer_id, brewery_id) VALUES (@beer_id, @brewery_id)", conn);

                        cmd2.Parameters.AddWithValue("@beer_id", returnBeerId);
                        cmd2.Parameters.AddWithValue("@brewery_id", beer.BreweryId);
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnBeer;
        }

        public void DeleteBeer(int beerId)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM reviews WHERE beer_id = @beer_id " +
                                                    "DELETE FROM beers_by_brewery WHERE beer_id = @beer_id " +
                                                    "DELETE FROM beers WHERE beer_id = @beer_id", conn);
                    cmd.Parameters.AddWithValue("@beer_id", beerId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        public List<Beer> GetAllBeers()
        {
            List<Beer> beerList = new List<Beer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT beer_id, beer_name, description, image, abv, beer_type FROM beers", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        beerList.Add(GetBeerFromReader(reader));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return beerList;
        }

        public List<Beer> GetBeersByBrewery(int breweryId)
        {
            List<Beer> beerList = new List<Beer>();

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT beers.beer_id, beer_name, description, image, abv, beer_type, brewery_id " +
                                                "FROM beers " +
                                                "JOIN beers_by_brewery ON beers.beer_id = beers_by_brewery.beer_id " +
                                                "WHERE brewery_id = @brewery_id", conn);
                cmd.Parameters.AddWithValue("@brewery_id", breweryId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    beerList.Add(GetBeerFromReader(reader));
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return beerList;
        }

        public Beer GetBeerById(int beerId)
        {
            Beer returnBeer = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT beer_id, beer_name, description, image, abv, beer_type FROM beers WHERE beer_id = @beer_id", conn);
                    cmd.Parameters.AddWithValue("@beer_id", beerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnBeer = GetBeerFromReader(reader);      
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnBeer;
        }

        private Beer GetBeerFromReader(SqlDataReader reader)
        {
            Beer beer = new Beer()
            {
                BeerId = Convert.ToInt32(reader["beer_id"]),
                BeerName = Convert.ToString(reader["beer_name"]),
                Description = Convert.ToString(reader["description"]),
                Image = Convert.ToString(reader["image"]),
                ABV = Convert.ToDecimal(reader["abv"]),
                BeerType = Convert.ToString(reader["beer_type"]),
            };

            return beer;
        }
    }
}
