using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
		private User user;
		//private bool isShowingRegister = false;

		private bool isShowingRegister = false;

		public bool IsShowingRegister
        {
			get { return isShowingRegister = false; }
			set
			{
                isShowingRegister = value;
                OnPropertyChanged("IsShowingRegister");
            }
		}


		public User User
		{
			get { return user; }
			set 
			{ 
				user = value; 
			}
		}
        public event PropertyChangedEventHandler PropertyChanged;

        private Visibility _loginVis;

        public Visibility LoginVis
        {
			get { return _loginVis; }
			set 
			{
                _loginVis = value;
				OnPropertyChanged("LoginVis"); 
			}
		}

        private Visibility _registerVis;

        public Visibility RegisterVis
        {
            get { return _registerVis; }
            set 
			{
                _registerVis = value; 
				OnPropertyChanged("RegisterVis"); 
			}
        }

        public RegisterCommand RegisterCommand{ get; set; }
        public LoginCommand LoginCommand{ get; set; }
		public ShowRegisterCommand ShowRegisterCommand { get; set; }	

        public LoginViewModel()
		{
			LoginVis = Visibility.Visible;
			RegisterVis = Visibility.Collapsed;

            RegisterCommand = new RegisterCommand(this);
			LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

        }

		public void SwitchViews()
		{
			isShowingRegister = !isShowingRegister;
			if(isShowingRegister)
			{
				RegisterVis = Visibility.Visible;
				LoginVis = Visibility.Collapsed;
			}
			else
			{
                RegisterVis = Visibility.Collapsed;
                LoginVis = Visibility.Visible;
            }
		}

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}
