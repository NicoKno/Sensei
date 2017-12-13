using System;
using System.IO;
using Sensei.Droid;
using Android.App;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(VideoSourceProvider))]
namespace Sensei.Droid
{
    public class VideoSourceProvider : IVideoSourceProvider
    {
        public string GetVideoSource(string videoName)
        {
            var assets = Forms.Context.Assets;

            var stream = assets.Open(videoName);

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, videoName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }

            return filePath;
        }
    }
}