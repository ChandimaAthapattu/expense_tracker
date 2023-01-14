using System;
namespace ExpenseTracker
{
	interface Budget
	{
		public string getcategoryName();
        public double getBudget();
		public void setBudget(double budget);
	}
}

