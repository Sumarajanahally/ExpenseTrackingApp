using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpenseTrackingApp.Models;
using System.IO;

namespace ExpenseTrackingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensePage : ContentPage
    {
        public ExpensePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var expense = (Expense)BindingContext;
            if(expense!= null && !String.IsNullOrEmpty(expense.FileName))
            {
                var allText = File.ReadAllText(expense.FileName);
                string[] lines = allText.Split('\n');
                ExpenseAmount.Text = lines[1];
                ExpenseDesc.Text = lines[2];
                ExpenseT.SelectedItem = lines[3];
                ExpenseDate.Date = DateTime.Parse( lines[4]);
                   
            }
        }
        private void ExpenseDate_DateSelected(object sender, DateChangedEventArgs e)
        {

        }


        private async void Save_Clicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            //var allText = $@"{ExpenseDesc.Text}
            //                 {ExpenseAmount.Text}
            //                 {ExpenseT.SelectedItem.ToString()}
            //                 {ExpenseDate.Date}";
            var allText = "";

            if (expense == null || string.IsNullOrEmpty(expense.FileName))
            {
                expense = new Expense();
                expense.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    $"{Path.GetRandomFileName()}.expense.txt");
            }
            allText += expense.FileName + "\n";

            if (string.IsNullOrWhiteSpace(ExpenseAmount.Text) || !(double.TryParse(ExpenseAmount.Text, out var amount)))
            {
                await DisplayAlert("Alert", "Please enter a valid number", "OK");
            }
            else
            {
                allText += ExpenseAmount.Text + "\n";
                allText += ExpenseDesc.Text + "\n";
                allText += ExpenseT.SelectedItem.ToString() + "\n";
                allText += ExpenseDate.Date.ToString() + "\n";
                File.WriteAllText(expense.FileName, allText);


                if (Navigation.ModalStack.Count > 0)
                {
                    ExpenseAmount.Text = string.Empty;
                    ExpenseDesc.Text = string.Empty;
                    ExpenseT.SelectedItem = null;
                    ExpenseDate.Date = DateTime.Today;
                    await Navigation.PopModalAsync();
                }
                else
                {
                    ExpenseAmount.Text = string.Empty;
                    ExpenseDesc.Text = string.Empty;
                    ExpenseT.SelectedItem = null;
                    ExpenseDate.Date = DateTime.Today;
                    Shell.Current.CurrentItem = (Shell.Current as AppShell).MainPageContent;
                }
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            if(File.Exists(expense.FileName))
            {
                File.Delete(expense.FileName);
            }
            ExpenseAmount.Text = string.Empty;
            ExpenseDesc.Text = string.Empty;
            ExpenseT.SelectedItem = null;
            ExpenseDate.Date = DateTime.Today ;
            await Navigation.PopModalAsync();
        }
    }
}