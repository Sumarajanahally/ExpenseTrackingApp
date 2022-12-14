using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using ExpenseTrackingApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;
using static Xamarin.Essentials.Permissions;
using static Xamarin.Forms.Internals.GIFBitmap;

namespace ExpenseTrackingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private List<Expense> expenses;
        private string budgetFileName, budgetmonth, budgetamunt;
        double totalexpense;

        private List<Expense> expensesByMonth = new List<Expense>();
        public MainPage()
        {
            InitializeComponent();
            MonthPicker.SelectedIndex = DateTime.Now.Month - 1;//current month selected by default
          

        }


        protected override void OnAppearing()
        {
            PageLoad();
        }


        private void PageLoad()
        {
            expenses = new List<Expense>();
            GetExpensesList();
            BudgetAmount.Text = string.Empty;
            GetBudget(MonthPicker.SelectedItem.ToString());
            expensesByMonth.Clear();
            expensesByMonth = GetExpenseByMonth(MonthPicker.SelectedItem.ToString());
            totalexpense = expensesByMonth.Sum(t => t.Amount);
            Lstview.ItemsSource = expensesByMonth.OrderByDescending(t => t.Type);
            lblSpent.Text = "Amount Spent: $" + totalexpense.ToString();
            double amountRemaining;
            if (!string.IsNullOrEmpty(budgetamunt))
            { amountRemaining = Double.Parse(budgetamunt) - totalexpense; }
            else
            {
                amountRemaining = 0;
            }
             lblRemaining.Text = " Amount Remaining: $" + amountRemaining.ToString(); 

        }
        private void GetBudget(string month)
        {
            var files = Directory.EnumerateFiles(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), $"*.{month}.budget.txt");
            budgetFileName = string.Empty;
            budgetmonth = string.Empty;
            budgetamunt = string.Empty;
            foreach (var file in files)
            {
                var allText = System.IO.File.ReadAllText(file);
                string[] lines = allText.Split('\n');
                budgetFileName = lines[0];
                budgetmonth = lines[1];
                budgetamunt = lines[2];
            }


            BudgetAmount.Text = budgetamunt;
        }

        private void GetExpensesList()
        {
            var files = Directory.EnumerateFiles(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "*.expense.txt");

            foreach (var file in files)
            {
                var allText = System.IO.File.ReadAllText(file);
                string[] lines = allText.Split('\n');
                var amount = lines[1];
                var desc = lines[2];
                ExpenseType type = (ExpenseType)Enum.Parse(typeof(ExpenseType), lines[3]);
                DateTime dateselected = DateTime.Parse(lines[4]);
                string typeImageFile = GetImage(type);

                var expense = new Expense
                {
                    Amount = Double.Parse(amount),
                    Date = dateselected,
                    Name = desc,
                    Type = type,
                    FileName = file,
                    TypeIconFileName = typeImageFile

                };
                expenses.Add(expense);
            }
        }

        private string GetImage(ExpenseType type)
        {
            string path;
            switch (type)
            {
                case ExpenseType.Rent:
                    path = "Rent.png";
                    break;
                case ExpenseType.Automobile:
                    path = "Automobile.png";
                    break;
                case ExpenseType.Food:
                    path = "Food.png";
                    break;
                case ExpenseType.Misc:
                    path = "Misc.png";
                    break;
                case ExpenseType.Utilities:
                    path = "Utilities.png";
                    break;
                case ExpenseType.Medical :
                    path = "Medical.png";
                    break;

                default:
                    path = "xamarin_logo.png";
                    break;

                    
            }
            return path;
        }

        private async void Listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = (Expense)e.SelectedItem
            });

        }

        private void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageLoad();
        }

        private async void   Save_Clicked(object sender, EventArgs e)
        {
            var month = MonthPicker.SelectedItem;
            var monthBudget = BudgetAmount.Text;
            var allText = string.Empty;
            if (string.IsNullOrEmpty(budgetFileName))
            {
                budgetFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                $"{Path.GetRandomFileName()}.{month}.budget.txt");
            }

            if (string.IsNullOrEmpty(monthBudget) || !(double.TryParse(monthBudget, out var amount)))
            {
                await DisplayAlert("Alert", "Please enter a valid number", "OK");
            }
            else
            {
                allText += budgetFileName + "\n";
                allText += month + "\n";
                allText += monthBudget + "\n";
                System.IO.File.WriteAllText(budgetFileName, allText);
                await DisplayAlert("Alert", $"{month} budget is set to {monthBudget}.", "OK");

            }
           Shell.Current.CurrentItem  = (Shell.Current as AppShell).ExpensePageContent;


        }

        private List<Expense> GetExpenseByMonth(string month)
        {
           
            int iMonth = (int)Enum.Parse(typeof(Month), month) +1;
            List<Expense> filteredList = new List<Expense>();
            filteredList = expenses.Where(t => t.Date.Month.Equals(iMonth)).ToList();
           
            return filteredList;
        }
    }
}