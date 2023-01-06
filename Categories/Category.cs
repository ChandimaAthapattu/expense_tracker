using System;
namespace ExpenseTracker
{
	public abstract class Category
	{

		public Category()
		{
		}

		public abstract String getCategoryName();

		public abstract bool getCategoryType();

		public abstract Double getTotal();

		public abstract void setTotal(Double amount);
        
    }
}

