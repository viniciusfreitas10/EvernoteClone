using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesViewModel ViewModel{ get; set; }

        public EditCommand(NotesViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.StartEditing();
        }
    }
}
