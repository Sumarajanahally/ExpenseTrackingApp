using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExpenseTrackingApp.Models;
using Xamarin.Forms;
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
        private ObservableCollection<GroupedExpenseModel> GroupedExpenses { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            expenses = new List<Expense>();
            GetExpensesList();
            //GroupedExpenses = new ObservableCollection<GroupedExpenseModel>();
            //var foodExpenseGroup = new GroupedExpenseModel() { ExpenseCategoryName = "Food", ExpenseCategoryInitial = "F" };
            //var automobileExpenseGroup = new GroupedExpenseModel() { ExpenseCategoryName = "AutoMobile", ExpenseCategoryInitial = "A" };
            //var medicalExpenseGroup = new GroupedExpenseModel() { ExpenseCategoryName = "Medical", ExpenseCategoryInitial = "M" };
            //var utilitiesExpenseGroup = new GroupedExpenseModel() { ExpenseCategoryName = "Utlilities", ExpenseCategoryInitial = "U" };
            //var rentExpenseGroup = new GroupedExpenseModel() { ExpenseCategoryName = "Rent", ExpenseCategoryInitial = "R" };
            //var miscExpenseGroup = new GroupedExpenseModel() { ExpenseCategoryName = "Misc", ExpenseCategoryInitial = "MSc" };

            //foreach (var f in expenses.Where(t => t.Type == ExpenseType.Food).ToList())
            //{
            //    foodExpenseGroup.Add(new Expense() { Name = f.Name,Amount = f.Amount,FileName = f.FileName,Type = f.Type,Date = f.Date });
               
            //}

            //foreach (var a in expenses.Where(t => t.Type == ExpenseType.Automobile).ToList())
            //{
            //    automobileExpenseGroup.Add(new Expense() 
            //    { Name = a.Name, Amount = a.Amount, FileName = a.FileName, Type = a.Type, Date = a.Date });

            //}

            //foreach (var m in expenses.Where(t => t.Type == ExpenseType.Medical).ToList())
            //{
            //    medicalExpenseGroup.Add(new Expense() 
            //    { Name = m.Name, Amount = m.Amount, FileName = m.FileName, Type = m.Type, Date = m.Date });

            //}

            //foreach (var u in expenses.Where(t => t.Type == ExpenseType.Utilities).ToList())
            //{
            //    utilitiesExpenseGroup.Add(new Expense()
            //    { Name = u.Name, Amount = u.Amount, FileName = u.FileName, Type = u.Type, Date = u.Date });

            //}

            //foreach (var r in expenses.Where(t => t.Type == ExpenseType.Rent).ToList())
            //{
            //    rentExpenseGroup.Add(new Expense() 
            //    { Name = r.Name, Amount = r.Amount, FileName = r.FileName, Type = r.Type, Date = r.Date });

            //}

            //foreach (var msc in expenses.Where(t => t.Type == ExpenseType.Misc).ToList())
            //{
            //    miscExpenseGroup.Add(new Expense() 
            //    { Name = msc.Name, Amount = msc.Amount, FileName = msc.FileName, Type = msc.Type, Date = msc.Date });

            //}
            //GroupedExpenses.Clear();
            //GroupedExpenses.Add(foodExpenseGroup);
            //GroupedExpenses.Add(automobileExpenseGroup);
            //GroupedExpenses.Add(medicalExpenseGroup);
            //GroupedExpenses.Add(utilitiesExpenseGroup);
            //GroupedExpenses.Add(rentExpenseGroup);
            //GroupedExpenses.Add(miscExpenseGroup);
            //Lstview.ItemsSource = GroupedExpenses;
            
           Lstview.ItemsSource = expenses.OrderByDescending(t => t.Date);
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
                ExpenseType type = GetExpenseType(lines[3]);
                DateTime dateselected = DateTime.Parse(lines[4]);


                var expense = new Expense
                {
                    Amount = Double.Parse(amount),
                    Date = dateselected,
                    Name = desc,
                    Type = type,
                    FileName = file


                };
                expenses.Add(expense);
            }
        }
        private ExpenseType GetExpenseType(string value)
        {
            ExpenseType ex  ;

            switch (value)
            {
                case "Food": ex = ExpenseType.Food; 
                    break;
                case "Automobile": ex = ExpenseType.Automobile;
                    break;
                case "Medical": ex = ExpenseType.Medical;
                    break;
                case "Utilities": ex = ExpenseType.Utilities;
                    break;
                case "Rent": ex = ExpenseType.Rent;
                    break;
                case "Misc": ex = ExpenseType.Misc;
                    break;
                default: ex = ExpenseType.Misc;
                    break;
                
            }
            return ex;
        }
        private async void Listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new ExpensePage
            {
                BindingContext = (Expense)e.SelectedItem
            });

        }
    }
}