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
		private String category;
        String transactions_path;
        String[] transactionRecords;


        public Transaction(String name, Double amount, bool isExpense, String date, String category)
		{
			this.name = name;
			this.amount = amount;
			this.isExpense = isExpense;
			this.date = date;
			this.category = category;
		}

        //public static Transaction getInstance(String name, Double amount, bool isExpense, String date, String category)
        //{
        //    if (_instance is null)
        //    {
        //        _instance = new Transaction(name,amount,isExpense,date,category);
        //    }
        //    return _instance;
        //}

        public void writeToFile()
		{
			//Construct the transaction to a string
			String transaction_record = $"{name}|{amount}|{isExpense}|{date}|{category}";
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

        public void readTransactionData()
        {
            //Read the transaction file content
            transactions_path = @"Transactions.txt";
            transactionRecords = File.ReadAllLines(transactions_path);

            ////Read the category file content
            //String categories_path = @"Categories.txt";
            //String[] categoryRecords = File.ReadAllLines(categories_path);

            ////Extract the categories
            //List<String> incomeCategories = new List<string>();
            //List<String> expenseCategories = new List<string>();

            //for (int x = 0; x < categoryRecords.Length; x++)
            //{
            //    String[] category = categoryRecords[x].Split(' ');
            //    if (category[1].Equals("true"))
            //    {
            //        incomeCategories.Add(category[0]);
            //    }
            //    else
            //    {
            //        expenseCategories.Add(category[0]);
            //    }
            //}
            //foreach (String w in incomeCategories)
            //{
            //    Console.WriteLine($"Income category {w}");
            //}
            //foreach (String w in expenseCategories)
            //{
            //    Console.WriteLine($"Expense category {w}");
            //}


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

        public void editTransaction()
		{
            transactions_path = @"Transactions.txt";
            transactionRecords = File.ReadAllLines(transactions_path);
            Category category = null;

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
            if (updateRecord[4].Equals("Education"))
            {
                category = Education.getInstance();
                category.reduceTotal(Convert.ToDouble(updateRecord[1]));
            }
            else if (updateRecord[4].Equals("Transport"))
            {
                category = Transport.getInstance();
                category.reduceTotal(Convert.ToDouble(updateRecord[1]));
            }
            else if (updateRecord[4].Equals("Income"))
            {
                category = Income.getInstance();
                category.reduceTotal(Convert.ToDouble(updateRecord[1]));
            }

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

