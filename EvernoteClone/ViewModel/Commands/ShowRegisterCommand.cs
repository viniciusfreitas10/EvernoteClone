using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public LoginViewModel vm { get; set; }

        public ShowRegisterCommand(LoginViewModel loginViewModel)
        {
            vm = loginViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            vm.SwitchViews();
        }
    }
}
