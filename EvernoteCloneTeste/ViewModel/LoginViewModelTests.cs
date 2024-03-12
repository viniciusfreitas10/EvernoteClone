using EvernoteClone.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace EvernoteCloneTeste.ViewModel
{
    [TestClass]
    public class LoginViewModelTests
    {
        [TestMethod("Test for SwitchViews vissibility or collapsed")]
        public void SwitchViewsTest()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            // Act
            loginViewModel.SwitchViews();

            bool initialIsShowingRegister = loginViewModel.IsShowingRegister;
            Visibility initialRegisterVis = loginViewModel.RegisterVis;
            Visibility initialLoginVis = loginViewModel.LoginVis;
            // Assert
            if (initialIsShowingRegister)
            {
                Assert.AreEqual(Visibility.Collapsed, initialRegisterVis);
                Assert.AreEqual(Visibility.Visible, initialLoginVis);
            }
            else
            {
                Assert.AreEqual(Visibility.Visible, initialRegisterVis);
                Assert.AreEqual(Visibility.Collapsed, initialLoginVis);
            }

            //Assert.IsTrue(true); 
        }
    }
}
