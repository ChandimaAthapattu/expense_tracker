using System;
using System.Diagnostics;

namespace ExpenseTracker
{
    public class Program
    {
        
        static Category category = null;
        static Transaction transaction = null;
        //Read the transaction file content
        static String transactions_path = @"Transactions.txt";
        static String[] transactionRecords = File.ReadAllLines(transactions_path);
        static String transactionRecord;

        public static void Main(String[] args)
        {

            readTransactionData();
            Display_main();

            //createTransaction();

            ////Get the category wise spent amounts
            //category = Education.getInstance();
            //Console.WriteLine($"Spent amount for Education: {category.getTotal()}");
            //category = Transport.getInstance();
            //Console.WriteLine($"Spent amount for Transport: {category.getTotal()}");

            ////Get the total income
            //category = Income.getInstance();
            //Console.WriteLine($"Total Income: {category.getTotal()}");

            ////transaction.editTransaction();

            ////transaction.deleteTransactions();

            ////*********************************Add by Lohitha********************************
            //// Create Budget factory object
            //BudgetFactory bf = new BudgetFactory();

            //// Get data from the text file
            //bf.readTransactionData();

            //// Get total budget
            //Console.WriteLine(bf.getTotalBudget());

            ////Get category budget
            ////Assuming that categories are in an array or arrayList
            //List<string> catogeryList = new List<string>();

            ////Add objects to categoryList
            //catogeryList.Add("Transport");
            //catogeryList.Add("Food");
            //catogeryList.Add("Education");

            ////Loop to call category budget
            //for (int i = 0; i < catogeryList.Count; i++)
            //{
            //    string categoryName = catogeryList[i];
            //    Console.WriteLine(categoryName);
            //    Budget budg = bf.getCategoryBudget(categoryName);
            //    Console.WriteLine(budg.getcategoryName() + ", Budget Amount - " + budg.getBudget());
            //}

            ////*********************************************************************************

        }

        public static void readTransactionData()
        {
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
            Console.WriteLine("1. Enter the transaction name:");
            String name = Console.ReadLine();

            Console.WriteLine("2. Enter the transaction amount:");
            Double amount = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("3. Enter the transaction type (0 - Income, 1 - Expense):");
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

            Console.WriteLine("4. Enter the transaction category:");
            Console.WriteLine("(1 - Income, 2 - Education, 3 - Transport, 4 - Food, 5 - Health)");
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
            else if (transaction_category.Equals("4"))
            {
                category = Food.getInstance();
            }
            else if (transaction_category.Equals("5"))
            {
                category = Health.getInstance();
            }

            //Append the transaction amount to category
            //Console.WriteLine("Before add the amount " + category.getTotal());
            category.setTotal(amount);
            //Console.WriteLine("After added the amount " + category.getTotal());

            //Create the trasnaction record
            transactionRecord = $"{name}|{amount}|{isExpense}|{today}|{category.getCategoryName()}";

            //Create the transaction object
            transaction = new Transaction(name, amount, isExpense, today, category.getCategoryName());
        }

        public static void viewTransactions()
        {
            String transactions_path = @"Transactions.txt";
            String[] transactionRecords = File.ReadAllLines(transactions_path);


            Console.WriteLine("Transaction Records :");
            Console.WriteLine();
            Console.WriteLine("ID | Name | Amount | isExpense | Date | Category");

            //Display the recent transactions
            for (int i = 0; i < transactionRecords.Length; i++)
            {
                String[] transaction = transactionRecords[i].Split('|');
                Console.WriteLine($"{i} | {transaction[0]} | {transaction[1]} | {transaction[2]} | {transaction[3]} | {transaction[4]}");
            }
        }

        public static void editTransactions()
        {
            viewTransactions();
            Console.WriteLine();
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

            //Update the transaction details
            createTransaction();

            //Replace the transaction line in the file
            replaceTransactionLine(transactionRecord, "Transactions.txt", id);
        }

        public static void deleteTransactions()
        {
            viewTransactions();
            Console.WriteLine();
            Console.WriteLine("Enter the transaction ID that you want to delete:");
            int id = Convert.ToInt32(Console.ReadLine());
            String[] deleteRecord = transactionRecords[id].Split('|');

            //Reduce the transaction amount from the total
            reduceAmounts(deleteRecord);
            deleteTransactionLine("Transactions.txt", id);
        }

        public static void replaceTransactionLine(String newText, String fileName, int lineToEdit)
        {
            String[] arrLine = File.ReadAllLines(fileName);
            Console.WriteLine("Line before edit : " + arrLine[lineToEdit]);
            arrLine[lineToEdit] = newText;
            Console.WriteLine("Line after edit : " + arrLine[lineToEdit]);
            File.WriteAllLines(fileName, arrLine);
        }

        public static void deleteTransactionLine(String fileName, int lineToDelete)
        {
            String[] arrLine = File.ReadAllLines(fileName);
            List<String> arrayList = new List<string>(arrLine);
            arrayList.RemoveAt(lineToDelete);
            arrLine = arrayList.ToArray();
            File.WriteAllLines(fileName, arrLine);
            Console.WriteLine();
            Console.WriteLine("Transaction successfully deleted.");
        }

        public static void reduceAmounts(String[] record)
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
            else if (record[4].Equals("Food"))
            {
                category = Food.getInstance();
                category.reduceTotal(Convert.ToDouble(record[1]));
            }
            else if (record[4].Equals("Health"))
            {
                category = Health.getInstance();
                category.reduceTotal(Convert.ToDouble(record[1]));
            }
        }

