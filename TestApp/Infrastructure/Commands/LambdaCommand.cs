using System;
using TestApp.Infrastructure.Commands.Base;

namespace TestApp.Infrastructure.Commands
{
    internal class LambdaCommand : BaseCommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute ;
        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => execute(parameter);
    }
}
