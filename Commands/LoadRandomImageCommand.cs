using ImageGrabber.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageGrabber.Commands
{
    class LoadRandomImageCommand : AsyncCommandBase
    {
        private readonly ImageStore _imageStore;

        public LoadRandomImageCommand(ImageStore imageStore)
        {
            _imageStore = imageStore;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                int random_number = new Random().Next(1, 1084);
                //InputId = Convert.ToString(random_number);
                await _imageStore.GetImage(random_number);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex, "Exception Happened");
            }
        }
    }
}
