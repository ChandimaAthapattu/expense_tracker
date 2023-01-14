using System;
using System.Transactions;

namespace ExpenseTracker
{
	public class Transaction
	{
		//private static Transaction _instance;
		private String name;
		private Double amount;
		private bool isExpense;
		private String date;
		private String categoryName;
        String transactions_path;
        String[] transactionRecords;
        Category category = null;


        public Transaction(String name, Double amount, bool isExpense, String date, String category)
		{
			this.name = name;
			this.amount = amount;
			this.isExpense = isExpense;
			this.date = date;
			this.categoryName = category;
		}

        public void writeToFile()
        {
            //Construct the transaction to a string
            String transaction_record = $"{name}|{amount}|{isExpense}|{date}|{categoryName}";
            Console.WriteLine();
            Console.WriteLine($"Transaction is {transaction_record}");

            //Creating the file
            String file = @"Transactions.txt";

            //Append the text
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(transaction_record);

            }
        }
    }
}

