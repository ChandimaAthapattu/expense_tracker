using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExpenseTracker
{
class BudgetFactory
    {
        private Dictionary<string, CategoryBudget> categoryBudgetList = new Dictionary<string, CategoryBudget>();
        private Budget categoryBudget;
        double totalBudget;
        public Double allocateBudget, remainingTotalBudget, totalIncome;

        private string categoryBudgetPath;
        private string[] categoryBudgetRecords;

        public Budget newCategoryBudget(string categoryName, double targetAmount)
        {
            if (categoryBudgetList.ContainsKey(categoryName))
            {
                CategoryBudget categoryBudget = (CategoryBudget)categoryBudgetList[categoryName];
                allocateBudget = allocateBudget - getCategoryBudget(categoryName).getBudget();
                allocateBudget = allocateBudget + targetAmount;
                remainingTotalBudget = remainingTotalBudget + getCategoryBudget(categoryName).getBudget();
                remainingTotalBudget = remainingTotalBudget - targetAmount;
                Console.WriteLine($"Allocate budget AFTER OVERWRITE {allocateBudget}");
                Console.WriteLine($"Remaining total budget AFTER OVERWRITE {remainingTotalBudget}");
                categoryBudget.setBudget(targetAmount);
                //Update the file
                
            }
            else
            {
                CategoryBudget categoryBudget = new CategoryBudget(categoryName, targetAmount);
                categoryBudgetList.Add(categoryName, categoryBudget);
                writeToFile(categoryName, targetAmount);
                allocateBudget = allocateBudget + targetAmount;
                remainingTotalBudget = remainingTotalBudget - targetAmount;
                Console.WriteLine($"Allocate budget AFTER {allocateBudget}");
                Console.WriteLine($"Remaining total budget AFTER {remainingTotalBudget}");
            }
            return (Budget)categoryBudget;
        }

        public void createBudget(String transaction_category)
        {
            Console.WriteLine();
            Console.WriteLine("2. Enter the target budget amount:");
            Double targetAmount = Convert.ToDouble(Console.ReadLine());

            Category category = Income.getInstance();
            totalIncome = category.getTotal();
            if (allocateBudget==0)
            {
                remainingTotalBudget = totalIncome;
            }
            
            Console.WriteLine($"Allocate budget {allocateBudget}");
            Console.WriteLine($"Remaining total budget {remainingTotalBudget}");
            Console.WriteLine($"Total Income {totalIncome}");

            if (targetAmount <= totalIncome)
            {
                if (targetAmount <= remainingTotalBudget)
                {
                    newCategoryBudget(transaction_category, targetAmount);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Your budget amount is over the remaining balance.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Your budget amount is over the total income.");
            }
            Program.exit();
        }

        public Budget getCategoryBudget(string categoryName)
        {
            if (categoryBudgetList.ContainsKey(categoryName))
            {
                CategoryBudget categoryBudget = (CategoryBudget)categoryBudgetList[categoryName];
                Console.WriteLine("Sending Existing Category Budget");
                return categoryBudget;
            }
            else
            {
                return null;
            }
        }

        //Alternative Method
        public Budget getCategoryBudget(Category category)
        {
            string categoryName = category.getCategoryName();
            if (categoryBudgetList.ContainsKey(categoryName))
            {
                CategoryBudget categoryBudget = (CategoryBudget)categoryBudgetList[categoryName];
                return categoryBudget;
            }
            else
            {
                return null;
            }
        }

        public void writeToFile(string categoryName, double targetAmount)
        {
            //Construct the Category Budget to a string
            String categoryBudget_record = $"{categoryName}|{targetAmount}";
            Console.WriteLine();
            Console.WriteLine($"Budget is {categoryBudget_record}");

            //Creating the file
            String file = @"CategoryBudget.txt";

            //Append the text
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(categoryBudget_record);
            }
        }

        
        public void readTransactionData()
        {
            //Read the Budget file content
            categoryBudgetPath = @"CategoryBudget.txt";
            categoryBudgetRecords = File.ReadAllLines(categoryBudgetPath);


            //Read each Budget line
                       
            for (int i = 0; i < categoryBudgetRecords.Length; i++)
            {
                String[] categoryBudget = categoryBudgetRecords[i].Split('|');
                string categoryNameFile = categoryBudget[0];
                double budgetFile = System.Convert.ToDouble(categoryBudget[1]);

                Console.WriteLine("Read from file : Name -" + categoryNameFile + ", Budget - " + budgetFile);
                categoryBudgetList.Add(categoryNameFile, new CategoryBudget(categoryNameFile, budgetFile));
            }
        }
        
        public double getTotalBudget()
        {
            foreach (var value in categoryBudgetList.Values)
            {
                totalBudget = totalBudget + value.getBudget();
            }
            return totalBudget;
        }
    }
}


