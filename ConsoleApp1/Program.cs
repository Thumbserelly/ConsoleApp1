using ImageMagick;
using ImageMagick.Formats;
using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MagickImage[] images = new MagickImage[4];
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = new MagickImage(AppDomain.CurrentDomain.BaseDirectory + $"IMG_000{i + 1}.JPG");
                var text = new DrawableText(300, 200, (i + 1).ToString());
                var pointSize = new DrawableFontPointSize(60);
                MagickGeometry geom = new MagickGeometry();
                geom.Height = 600;
                geom.Width = 400;
                geom.FillArea = true;
                images[i].AnimationDelay = 250;
                images[i].Resize(geom);
                images[i].Crop(geom, Gravity.Center);
                images[i].Draw(pointSize, text);
                images[i].RePage();
            }

            var defines = new VideoWriteDefines(MagickFormat.Mp4)
            {
                PixelFormat = "yuv420p",
            };

            var imageCollection = new MagickImageCollection(images);
            imageCollection.Write("output.mp4", MagickFormat.Mp4);

            var data = imageCollection.ToByteArray(defines);

            //string result = String.Empty;
            //using (Process p = new Process())
            //{
            //    p.StartInfo.UseShellExecute = false;
            //    p.StartInfo.CreateNoWindow = true;
            //    p.StartInfo.RedirectStandardOutput = true;
            //    p.StartInfo.FileName = "./ffmpeg.exe";
            //    p.StartInfo.Arguments = "-y -i ./output.mp4 -s 400x600 -vf format=yuv420p  ./output_fixed.mp4";
            //    p.Start();
            //    p.WaitForExit();
            //    result = p.StandardOutput.ReadToEnd();
            //}
        }
    }
}
