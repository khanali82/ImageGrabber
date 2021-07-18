using ImageGrabber.Models;
using ImageGrabber.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageGrabber.Stores
{
    class ImageStore
    {

        private Image _currentImage;
        private ImageService _imageService;

        public ImageStore(ImageService imageService)
        {
            _imageService = imageService;
            _currentImage = new Image();
            
        }

        public Image CurrentImage
        {
            get => _currentImage;
            set
            {
                _currentImage = value;
               // CurrentImageChanged?.Invoke();
            }
        }
        
        public event Action CurrentImageChanged;


        public async Task GetImage(int id)
        {
            _currentImage = await _imageService.GetImage(id);
            CurrentImageChanged?.Invoke();
        }
    }
}
