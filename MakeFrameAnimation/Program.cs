using System.Drawing;
using System.Drawing.Imaging;

namespace MakeFrameAnimation
{
    internal class Program
    {
        public enum Direction
        {
            Left = 1,
            Right = 2,
            Up = 3,
            Down = 4,
            Center = 5
        }
        public static string CurrentDir
        {
            get => System.IO.Directory.GetCurrentDirectory();
        }
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                throw new Exception("Please provide the path to the folder containing the images.");
            }

            if (!Directory.Exists(args[0]))
            {
                throw new IOException("The folder does not exist.");
            }
            Direction direction = Direction.Center;
            if (args.Length > 1)
            {
                if (byte.TryParse(args[1], out var result))
                {
                    if (result > 0 && result <= (byte)Direction.Center)
                    {
                        direction = (Direction)result;
                    }
                }
            }
            var files = Directory.GetFiles(args[0]);

            List<Image> images = new List<Image>();

            foreach (var item in files)
            {
                try
                {
                    images.Add(Image.FromFile(item));
                }
                catch (Exception)
                {
                }
            }

            int maxWidth = images.Max(x => x.Width);
            int maxHeight = images.Max(x => x.Height);
            Bitmap bitmap = new Bitmap(maxWidth * images.Count, maxHeight);
            Graphics graphics = Graphics.FromImage(bitmap);
            for (int i = 0; i < images.Count; i++)
            {
                Image image = images[i];
                switch (direction)
                {
                    case Direction.Left:
                        graphics.DrawImage(image, i * maxWidth, (maxHeight - image.Height) / 2);
                        break;
                    case Direction.Right:
                        graphics.DrawImage(image, i * maxWidth + (maxWidth - image.Width), (maxHeight - image.Height) / 2);
                        break;
                    case Direction.Up:
                        graphics.DrawImage(image, i * maxWidth + ((maxWidth - image.Width) / 2), 0);
                        break;
                    case Direction.Down:
                        graphics.DrawImage(image, i * maxWidth + ((maxWidth - image.Width) / 2), maxHeight - image.Height);
                        break;
                    case Direction.Center:
                        graphics.DrawImage(image, i * maxWidth + ((maxWidth - image.Width) / 2), (maxHeight - image.Height) / 2);
                        break;
                    default:
                        break;
                }
            }
            bitmap.Save(CurrentDir + "/output.png", ImageFormat.Png);
        }
    }
}
