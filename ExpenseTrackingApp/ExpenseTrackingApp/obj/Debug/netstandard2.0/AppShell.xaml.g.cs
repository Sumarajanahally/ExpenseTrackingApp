//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("ExpenseTrackingApp.AppShell.xaml", "AppShell.xaml", typeof(global::ExpenseTrackingApp.AppShell))]

namespace ExpenseTrackingApp {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("AppShell.xaml")]
    public partial class AppShell : global::Xamarin.Forms.Shell {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ShellContent Budget;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ShellContent Expenses;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(AppShell));
            Budget = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ShellContent>(this, "Budget");
            Expenses = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ShellContent>(this, "Expenses");
        }
    }
}
