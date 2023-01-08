using System;
using System.Diagnostics;

namespace ExpenseTracker
{
    public class Program
    {
        
        static Category category = null;

        public static void Main(String[] args)
        {
            readTransactionData();
            createTransaction();
           
            //Get the category wise spent amounts
            category = Education.getInstance();
            Console.WriteLine($"Spent amount for Education: {category.getTotal()}");
            category = Transport.getInstance();
            Console.WriteLine($"Spent amount for Transport: {category.getTotal()}");

            //Get the total income
            category = Income.getInstance();
            Console.WriteLine($"Total Income: {category.getTotal()}");

            //transaction.editTransaction();

            //transaction.deleteTransactions();
            
            //*********************************Add by Lohitha********************************
            // Create Budget factory object
            BudgetFactory bf = new BudgetFactory();

            // Get data from the text file
            bf.readTransactionData();
            
            // Get total budget
            Console.WriteLine(bf.getTotalBudget());
            
            //Get category budget
            //Assuming that categories are in an array or arrayList
            List<string> catogeryList = new List<string>();

            //Add objects to categoryList
            catogeryList.Add("Transport");
            catogeryList.Add("Food");
            catogeryList.Add("Education");

            //Loop to call category budget
            for(int i = 0; i < catogeryList.Count; i++)
            {
                string categoryName = catogeryList[i];
                Console.WriteLine(categoryName);
                Budget budg = bf.getCategoryBudget(categoryName);
                Console.WriteLine(budg.getcategoryName()+", Budget Amount - "+ budg.getBudget());
            }
            
            //*********************************************************************************

        }

        public static void readTransactionData()
        {
            //Read the transaction file content
            String transactions_path = @"Transactions.txt";
            String[] transactionRecords = File.ReadAllLines(transactions_path);

            //Read each transaction line
            Double amount = 0.0;
            for (int i = 0; i < transactionRecords.Length; i++)
            {
                String[] transaction = transactionRecords[i].Split('|');
                amount = System.Convert.ToDouble(transaction[1]);

                //Set each transaction amount based on it's category
                if (transaction[4].Equals("Education"))
                {
                    Category category = Education.getInstance();
                    category.setTotal(amount);
                }
                else if (transaction[4].Equals("Transport"))
                {
                    Category category = Transport.getInstance();
                    category.setTotal(amount);
                }
                else if (transaction[4].Equals("Income"))
                {
                    Category category = Income.getInstance();
                    category.setTotal(amount);
                }
            }
        }

        public static void createTransaction()
        {
            

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
            Console.WriteLine("Before add the amount " + category.getTotal());
            category.setTotal(amount);
            Console.WriteLine("After added the amount " + category.getTotal());

            //Create the transaction object
            Transaction transaction = new Transaction(name, amount, isExpense, today, category.getCategoryName());
            transaction.writeToFile();
        }

        
    }
}