        public static void Display_main()
        {
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("$$$$ Personal Capital management System - The Expense Tracking and Budgeting Application $$$$ ");
            Console.WriteLine("=================================================================================================\n");
            Console.WriteLine();
            Console.WriteLine("==================================== Main Menu ===================================================");
            Console.WriteLine();
            Console.WriteLine("1. To Access Viewer Mode");
            Console.WriteLine();
            Console.WriteLine("2. To Access Editor Mode");
            Console.WriteLine();
            Console.WriteLine("3. To Exit The System");
            Console.WriteLine();
            Console.WriteLine("==================================================================================================");

            int input_main = Convert.ToInt32(Console.ReadLine());

            if (input_main == 1)
            {
                Console.Clear();
                Display_Viewmode_Selection_M();
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == 1)
                {
                    //View Transaction
                    Console.Clear();
                    viewTransactions();
                    exit();

                }
                else if (input == 2)
                {
                    //View budget
                    Console.Clear();
                    Console.WriteLine("Budget View");
                    exit();

                }
                else if (input == 3)
                {
                    //View Categories
                    Console.Clear();
                    Console.WriteLine("Expense Categories:");
                    Console.WriteLine();
                    Console.WriteLine("- Education");
                    Console.WriteLine("- Food");
                    Console.WriteLine("- Health");
                    Console.WriteLine("- Transport");
                    Console.WriteLine();
                    Console.WriteLine("Income Categories:");
                    Console.WriteLine();
                    Console.WriteLine("- Income");
                    Console.WriteLine();
                    exit();
                }
                else
                {
                    Console.Clear();
                    Display_main();
                }
            }
            else if (input_main == 2)
            {
                Console.Clear();
                Display_Editormode_Selection_M();
                int input = Convert.ToInt32(Console.ReadLine());

                if (input == 1)
                {
                    Console.Clear();
                    Display_Editormode_Transaction_M();
                    input = Convert.ToInt32(Console.ReadLine());

                    if (input == 1)
                    {
                        //Create new transaction
                        Console.Clear();
                        createTransaction();
                        transaction.writeToFile();
                        exit();
                    }
                    else if (input == 2)
                    {
                        //Edit transaction
                        Console.Clear();
                        editTransactions();
                        exit();
                    }
                    else if (input == 3)
                    {
                        //Delete transaction
                        Console.Clear();
                        deleteTransactions();
                        exit();
                    }
                    else
                    {
                        Console.Clear();
                        Display_main();
                    }
                }
                else if (input == 2)
                {
                    Console.Clear();
                    Display_Editormode_Budget_M();
                    input = Convert.ToInt32(Console.ReadLine());

                    if (input == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Create a budget");
                        //Create budget
                        exit();
                    }
                    else
                    {
                        Console.Clear();
                        Display_main();
                    }
                }
                else
                {
                    Console.Clear();
                    Display_main();
                }
            }
            else
            {
                //Program exited.
            }
        }

        public static void Display_Viewmode_Selection_M()
        {
            Console.WriteLine("**************************You are in Viewer Mode Selection Menue********************* ");
            Console.WriteLine(" ");
            Console.WriteLine("============================View Mode Selection Menue=========================== ");
            Console.WriteLine(" ");
            Console.WriteLine("1. To view Transactions");
            Console.WriteLine(" ");
            Console.WriteLine("2. To View Budget");
            Console.WriteLine(" ");
            Console.WriteLine("3. To View Categories");
            Console.WriteLine(" ");
            Console.WriteLine("4. Back to Main Menu");
            Console.WriteLine(" ");
            Console.WriteLine("==========================================================================================");
        }

        public static void exit()
        {
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine();
            Console.WriteLine("Do you want to exit? (Y or N) :");
            String answer = Console.ReadLine();
            if(answer.Equals("N") || answer.Equals("n"))
            {
                Console.Clear();
                Display_main();
            }
        }

        public static void Display_Editormode_Selection_M()
        {
            Console.WriteLine("**************************You are in Editor Mode Selection Menue********************* ");
            Console.WriteLine(" ");
            Console.WriteLine("============================Editor Mode Selection Menue=========================== ");
            Console.WriteLine(" ");
            Console.WriteLine("1. Editor mode Transaction Menu");
            Console.WriteLine(" ");
            Console.WriteLine("2. Editor mode Budget Menu");
            Console.WriteLine(" ");
            Console.WriteLine("3. Back to Main Menu");
            Console.WriteLine(" ");
            Console.WriteLine("==========================================================================================");
        }

        public static void Display_Editormode_Transaction_M()
        {
            Console.WriteLine("**************************You are in Editor Mode Transaction Menue********************* ");
            Console.WriteLine(" ");
            Console.WriteLine("============================Editor Mode Transaction Menue=========================== ");
            Console.WriteLine(" ");
            Console.WriteLine("1. To Create new transaction");
            Console.WriteLine(" ");
            Console.WriteLine("2. To Edit transaction");
            Console.WriteLine(" ");
            Console.WriteLine("3. to Delete transaction");
            Console.WriteLine(" ");
            Console.WriteLine("4. Back to Main Menu");
            Console.WriteLine(" ");
            Console.WriteLine("==========================================================================================");
        }

        public static void Display_Editormode_Budget_M()
        {
            Console.WriteLine("**************************You are in Editor Mode Budget Menue********************* ");
            Console.WriteLine(" ");
            Console.WriteLine("============================Editor Mode Budget Menue=========================== ");
            Console.WriteLine(" ");
            Console.WriteLine("1. To create new budget");
            Console.WriteLine(" ");
            Console.WriteLine("2. Back to Main Menu");
            Console.WriteLine(" ");
            Console.WriteLine("==========================================================================================");
        }
    }
}
