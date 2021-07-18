using ImageGrabber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageGrabber.Services
{
    class ImageService
    {
        public async Task<Image> GetImage(int id)
        {

            Image image = new Image();
            using (var client = new HttpClient())
            {
                var uri = $"https://picsum.photos/id/{id}/400/300";

                client.BaseAddress = new Uri(uri);

                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "");
                try
                {
                    Task<HttpResponseMessage> getResponse = client.SendAsync(request);
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await getResponse;

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] mybytearray = response.Content.ReadAsByteArrayAsync().Result;
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new System.IO.MemoryStream(mybytearray);
                        bitmapImage.EndInit();

                        image.ID = id;
                        image.ImageURI = uri;
                        image.Bitmap = bitmapImage;
                    }

                    return image;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex, "Exception Happened");
                    return image;
                }
            }
        }
    }
}
