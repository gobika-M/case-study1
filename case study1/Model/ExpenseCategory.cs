using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace case_study1.Model
{
    internal class ExpenseCategory
    {
        private int categoryId;
        private string categoryName;

        // Default constructor
        public ExpenseCategory() { }

        // Parameterized constructor
        public ExpenseCategory(int categoryId, string categoryName)
        {
            this.categoryId = categoryId;
            this.categoryName = categoryName;
        }

        // Getters and Setters (Properties)
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
    }
}
