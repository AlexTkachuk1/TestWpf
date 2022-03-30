using System;
using System.Windows.Input;

namespace TestApp.Infrastructure.Commands.Base
{
    internal abstract class BaseCommand : ICommand
    {
        public const string rest_url = @"https://localhost:44310/Rest/APIServerAsync/";

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
           remove => CommandManager.RequerySuggested -= value; 
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
