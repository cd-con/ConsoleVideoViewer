using OpenCvSharp;

internal class Program
{

    private static void Main(string[] args)
    {
        // а
        String a = " .'`^\",:;Il!i><~+_-?][}{1)(|";
        string path = "C:\\Users\\ИСП-31\\Source\\Repos\\ConsoleVideoViewer\\Meet the Heavy.mp4";

        VideoCapture capture = new(path);
        Mat image = new Mat();

        Console.SetBufferSize(Console.BufferWidth, 80);

        int fCounter = 0;
        while (capture.IsOpened())
        {
            String frame = "";
            capture.Read(image);

            image = image.Resize(new Size(Console.BufferWidth, Console.BufferHeight - 18));
            //Cv2.ImWrite($"C:\\Users\\ИСП-31\\Source\\Repos\\ConsoleVideoViewer\\bin\\Debug\\net6.0\\frames\\frame{fCounter}.png", image);
            fCounter++;
            for (int x = 0; x < image.Height; x++)
            {
                for (int y = 0; y < image.Width - 1; y++)
                {
                    Vec3b pixel = image.At<Vec3b>(x,y);
                    double luminocity = (pixel.Item0 + pixel.Item1 + pixel.Item2) / 3d;
                    int round = (int)Math.Round(luminocity / 256 * (a.Count() - 1));
                    frame += a[round];
                }
                frame+= "\n";
            }
            Console.Write(frame);
        }
    }
}