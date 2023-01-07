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
        

        private string categoryBudgetPath;
        private string[] categoryBudgetRecords;

        public Budget newCategoryBudget(string categoryName, double targetAmount)
        {
            if (categoryBudgetList.ContainsKey(categoryName))
            {
                CategoryBudget categoryBudget = (CategoryBudget)categoryBudgetList[categoryName];
                categoryBudget.setBudget(targetAmount);
            }
            else
            {
                CategoryBudget categoryBudget = new CategoryBudget(categoryName, targetAmount);
                categoryBudgetList.Add(categoryName, categoryBudget);
                writeToFile(categoryName, targetAmount);
            }
            return (Budget)categoryBudget;
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
            string categoryName = category.getName();
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

