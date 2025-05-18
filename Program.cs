using System;
using System.Diagnostics;
using System.IO;

class DDSBatchConverter
{
    static string texconvPath = "";

    static void Main(string[] args)
    {
        Console.Title = "Simple DDS Texture Converter";
        Console.WriteLine("== DDS Texture Converter by 510deshawn ==");
        Console.WriteLine("Please enter the FULL path to texconv.exe:");
        texconvPath = Console.ReadLine().Trim('"');

        if (!File.Exists(texconvPath) || !texconvPath.EndsWith("texconv.exe", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("❌ Invalid texconv.exe path. Exiting...");
            return;
        }

        Console.WriteLine("Enter the path to the input folder:");
        string inputFolder = Console.ReadLine().Trim('"');

        Console.WriteLine("Enter the path to the output folder:");
        string outputFolder = Console.ReadLine().Trim('"');

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine("❌ Error: Input folder not found.");
            return;
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.AllDirectories);
        int count = 0;

        foreach (string path in imageFiles)
        {
            string name = Path.GetFileNameWithoutExtension(path).ToLower();
            string format = null;
            bool isSRGB = false;

            if (name.EndsWith("_color"))
            {
                format = "BC7_UNORM_SRGB";
                isSRGB = true;
            }
            else if (name.EndsWith("_metal") || name.EndsWith("_rough") ||
                     name.EndsWith("_ao") || name.EndsWith("_opacity"))
            {
                format = "BC4_UNORM";
            }
            else if (name.EndsWith("_normal"))
            {
                format = "BC5_SNORM";
            }
            else
            {
                Console.WriteLine($"⚠ Skipped: {Path.GetFileName(path)} (no matching suffix)");
                continue;
            }

            ConvertToDDS(path, outputFolder, format, isSRGB);
            count++;
        }

        Console.WriteLine($"\n✅ Done. Converted {count} file(s). Press any key to exit.");
        Console.ReadKey();
    }

    static void ConvertToDDS(string inputImage, string outputFolder, string format, bool isSRGB)
    {
        string srgbFlag = isSRGB ? "-srgb" : "";
        string args = $"-f {format} {srgbFlag} -y -o \"{outputFolder}\" \"{inputImage}\"";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = texconvPath,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        try
        {
            using (Process proc = Process.Start(psi))
            {
                proc.WaitForExit();
                Console.WriteLine($"✔ {Path.GetFileName(inputImage)} → {format}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✖ Failed: {Path.GetFileName(inputImage)} - {ex.Message}");
        }
    }
}
