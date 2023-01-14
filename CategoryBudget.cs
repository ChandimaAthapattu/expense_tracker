using System;
using System.Collections.Generic;
using System.Text;
namespace ExpenseTracker
{
    class CategoryBudget : Budget
    {
        private string categoryName;
        private double targetAmount;


        public CategoryBudget(string categoryName, double targetAmount)
        {
            this.categoryName = categoryName;
            this.targetAmount = targetAmount;
        }

        public CategoryBudget(Category category, double targetAmount)
        {
            this.categoryName = category.getCategoryName();
            this.targetAmount = targetAmount;
        }

        public string getcategoryName()
        {
            return categoryName;
        }

        public double getBudget()
        {
            return targetAmount;
        }

        public void setBudget(double budget)
        {
            this.targetAmount = budget;
        }

    }
}
