using System.Windows;
using TestApp.Infrastructure.Commands.Base;

namespace TestApp.Infrastructure.Commands
{
    internal class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
