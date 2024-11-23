using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using case_study1.Exceptions;
using case_study1.Model;

namespace case_study1.Repository
{
    internal class FinanceRepository : IFinanceRepository
    {
        private string connectionString;

        public FinanceRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Create a new User
        public bool CreateUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (username, email, password) VALUES (@Username, @Email, @PasswordHash)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.Password);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;  // Return true if the insert was successful
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating user: " + ex.Message);
                return false;
            }
        }

        // Create a new Expense
        public bool CreateExpense(Expense expense)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Expenses (user_id, amount, category_id, date, description) VALUES (@UserId, @Amount, @CategoryId, @Date, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", expense.UserId);
                        cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                        cmd.Parameters.AddWithValue("@CategoryId", expense.CategoryId);
                        cmd.Parameters.AddWithValue("@Date", expense.Date);
                        cmd.Parameters.AddWithValue("@Description", expense.Description);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;  // Return true if the insert was successful
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating expense: " + ex.Message);
                return false;
            }
        }

        // Delete a User by UserId
        public bool DeleteUser(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Users WHERE user_id = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        var result = cmd.ExecuteNonQuery();
                        if (result == 0)
                        {
                            // Throw exception if user is not found
                            throw new UserNotFoundException($"User with ID {userId} not found.");
                        }

                        return result > 0;  // Return true if the delete was successful
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user: " + ex.Message);
                return false;
            }
        }

        // Delete an Expense by ExpenseId
        public bool DeleteExpense(int expenseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Expenses WHERE expense_id = @ExpenseId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ExpenseId", expenseId);

                        var result = cmd.ExecuteNonQuery();
                        if (result == 0)
                        {
                            // Throw exception if expense is not found
                            throw new ExpenseNotFoundException($"Expense with ID {expenseId} not found.");
                        }

                        return result > 0;  // Return true if the delete was successful
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting expense: " + ex.Message);
                return false;
            }
        }

        // Get all Expenses for a specific UserId
        public List<Expense> GetAllExpenses(int userId)
        {
            List<Expense> expenses = new List<Expense>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT expense_id, user_id, amount, category_id, date, description FROM Expenses WHERE user_id = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                // Throw exception if no expenses are found for the user
                                throw new ExpenseNotFoundException($"No expenses found for user with ID {userId}.");
                            }

                            while (reader.Read())
                            {
                                Expense expense = new Expense
                                {
                                    ExpenseId = reader.GetInt32(0),
                                    UserId = reader.GetInt32(1),
                                    Amount = reader.GetDecimal(2),
                                    CategoryId = reader.GetInt32(3),
                                    Date = reader.GetDateTime(4),
                                    Description = reader.GetString(5)
                                };
                                expenses.Add(expense);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting expenses: " + ex.Message);
            }
            return expenses;
        }

        // Update an Expense for a specific UserId
        public bool UpdateExpense(int userId, Expense expense)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Expenses SET amount = @Amount, category_id = @CategoryId, date = @Date, description = @Description WHERE user_id = @UserId AND expense_id = @ExpenseId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                        cmd.Parameters.AddWithValue("@CategoryId", expense.CategoryId);
                        cmd.Parameters.AddWithValue("@Date", expense.Date);
                        cmd.Parameters.AddWithValue("@Description", expense.Description);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ExpenseId", expense.ExpenseId);

                        var result = cmd.ExecuteNonQuery();
                        if (result == 0)
                        {
                            // Throw exception if the expense was not found
                            throw new ExpenseNotFoundException($"Expense with ID {expense.ExpenseId} for user {userId} not found.");
                        }

                        return result > 0;  // Return true if the update was successful
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating expense: " + ex.Message);
                return false;
            }
        }
    }
}
