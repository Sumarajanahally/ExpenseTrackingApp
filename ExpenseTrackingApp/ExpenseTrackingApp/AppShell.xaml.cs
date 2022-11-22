using System;
using System.Collections.Generic;

using ExpenseTrackingApp.Views;
using Xamarin.Forms;

namespace ExpenseTrackingApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ShellContent MainPageContent;
        public AppShell()
        {
            InitializeComponent();
            MainPageContent = Budget;
           
        }

    }
}
