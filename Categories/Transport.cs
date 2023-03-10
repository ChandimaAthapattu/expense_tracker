using System;
namespace ExpenseTracker
{
	public class Transport:Category
	{
        private static Transport _instance;
        private String name;
        private bool isExpense;
        private Double totalAmount;

        private Transport()
        { 
            name = "Transport";
			isExpense = true;
		}

        public static Transport getInstance()
        {
            if (_instance is null)
            {
                _instance = new Transport();
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

