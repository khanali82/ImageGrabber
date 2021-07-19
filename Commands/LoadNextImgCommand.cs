using ImageGrabber.Stores;
using ImageGrabber.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageGrabber.Commands
{
    class LoadNextImgCommand : AsyncCommandBase
    {
        private readonly ImageStore _imageStore;
        private ImageViewModel _imageViewModel;

        public LoadNextImgCommand(ImageStore imageStore, ImageViewModel imageViewModel)
        {
            _imageStore = imageStore;
            _imageViewModel = imageViewModel;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                int intInputID = int.Parse(_imageViewModel.InputId);
                intInputID += 1;
                _imageViewModel.InputId = intInputID.ToString();

                await _imageStore.GetImage(intInputID);

                _imageViewModel.ShowImage = _imageStore.CurrentImage.Bitmap != null ? true : false;
                _imageViewModel.ShowPicNotAvail = !_imageViewModel.ShowImage;

            }
            catch (Exception ex)
            {
                _imageViewModel.ShowImage = false;
                _imageViewModel.ShowPicNotAvail = !_imageViewModel.ShowImage;
                System.Diagnostics.Debug.WriteLine(ex, "Exception Happened");
            }
        }
    }
}
