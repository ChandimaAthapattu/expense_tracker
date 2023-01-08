using System;
namespace ExpenseTracker
{
	public class Health:Category
	{
        private static Health _instance;
        private String name;
        private bool isExpense;
        private Double totalAmount;

        private Health()
        {
            name = "Health";
            isExpense = true;
        }

        public static Health getInstance()
        {
            if (_instance is null)
            {
                _instance = new Health();
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

