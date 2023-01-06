using System;
namespace ExpenseTracker
{
	public class Education:Category
	{
		private static Education _instance;
		private String name;
		private bool isExpense;
		private Double totalSpent;

		private Education()
		{
			name = "Education";
			isExpense = false;
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
			return totalSpent;
		}

		public override void setTotal(Double transaction_amount)
		{
			totalSpent = totalSpent + transaction_amount;
			
		}
	}
}

