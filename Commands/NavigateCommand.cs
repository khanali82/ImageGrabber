using ImageGrabber.Services;
using ImageGrabber.Stores;
using ImageGrabber.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGrabber.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
