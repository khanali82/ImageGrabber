using ImageGrabber.Commands;
using ImageGrabber.Models;
using ImageGrabber.Services;
using ImageGrabber.Stores;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageGrabber.ViewModels
{
    class ImageViewModel : ViewModelBase
    {
        private readonly ImageStore _imageStore;

        private string _inputId;
        public string InputId
        {
            get => _inputId;
            set
            {
                _inputId = value;
                OnPropertyChanged(nameof(InputId));
            }
        }


        public int ID => _imageStore.CurrentImage.ID;
        public string ImageURI => _imageStore.CurrentImage.ImageURI;
        public BitmapImage Bitmap => _imageStore.CurrentImage.Bitmap;

        private bool _showImage;
        public bool ShowImage
        {
            get => _showImage;
            set
            {
                _showImage = value;
                OnPropertyChanged(nameof(ShowImage));
            }
        }


        private bool _showPicNotAvail;
        public bool ShowPicNotAvail
        {
            get => _showPicNotAvail;
            set
            {
                _showPicNotAvail = value;
                OnPropertyChanged(nameof(ShowPicNotAvail));
            }
        }

        public ICommand NavigateHomeCommand { get; }
        public ICommand LoadImageCommand { get; }      
        
        public ICommand LoadRandomImageCommand { get; }

        public ICommand LoadNextImgCommand { get; }

        public ICommand LoadPreviousImgCommand { get; }

        public DelegateCommand<object> RandomCommand { get; } // No utilized - functionality moved LoadRandomImageCommand

        public DelegateCommand<object> LoadNextImageCommand { get; } // No utilized - functionality moved LoadRandomImageCommand

        public DelegateCommand<object> LoadPreviousImageCommand { get; } // No utilized - functionality moved LoadRandomImageCommand

        public ImageViewModel(ImageStore imageStore, INavigationService homeNavigationService)
        {
            _imageStore = imageStore;
            _imageStore.CurrentImageChanged += OnCurrentImageChanged;

            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            LoadImageCommand = new LoadImageCommand(_imageStore, this);
            LoadRandomImageCommand = new LoadRandomImageCommand(_imageStore, this);
            LoadNextImgCommand = new LoadNextImgCommand(_imageStore, this);
            LoadPreviousImgCommand = new LoadPreviousImgCommand(_imageStore, this);

            // Not using Delegation any more
            RandomCommand = new DelegateCommand<object>(RandomCommand_Execute);
            LoadNextImageCommand = new DelegateCommand<object>(LoadNextImageCommand_Execute);
            LoadPreviousImageCommand = new DelegateCommand<object>(LoadPreviousImageCommand_Execute);
        }

        private async void LoadPreviousImageCommand_Execute(object arg)
        {

            bool success = int.TryParse(InputId, out int intInputID);

            if (success) {
                intInputID -= 1;
                InputId = intInputID.ToString();
                await _imageStore.GetImage(intInputID);
                ShowImage = Bitmap != null ? true : false;
                ShowPicNotAvail = !ShowImage;
            }
            else
            {
                ShowImage = false;
                ShowPicNotAvail = !ShowImage;
                Console.WriteLine("Attempted conversion of '{0}' failed.", InputId ?? "<null>");
            }
        }

        private async void LoadNextImageCommand_Execute(object arg)
        {

            bool success = int.TryParse(InputId, out int intInputID);

            if (success)
            {
                intInputID += 1;
                InputId = intInputID.ToString();
                await _imageStore.GetImage(intInputID);
                ShowImage = Bitmap != null ? true : false;
                ShowPicNotAvail = !ShowImage;
            }
            else
            {
                ShowImage = false;
                ShowPicNotAvail = !ShowImage;
                Console.WriteLine("Attempted conversion of '{0}' failed.", InputId ?? "<null>");
            }
        }

        private async void RandomCommand_Execute(object arg)
        {

            Random random = new Random();
            _imageStore.CurrentImage.Bitmap = null;

            while (_imageStore.CurrentImage.Bitmap == null)
            {
                int random_number = random.Next(1, 10000);
                InputId = Convert.ToString(random_number);
                await _imageStore.GetImage(int.Parse(InputId));
                ShowImage = Bitmap != null ? true : false;
                ShowPicNotAvail = !ShowImage;
            }
        }

        private void OnCurrentImageChanged()
        {
            OnPropertyChanged(nameof(ID));
            OnPropertyChanged(nameof(ImageURI));
            OnPropertyChanged(nameof(Bitmap));
        }

        public override void Dispose()
        {
            _imageStore.CurrentImageChanged -= OnCurrentImageChanged;
            base.Dispose();
        }

    }
}
