using System;
namespace ExpenseTracker
{
	public class Food:Category
	{
        private static Food _instance;
        private String name;
        private bool isExpense;
        private Double totalAmount;

        private Food()
        {
            name = "Food";
            isExpense = true;
        }

        public static Food getInstance()
        {
            if (_instance is null)
            {
                _instance = new Food();
            }
            return _instance;
        }

        public override String getCategoryName()
        {
            return name;
        }

        public override bool getCategoryType()
        {
            return isExpense;
        }

        public override Double getTotal()
        {
            return totalAmount;
        }

        public override void setTotal(Double transaction_amount)
        {
            totalAmount = totalAmount + transaction_amount;
        }

        public override void reduceTotal(Double amount)
        {
            totalAmount = totalAmount - amount;
        }
    }
}

