using System;
using Synthizer;
namespace Example
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter path to file");
            string file = Console.ReadLine();
            synthizer.initialize(LogLevel.debug, LoggingBackend.STDERR);
            Context ctx = new();
            ctx.DefaultPannerStrategy.Value = (int)PannerStrategy.Hrtf;
            Synthizer.Buffer buf = new(); // Calling the namespace so the compiler won't get confused between System.Buffer and Synthizer.Buffer
            buf.FromFile(file);
            BufferGenerator gen = new(ctx);
            gen.Buffer.Value = buf;
            gen.Looping.Value = true;
            Source3D src = new(ctx);
            src.AddGenerator(gen);
            while (true)
            {
                Console.WriteLine("Enter command...");
                string arg = Console.ReadLine();
                string[] cmd = arg.Split(" ", 4);
                if (cmd[0] == "seek" && cmd.Length == 2)
                {
                    double pos = 0.0;
                    try
                    {
                        pos = double.Parse(cmd[1]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e}");
                        continue;
                    }
                    gen.PlaybackPosition.Value = pos;
                }
                else if (cmd[0] == "gain" && cmd.Length == 2)
                {
                    double vol = 0.0;
                    try
                    {
                        vol = double.Parse(cmd[1]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e}");
                        continue;
                    }
                    gen.Gain.Value = vol;
                }
                else if (cmd[0] == "pos" && cmd.Length == 4)
                {
                    double x, y, z = 0.0;
                    try
                    {
                        x = Double.Parse(cmd[1]);
                        y = double.Parse(cmd[2]);
                        z = double.Parse(cmd[3]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e}");
                        continue;
                    }
                    src.Position.Value = (x, y, z);
                }
                else if (cmd[0] == "quit")
                    break;
            }
            ctx.Destroy();
            gen.Destroy();
            src.Destroy();
            buf.Destroy();
            synthizer.shutdown();
        }
    }
}
