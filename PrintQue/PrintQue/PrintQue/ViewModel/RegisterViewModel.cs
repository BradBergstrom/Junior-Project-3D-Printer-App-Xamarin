﻿using PrintQue.Models;
using PrintQue.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PrintQue.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        public RegisterCommand RegisterCommand { get; set; }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                User = new User()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("LastName");
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                User = new User()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("FirstName");
            }
        }


        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new User()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;
        private string firstName;
        private string lastName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User()
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("Password");
            }

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public RegisterViewModel()
        {
            User = new User();
            RegisterCommand = new RegisterCommand(this);
        }
        public async void Register()
        {
            int canRegister = await User.Register(User.Email, User.Password, User.FirstName, User.LastName);
            switch (canRegister)
            {
                case 0:
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "Try again", "OK");
                    break;
                case 1:
                    var Num = await User.Insert(user);
                    if (Num > 0)
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Success!", "You have successfully logged in!", "OK");
                        await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Failure!", "An error was encountered when registering", "OK");
                    }
                    break;

            }
        }
    }
}
