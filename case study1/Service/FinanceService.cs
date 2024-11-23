using System;
using System.Collections.Generic;
using case_study1.Model;
using case_study1.Repository;
using case_study1.utility;

namespace case_study1.Service
{
    public class FinanceService
    {
        private static string connectionString = DbConnUtil.GetConnectionString();
        private IFinanceRepository financeRepository;

        public FinanceService()
        {
            financeRepository = new FinanceRepository(connectionString);
        }

        // Function to display the menu and interact with the user
        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Finance Management System");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Create Expense");
                Console.WriteLine("3. Get All Expenses");
                Console.WriteLine("4. Update Expense");
                Console.WriteLine("5. Delete Expense");
                Console.WriteLine("6. Delete User");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateUser();
                        break;
                    case 2:
                        CreateExpense();
                        break;
                    case 3:
                        GetAllExpenses();
                        break;
                    case 4:
                        UpdateExpense();
                        break;
                    case 5:
                        DeleteExpense();
                        break;
                    case 6:
                        DeleteUser();
                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Function to create a new user
        private void CreateUser()
        {
            Console.WriteLine("\nCreate User");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            User newUser = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            bool result = financeRepository.CreateUser(newUser);
            if (result)
            {
                Console.WriteLine("User created successfully.");
            }
            else
            {
                Console.WriteLine("Error creating user.");
            }
        }

        // Function to create a new expense
        private void CreateExpense()
        {
            Console.WriteLine("\nCreate Expense");

            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Category ID: ");
            int categoryId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Expense newExpense = new Expense
            {
                UserId = userId,
                Amount = amount,
                CategoryId = categoryId,
                Date = DateTime.Now,
                Description = description
            };

            bool result = financeRepository.CreateExpense(newExpense);
            if (result)
            {
                Console.WriteLine("Expense created successfully.");
            }
            else
            {
                Console.WriteLine("Error creating expense.");
            }
        }

        // Function to get all expenses for a specific user
        private void GetAllExpenses()
        {
            Console.WriteLine("\nGet All Expenses");

            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            List<Expense> expenses = financeRepository.GetAllExpenses(userId);
            if (expenses.Count > 0)
            {
                foreach (var expense in expenses)
                {
                    Console.WriteLine($"Expense ID: {expense.ExpenseId}, Amount: {expense.Amount}, Date: {expense.Date}, Description: {expense.Description}");
                }
            }
            else
            {
                Console.WriteLine("No expenses found for this user.");
            }
        }

        // Function to update an expense
        private void UpdateExpense()
        {
            Console.WriteLine("\nUpdate Expense");

            Console.Write("Enter Expense ID: ");
            int expenseId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Category ID: ");
            int categoryId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Expense updatedExpense = new Expense
            {
                ExpenseId = expenseId,
                UserId = userId,
                Amount = amount,
                CategoryId = categoryId,
                Date = DateTime.Now,
                Description = description
            };

            bool result = financeRepository.UpdateExpense(userId, updatedExpense);
            if (result)
            {
                Console.WriteLine("Expense updated successfully.");
            }
            else
            {
                Console.WriteLine("Error updating expense.");
            }
        }

        // Function to delete an expense
        private void DeleteExpense()
        {
            Console.WriteLine("\nDelete Expense");

            Console.Write("Enter Expense ID: ");
            int expenseId = Convert.ToInt32(Console.ReadLine());

            bool result = financeRepository.DeleteExpense(expenseId);
            if (result)
            {
                Console.WriteLine("Expense deleted successfully.");
            }
            else
            {
                Console.WriteLine("Error deleting expense.");
            }
        }

        // Function to delete a user
        private void DeleteUser()
        {
            Console.WriteLine("\nDelete User");

            Console.Write("Enter User ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            bool result = financeRepository.DeleteUser(userId);
            if (result)
            {
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("Error deleting user.");
            }
        }
    }
}
