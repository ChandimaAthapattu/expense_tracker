using System;
namespace ExpenseTracker
{
	public class Income:Category
	{
        private static Income _instance;
        private String name;
        private bool isExpense;
        private Double totalAmount;

        private Income()
		{
            name = "Income";
            isExpense = false;
        }

        public static Income getInstance()
        {
            if (_instance is null)
            {
                _instance = new Income();
            }
            return _instance;
        }

        public override string getCategoryName()
        {
            return name;
        }

        public override bool getCategoryType()
        {
            return isExpense;
        }

        public override double getTotal()
        {
            return totalAmount;
        }

        public override void setTotal(double amount)
        {
            totalAmount = totalAmount + amount;
        }

        public override void reduceTotal(Double amount)
        {
            totalAmount = totalAmount - amount;
        }
    }
}

