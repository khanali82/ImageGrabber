using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGrabber.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        public ViewModelBase ContentViewModel { get; }

        public LayoutViewModel(ViewModelBase contentViewModel)
        {
            ContentViewModel = contentViewModel;
        }

        public override void Dispose()
        {
            ContentViewModel.Dispose();

            base.Dispose();
        }
    }
}
