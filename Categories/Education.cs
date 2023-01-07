using System;
namespace ExpenseTracker
{
	public class Education:Category
	{
		private static Education _instance;
		private String name;
		private bool isExpense;
		private Double totalAmount;

		private Education()
		{
			name = "Education";
			isExpense = true;
		}

		public static Education getInstance()
		{
			if (_instance is null)
			{
				_instance = new Education();
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

