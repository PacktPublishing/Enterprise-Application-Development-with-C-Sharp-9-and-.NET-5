using System;
using System.Threading;

namespace LazyInitialization
{
    class Program
    {
        // Uncomment each sample seperately
        static void Main(string[] args)
        {
            //// Sample 1
            //Lazy<ImageFile> imageFile = new Lazy<ImageFile>(() => new ImageFile("test"));
            //var image = imageFile.Value.LoadImage;

            //// Sample 2
            //Func<object> imageFile = new Func<object>(() => { var obj = new ImageFile("test"); return obj.LoadImageFromDisk(); });
            //Lazy<object> lazyImage = new Lazy<object>(imageFile);
            //var image = lazyImage.Value;
            //Console.WriteLine(image);
            //Func<object> imageFile1 = new Func<object>(() => { var obj = new ImageFile("test1"); return obj.LoadImageFromDisk(); });
            //Lazy<object> lazyImage1 = new Lazy<object>(imageFile1);
            //var image1 = lazyImage1.Value;

            //// Sample 3
            //object image = null;
            //LazyInitializer.EnsureInitialized(ref image, () =>
            //{
            //    var obj = new ImageFile("test");
            //    return obj.LoadImageFromDisk();
            //});

            Console.ReadLine();
        }
    }
}
