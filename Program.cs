using System;
using System.Diagnostics;

namespace ExpenseTracker
{
    public class Program
    {

        public static void Main(String[] args)
        {
            //Create objects following singleton design pattern
            Category category = null;
            Transaction transaction;

            Console.WriteLine("Enter the transaction name:");
            String name = Console.ReadLine();

            Console.WriteLine("Enter the transaction amount:");
            Double amount = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the transaction type (0 - Income, 1 - Expense):");
            String type = Console.ReadLine();
            bool isExpense = true;
            if (type.Equals("1"))
            {
                isExpense = true;
            }
            else if (type.Equals("0"))
            {
                isExpense = false;
            }

            String today = DateTime.Now.ToString("M/d/yyyy");

            Console.WriteLine("Enter the transaction category:");
            Console.WriteLine("1 - Income, 2 - Education, 3 - Transport");
            String transaction_category = Console.ReadLine();
            if (transaction_category.Equals("1"))
            {
                category = Income.getInstance();
            }
            else if (transaction_category.Equals("2"))
            {
                category = Education.getInstance();
            }
            else if (transaction_category.Equals("3"))
            {
                category = Transport.getInstance();
            }

            //Append the transaction amount to category
            category.setTotal(amount);

            //Create the transaction object
            transaction = new Transaction(name, amount, isExpense, today, category.getCategoryName());
            transaction.writeToFile();
            transaction.readTransactionData();

            //Get the category wise spent amounts
            category = Education.getInstance();
            Console.WriteLine($"Spent amount for Education: {category.getTotal()}");
            category = Transport.getInstance();
            Console.WriteLine($"Spent amount for Transport: {category.getTotal()}");

            //Get the total income
            category = Income.getInstance();
            Console.WriteLine($"Total Income: {category.getTotal()}");

            transaction.editTransaction();

        }

        
    }
}