using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExpenseTrackingApp.Models
{

    public enum ExpenseType
    {
        Food,
        Automobile,
        Medical,
        Utilities,
        Rent,
        Misc
    }

    public enum Month
    {
        January,
        February,
        March,  
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    public class Expense
    {

        public string Name { get; set; }

        public DateTime Date { get; set; } 

        public double Amount { get; set; }

        public string FileName { get; set; }

        public ExpenseType  Type { get; set; }

        public Expense()
        { }
    }


    public class GroupedExpenseModel : ObservableCollection<Expense>
    {
        public string ExpenseCategoryName { get; set; }
        public string ExpenseCategoryInitial { get; set; }
        
    }
}
