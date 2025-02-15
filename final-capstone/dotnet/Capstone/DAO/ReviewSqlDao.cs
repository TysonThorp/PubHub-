﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class ReviewSqlDao : IReviewDao
    {
        private readonly string connectionString;

        public ReviewSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Review GetReview(int reviewId)
        {
            Review returnReview = null;

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT review_id, user_id, beer_id, rating, review_description " +
                    "FROM reviews " +
                    "WHERE review_id = @reviewId", conn);

                cmd.Parameters.AddWithValue("@reviewId", reviewId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    returnReview = GetReviewFromReader(reader);
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnReview;
        }

        public List<Review> GetAllReviews()
        {
            List<Review> reviewList = new List<Review>();

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT review_id, user_id, beer_id, rating, review_description " +
                    "FROM reviews ORDER BY rating DESC", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    reviewList.Add(GetReviewFromReader(reader));
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return reviewList;
        }

        public Review AddReview(Review review)
        {
            Review returnReview = null;
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO reviews (user_id, beer_id, rating, review_description) " +
                    "OUTPUT INSERTED.review_id " +
                    "VALUES (@user_id, @beer_id, @rating, @review_description)", conn);

                cmd.Parameters.AddWithValue("@user_id", review.UserId);
                cmd.Parameters.AddWithValue("@beer_id", review.BeerId);
                cmd.Parameters.AddWithValue("@rating", review.Rating);
                cmd.Parameters.AddWithValue("@review_description", review.ReviewText);

                int newReviewId = Convert.ToInt32(cmd.ExecuteScalar());
                returnReview = GetReview(newReviewId);
                
            }
            catch (SqlException)
            {
                throw;
            }
            return returnReview;
        }

        public void DeleteReview(int reviewId)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM reviews WHERE review_id = @review_id", conn);
            cmd.Parameters.AddWithValue("@review_id", reviewId);
            cmd.ExecuteNonQuery();
        }

        public Review UpdateReview(Review review)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE reviews " +
                    "SET user_id = @user_id, beer_id = @beer_id, rating = @rating, review_description = @review_description " +
                    "WHERE review_id = @review_id", conn);

                cmd.Parameters.AddWithValue("@review_id", review.ReviewId);
                cmd.Parameters.AddWithValue("@user_id", review.UserId);
                cmd.Parameters.AddWithValue("@beer_id", review.BeerId);
                cmd.Parameters.AddWithValue("@rating", review.Rating);
                cmd.Parameters.AddWithValue("@review_description", review.ReviewText);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            //return the actual updated item from the database here?
            return review;
        }

        private Review GetReviewFromReader(SqlDataReader reader)
        {
            return new Review()
            {
                ReviewId = Convert.ToInt32(reader["review_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                BeerId = Convert.ToInt32(reader["beer_id"]),
                Rating = Convert.ToDecimal(reader["rating"]),
                ReviewText = Convert.ToString(reader["review_description"])
            };
        }
    }
}
