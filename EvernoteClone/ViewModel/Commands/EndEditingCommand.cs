using EvernoteClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndEditingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public NotesViewModel NotesViewModel { get; set; }

        public EndEditingCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Notebook notebookSelected = parameter as Notebook;
            if (notebookSelected != null)
            {
                NotesViewModel.StopedEditing(notebookSelected);
            }
        }
    }
}
