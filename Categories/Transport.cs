using System;
namespace ExpenseTracker
{
	public class Transport:Category
	{
        private static Transport _instance;
        private String name;
        private bool isExpense;
        private Double totalSpent;

        private Transport()
        { 
            name = "Transport";
			isExpense = false;
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
            return totalSpent;
        }

        public override void setTotal(Double transaction_amount)
        {
            totalSpent = totalSpent + transaction_amount;
        }
    }
}

