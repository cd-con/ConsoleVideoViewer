using OpenCvSharp;

internal class Program
{
    // Extra charsets
    //const string charSet = " .'`^\",:;Il!i><~+_-?][}{1)(|";
    //const string charSet = "  .·*оО#";
    const string charSet = "  ░▒▓█"; // Remove one space for more smooth gradient
    private static void Main(string[] args)
    {
        string path = string.Empty;
        int framerate = 30;

        if (!OperatingSystem.IsWindows())
        {
            Console.WriteLine("Unsupported OS");
            return;
        }

        if (args.Length != 2)
        {
            Console.WriteLine("Invalid argument count. Please input it manually.");
            Console.Write("Path > ");
            path = Console.ReadLine();
            Console.Write("Framerate > ");
            if (path == null || !int.TryParse(Console.ReadLine(), out framerate)){
                Console.WriteLine("Invalid args.");
                return;
            }
            Console.Clear();
        }
        else
        {
            path = args[0];
            framerate = int.Parse(args[1]);
        }

        VideoCapture capture = new(path);
        Console.SetBufferSize(Console.BufferWidth, 80);

        while (capture.IsOpened())
        {
            try
            {
                Mat image = new();
                string frame = string.Empty;
                capture.Read(image);

                image = image.Resize(new Size(Console.BufferWidth, Console.BufferHeight - 17));
                for (int x = 0; x < image.Height; x++)
                {
                    for (int y = 0; y < image.Width - 1; y++)
                    {
                        Vec3b pixel = image.At<Vec3b>(x, y);
                        double luminocity = (pixel.Item0 + pixel.Item1 + pixel.Item2) / 3d;
                        int round = (int)Math.Round(luminocity / 256 * (charSet.Length - 1));
                        frame += charSet[round];
                    }
                    frame += "\n";
                }
                Console.Write(frame);
            }
            catch (Exception _)
            {
                return;
            }
            Thread.Sleep(1000 / framerate);
        }
    }
}