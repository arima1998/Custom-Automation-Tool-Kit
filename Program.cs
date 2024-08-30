using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    internal class DublicateFIleRemover
    {
        static void Main(string[] args)
        {
            string directoryPath = @"E:\GRE-RES"; 

            var fileChecksums = new Dictionary<string, List<string>>();

            
            string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                try
                {
                    string checksum = ComputeFileChecksum(file);

                    if (!fileChecksums.ContainsKey(checksum))
                    {
                        fileChecksums[checksum] = new List<string>();
                    }
                    fileChecksums[checksum].Add(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {file}: {ex.Message}");
                }
            }

            foreach (var entry in fileChecksums)
            {
                if (entry.Value.Count > 1)
                {
                    Console.WriteLine("Duplicate files found:");
                    foreach (var path in entry.Value)
                    {
                        Console.WriteLine(path);
                    }

                    for (int i = 1; i < entry.Value.Count; i++)
                    {
                        File.Delete(entry.Value[i]);
                        Console.WriteLine($"Deleted: {entry.Value[i]}");
                    }
                }
            }

            Console.WriteLine("Duplicate search completed.");
        }

        static string ComputeFileChecksum(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }

}
