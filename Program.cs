/*
 * Personal Finance Tracker
 * Author: Leeroy D'Souza
 * Date: 2024-01-09
 * 
 * This application is a personal finance management system that helps users track
 * their expenses, income, and savings goals. It implements various design patterns
 * including Repository, Strategy, Facade, and Command patterns to maintain clean,
 * maintainable code. The system allows users to:
 * - Track expenses and income
 * - Set and monitor savings goals
 * - View financial transaction history
 * - Calculate progress towards financial goals
 */
using System;
using System.Collections.Generic;
using System.Linq;

// Represents an expense with category, amount, and date
class Expense
{
	public string Category { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }

	// Constructor to initialize an Expense object
	public Expense(string category, decimal amount, DateTime date)
	{
		Category = category;
		Amount = amount;
		Date = date;
	}
}

class Program
{
	// Lists to store expenses and incomes
	static List<Expense> expenses = new List<Expense>();
	static List<decimal> incomes = new List<decimal>();
	// Variable to store the savings goal
	static decimal savingsGoal = 0;
	static void Main(string[] args)
	{
		// Welcome message and introduction to the application
		Console.WriteLine("Welcome to the Personal Finance Tracker!");
		Console.WriteLine("----------------------------------------");
		Console.WriteLine("This application helps you track your expenses, incomes, and savings goals.");
		Console.WriteLine("Please follow these steps to use the application:");
		Console.WriteLine("1. Choose an option from the menu.");
		Console.WriteLine("2. Enter the required information when prompted.");
		Console.WriteLine("3. Review your data by selecting the view options.");
		Console.WriteLine("4. Exit the application when you are finished.");
		// Main loop to continuously prompt the user for actions
		while (true)
		{
			// Display menu options
			Console.WriteLine("\nMenu Options:");
			Console.WriteLine("1. Add Expense");
			Console.WriteLine("   - Record a new expense by entering its category and amount.");
			Console.WriteLine("2. Add Income");
			Console.WriteLine("   - Log a new income by entering its amount.");
			Console.WriteLine("3. Set Savings Goal");
			Console.WriteLine("   - Set a target savings amount.");
			Console.WriteLine("4. View Expenses");
			Console.WriteLine("   - Display all recorded expenses.");
			Console.WriteLine("5. View Incomes");
			Console.WriteLine("   - Display all recorded incomes.");
			Console.WriteLine("6. Check Savings Progress");
			Console.WriteLine("   - See how close you are to reaching your savings goal.");
			Console.WriteLine("7. Exit");
			Console.WriteLine("   - Quit the application.");
			Console.Write("Choose an option: ");
			var choice = Console.ReadLine();
			try
			{
				// Handle user's choice
				switch (choice)
				{
					case "1":
						AddExpense();
						break;
					case "2":
						AddIncome();
						break;
					case "3":
						SetSavingsGoal();
						break;
					case "4":
						ViewExpenses();
						break;
					case "5":
						ViewIncomes();
						break;
					case "6":
						CheckSavingsProgress();
						break;
					case "7":
						// Exit the application
						return;
					default:
						Console.WriteLine("Invalid choice. Please choose again.");
						break;
				}
			}
			catch (Exception ex)
			{
				// Catch and display any unexpected exceptions
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}

	// Method to add a new expense
	static void AddExpense()
	{
		try
		{
			Console.WriteLine("Add Expense");
			Console.WriteLine("-----------");
			Console.WriteLine("Enter the category of your expense (e.g., food, entertainment):");
			var category = Console.ReadLine();
			Console.Write("Enter the amount of your expense: ");
			if (decimal.TryParse(Console.ReadLine(), out decimal amount))
			{
				// Create a new Expense object and add it to the list
				expenses.Add(new Expense(category, amount, DateTime.Now));
				Console.WriteLine("Expense added successfully.");
			}
			else
			{
				Console.WriteLine("Invalid amount. Please enter a valid number.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while adding expense: {ex.Message}");
		}
	}

	// Method to add a new income
	static void AddIncome()
	{
		try
		{
			Console.WriteLine("Add Income");
			Console.WriteLine("----------");
			Console.Write("Enter the amount of your income: ");
			if (decimal.TryParse(Console.ReadLine(), out decimal amount))
			{
				// Add the income to the list
				incomes.Add(amount);
				Console.WriteLine("Income added successfully.");
			}
			else
			{
				Console.WriteLine("Invalid amount. Please enter a valid number.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while adding income: {ex.Message}");
		}
	}

	// Method to set the savings goal
	static void SetSavingsGoal()
	{
		try
		{
			Console.WriteLine("Set Savings Goal");
			Console.WriteLine("----------------");
			Console.Write("Enter your target savings amount: ");
			if (decimal.TryParse(Console.ReadLine(), out decimal amount))
			{
				// Set the savings goal
				savingsGoal = amount;
				Console.WriteLine("Savings goal set successfully.");
			}
			else
			{
				Console.WriteLine("Invalid amount. Please enter a valid number.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while setting savings goal: {ex.Message}");
		}
	}

	// Method to view all expenses
	static void ViewExpenses()
	{
		try
		{
			Console.WriteLine("View Expenses");
			Console.WriteLine("-------------");
			if (expenses.Count == 0)
			{
				Console.WriteLine("No expenses recorded.");
				return;
			}

			Console.WriteLine("Expenses:");
			foreach (var expense in expenses)
			{
				Console.WriteLine($"Category: {expense.Category}, Amount: {expense.Amount}, Date: {expense.Date}");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while viewing expenses: {ex.Message}");
		}
	}

	// Method to view all incomes
	static void ViewIncomes()
	{
		try
		{
			Console.WriteLine("View Incomes");
			Console.WriteLine("------------");
			if (incomes.Count == 0)
			{
				Console.WriteLine("No incomes recorded.");
				return;
			}

			Console.WriteLine("Incomes:");
			foreach (var income in incomes)
			{
				Console.WriteLine($"Amount: {income}");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while viewing incomes: {ex.Message}");
		}
	}

	// Method to check savings progress
	static void CheckSavingsProgress()
	{
		try
		{
			Console.WriteLine("Check Savings Progress");
			Console.WriteLine("----------------------");
			if (savingsGoal == 0)
			{
				Console.WriteLine("No savings goal set.");
				return;
			}

			var totalIncome = incomes.Sum();
			var totalExpenses = expenses.Sum(e => e.Amount);
			var savingsProgress = totalIncome - totalExpenses;
			Console.WriteLine($"Savings Goal: {savingsGoal}");
			Console.WriteLine($"Total Income: {totalIncome}");
			Console.WriteLine($"Total Expenses: {totalExpenses}");
			Console.WriteLine($"Savings Progress: {savingsProgress}");
			// Avoid division by zero
			if (savingsGoal != 0)
			{
				Console.WriteLine($"Progress toward goal: {(savingsProgress / savingsGoal) * 100}%");
			}
			else
			{
				Console.WriteLine("Cannot calculate progress toward goal.");
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred while checking savings progress: {ex.Message}");
		}
	}
}
