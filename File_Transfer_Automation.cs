﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class GRE
    {
        static void Main(string[] args)
        {
            string sourceDirectory = @"C:\Users\USER\Downloads";

            string destinationDirectory = @"E:\GRE-RES";


            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            try
            {
                string[] pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf");

                foreach (string file in pdfFiles)
                {
                    
                    string fileName = Path.GetFileName(file);

                    
                    if (fileName.IndexOf("GRE", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        
                        string destinationPath = Path.Combine(destinationDirectory, fileName);

                        
                        File.Move(file, destinationPath);

                        Console.WriteLine($"Moved: {fileName}");
                    }
                }

                Console.WriteLine("File transfer completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
    
}
