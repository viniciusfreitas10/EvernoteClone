﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginViewModel VM { get; set; }  
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginViewModel vm) 
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           //TODO: Login functionality
        }
    }
}