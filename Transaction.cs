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

        public void replaceTransactionLine(String newText, String fileName, int lineToEdit)
        {

            String[] arrLine = File.ReadAllLines(fileName);
            Console.WriteLine("Line before edit : " + arrLine[lineToEdit]);
            arrLine[lineToEdit] = newText;
            Console.WriteLine("Line after edit : " + arrLine[lineToEdit]);
            File.WriteAllLines(fileName, arrLine);
        }

        public void deleteTransactionLine(String fileName, int lineToDelete)
        {
            String[] arrLine = File.ReadAllLines(fileName);
            List<String> arrayList = new List<string>(arrLine);
            arrayList.RemoveAt(lineToDelete);
            arrLine = arrayList.ToArray();
            File.WriteAllLines(fileName, arrLine);
            Console.WriteLine("Transaction successfully deleted.");
        }

        public void deleteTransactions()
        {
            transactions_path = @"Transactions.txt";
            transactionRecords = File.ReadAllLines(transactions_path);


            Console.WriteLine("Transaction Records");
            Console.WriteLine();
            Console.WriteLine("ID | Name | Amount | isExpense | Date | Category");

            //Display the recent transactions
            for (int i = 0; i < transactionRecords.Length; i++)
            {
                String[] transaction = transactionRecords[i].Split('|');
                Console.WriteLine($"{i} | {transaction[0]} | {transaction[1]} | {transaction[2]} | {transaction[3]} | {transaction[4]}");
            }

            Console.WriteLine("Enter the transaction ID that you want to delete:");
            int id = Convert.ToInt32(Console.ReadLine());
            String[] deleteRecord = transactionRecords[id].Split('|');

            //Reduce the transaction amount from the total
            reduceAmounts(deleteRecord);

            deleteTransactionLine("Transactions.txt", id);
        }

        public void reduceAmounts(String[] record)
        {
            if (record[4].Equals("Education"))
            {
                category = Education.getInstance();
                category.reduceTotal(Convert.ToDouble(record[1]));
            }
            else if (record[4].Equals("Transport"))
            {
                category = Transport.getInstance();
                category.reduceTotal(Convert.ToDouble(record[1]));
                Console.WriteLine("After removal " + category.getTotal());
            }
            else if (record[4].Equals("Income"))
            {
                category = Income.getInstance();
                category.reduceTotal(Convert.ToDouble(record[1]));
            }
        }

        public void editTransaction()
		{
            transactions_path = @"Transactions.txt";
            transactionRecords = File.ReadAllLines(transactions_path);


            Console.WriteLine("Transaction Records");
            Console.WriteLine();
            Console.WriteLine("ID | Name | Amount | isExpense | Date | Category");

            //Display the recent transactions
            for (int i = 0; i < transactionRecords.Length; i++)
            {
                String[] transaction = transactionRecords[i].Split('|');
                Console.WriteLine($"{i} | {transaction[0]} | {transaction[1]} | {transaction[2]} | {transaction[3]} | {transaction[4]}");
            }

            Console.WriteLine("Enter the transaction ID that you want to edit:");
            int id = Convert.ToInt32(Console.ReadLine());
            String[] updateRecord = transactionRecords[id].Split('|');
            Console.Clear();

            Console.WriteLine("Current Transaction Details as Below:");
            Console.WriteLine();
            Console.WriteLine("Name | Amount | isExpense | Date | Category");
            Console.WriteLine($"{updateRecord[0]} | {updateRecord[1]} | {updateRecord[2]} | {updateRecord[3]} | {updateRecord[4]}");
            Console.WriteLine();

            //Reduce the transaction amount from the total
            reduceAmounts(updateRecord);

            //Update the transaction
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

            String newRecord = $"{name}|{amount}|{isExpense}|{date}|{category.getCategoryName()}";
            Console.WriteLine("New record just after updated : " + newRecord);

            //Create the transaction object
            new Transaction(name, amount, isExpense, today, category.getCategoryName());

            Console.WriteLine("Category is : " + category.getCategoryName());

            //Replace the transaction line in the file
            replaceTransactionLine(newRecord,"Transactions.txt", id);
        }

	}
}

