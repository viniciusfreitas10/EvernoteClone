using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged(nameof(SelectedNotebook));
                getNotes();
            }
        }

        private Visibility isVisibility;
        public Visibility IsVisibility
        {
            get { return isVisibility; }
            set 
            { 
                isVisibility = value; 
                OnPropertyChanged(nameof(IsVisibility));
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set 
            { 
                selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
                SelectedNoteChanged?.Invoke(this, new EventArgs()); 
            }
        }


        public NewNotebookCommand newNotebookCommand { get; set; }
        public NewNoteCommand newNoteCommand { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public EditCommand editCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;
        public EndEditingCommand endEditingCommand { get; set; }    

        public NotesViewModel()
        {
            newNotebookCommand = new NewNotebookCommand(this);
            newNoteCommand = new NewNoteCommand(this);
            editCommand = new EditCommand(this);
            endEditingCommand = new EndEditingCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisibility = Visibility.Collapsed;

            getNotebooks();
        }

        public void CreateNote(int notbookId)
        {
            Note newNot = new Note()
            {
                NotebookId = notbookId,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Title = "New note"
            };

            DataBaseHelpers.Insert<Note>(newNot);

            getNotes();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New NoteBook"
            };
            DataBaseHelpers.Insert<Notebook>(newNotebook);

            getNotebooks();
        }

        public void getNotebooks()
        {
            var notebooks = DataBaseHelpers.Read<Notebook>();
            Notebooks.Clear();
            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void getNotes()
        {
            if (SelectedNotebook != null)
            {
                var notes = DataBaseHelpers.Read<Note>()
                    .Where(n => n.NotebookId.Equals(selectedNotebook.Id)).ToList();
                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            IsVisibility = Visibility.Visible;
        }
        public void StopedEditing(Notebook notebook)
        {
            IsVisibility = Visibility.Collapsed;
            DataBaseHelpers.Update<Notebook>(notebook);
            getNotebooks();
        }
    }
}
