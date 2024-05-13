using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.Utils
{
    internal class ReadingWritingUtil
    {

        public static void WriteDataToFile(byte[] data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Save As";
            saveFileDialog.Filter = "Binary Files (*.bin)|*.bin|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                        {
                            binaryWriter.Write(data);
                        }
                    }

                    Console.WriteLine("Data has been written to the file: " + filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while writing data to the file: " + ex.Message);
                }
            }
        }

        public static byte[] ReadBinaryFile()
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set properties of OpenFileDialog
            openFileDialog.Title = "Open Binary File";
            openFileDialog.Filter = "All Files (*.*)|*.*|Binary Files (*.bin)|*.bin|Text Files (*.txt)|*.txt";

            // Show OpenFileDialog and check if user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;

                try
                {
                    // Read the contents of the file into a byte array
                    byte[] data;
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        data = new byte[fileStream.Length];
                        fileStream.Read(data, 0, (int)fileStream.Length);
                    }

                    // Return the byte array containing the file data
                    return data;
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during file read operation
                    Console.WriteLine("An error occurred while reading the binary file: " + ex.Message);
                }
            }

            // Return null if no file was selected or an error occurred
            return null;
        }
    }
}
