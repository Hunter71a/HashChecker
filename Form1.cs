using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;



namespace HashChecker
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // initialize global class variables to hold textbox values and control variables
        private string hashCodeGood = "";
        private string output = "After selecting your file and entering the hash key, relax while the Hash-O-Matic XL3000 does all the work";
        private string testFileLocation = "";
        private string sha256computed = "";
        private string directory = "";
        private string fileName = "";
        private string generatedKey = "";
        private Byte[] hashValue;
      //  private FileStream file;
        private FileInfo src = null;


        /* test file 
         
         Name: a.7z
        Size: 1201092 bytes (1172 KiB)
        SHA256: 55BD7777EDD36CB10CA2F735DC98DAD2AA70CCDDEE072D4226BE45F108D0726A
         *
         */



        // The cryptographic service providers.
        private SHA256 Sha256 = SHA256.Create();





        // Utilities

        // Return a byte array as a sequence of hex values.
        public static string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes) result += b.ToString("x2");
            return result;
        }

        // event handler for exitButton to exit program safely
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Display the byte array in a readable format.
        public static void PrintByteArray(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]:X2}");
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }



        /*
         * event handler for selectFile button
         *
         * open file picker window and display filepath in textbox
         */
        private void selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse for file to check",

                CheckFileExists = true,
                CheckPathExists = true,



                //  DefaultExt = "txt",
                //    Filter = "txt files (*.txt)|*.txt",
                //   FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                testFileLocation = openFileDialog1.FileName;
                fileName = openFileDialog1.SafeFileName;
                src = new FileInfo(testFileLocation);
                fileSelected.Text = src.Name;
                outputText.Text = $"Entered hash key is {hashCodeGood} \n" +
$"and the file selected is {testFileLocation}\n {src.Name}\n src length = {src.Length} \n after hexadecimal conversion to utf-8: \n {src.LastAccessTime}";
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    try
                    {
                        // Create a fileStream for the file.
                        FileStream fileStream = src.Open(FileMode.Open);
                        // Be sure it's positioned to the beginning of the stream.
                        fileStream.Position = 0;
                        // Compute the hash of the fileStream.
                        hashValue = mySHA256.ComputeHash(fileStream);
                        generatedKey = BytesToString(hashValue);
                        // Write the name and hash value of the file to the console.
                        outputText.Text = $"Entered hash key is {fileHashGood} \n" +
    $"and the file selected is {testFileLocation}\n {src.Name}\n raw hash = {hashValue} \n after hexadecimal conversion to utf-8: \n {BytesToString(hashValue)}";
                        Console.Write($"{src.Name}: ");
                        PrintByteArray(hashValue);
                        // Close the file. (close file stream with calculate button
                        fileStream.Close();
                    }
                    catch (IOException E)
                    {
                        Console.WriteLine($"I/O Exception: {E.Message}");
                    }
                    catch (UnauthorizedAccessException E)
                    {
                        Console.WriteLine($"Access Exception: {E.Message}");
                    }
                }
            }
        }



        /*  
         *  Check hash from fileHashGood and compare to powershell generated results
         *  
         *  use regex to help identify hash key and reject ones that are impossible with MessageBox
         *  
         */
        private void processFileButton_Click(object sender, EventArgs e)
        {
           // testFileLocation = openFileDialog1.FileName;
          //  fileName = openFileDialog1.SafeFileName;
            //src = new FileInfo(testFileLocation);
        //    fileSelected.Text = src.Name;

            if (fileHashGood.Text == "")
            {
                MessageBox.Show("You need to include the hash key to check your file", "Incorrect Information",
                                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
                fileHashGood.Focus();
            }



            // Initialize a SHA256 hash object.
            else if (src != null)
            {
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    try
                    {
                        // Create a fileStream for the file.
                        FileStream fileStream = src.Open(FileMode.Open);
                        // Be sure it's positioned to the beginning of the stream.
                        fileStream.Position = 0;
                        // Compute the hash of the fileStream.

                        hashValue = mySHA256.ComputeHash(fileStream);
                        generatedKey = BytesToString(hashValue);
                        // Write the name and hash value of the file to the console.
                        outputText.Text = $"Entered hash key is {fileHashGood} \n" +
    $"and the file selected is {testFileLocation}\n {src.Name}\n raw hash = {hashValue} \n after hexadecimal conversion to utf-8: \n {BytesToString(hashValue)}";
                        Console.Write($"{src.Name}: ");
                        PrintByteArray(hashValue);
                        // Close the file.
                        fileStream.Close();
                    }
                    catch (IOException E)
                    {
                        Console.WriteLine($"I/O Exception: {E.Message}");
                    }
                    catch (UnauthorizedAccessException E)
                    {
                        Console.WriteLine($"Access Exception: {E.Message}");
                    }
                }
            }
            else
            {
                outputText.Text = $"The source file could not be found\nFile Name = {src.Name}  ";
            }

        }





        // Open file stream to read data for hash check
        //   StreamReader fileStream = new StreamReader(testFileLocation);
        //      var fileContent = fileStream.ReadToEnd();


        // check sha256 to test .Cryptography approach
        //   byte hashedData = Sha256.ComputeHash(fileContent.ToString());  

        //   sha256computed = ComputeSha256Hash()



        private string ComputeSHA256()
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                try
                {
                    // Create a fileStream for the file.
                    FileStream fileStream = src.Open(FileMode.Open);
                    // Be sure it's positioned to the beginning of the stream.
                    fileStream.Position = 0;
                    // Compute the hash of the fileStream.

                    hashValue = mySHA256.ComputeHash(fileStream);
                    generatedKey = BytesToString(hashValue);
                    // Write the name and hash value of the file to the console.
                    outputText.Text = $"Entered hash key is {fileHashGood} \n" +
$"and the file selected is {testFileLocation}\n {src.Name}\n raw hash = {hashValue} \n after hexadecimal conversion to utf-8: \n {BytesToString(hashValue)}";
                    Console.Write($"{src.Name}: ");
                    PrintByteArray(hashValue);
                    // Close the file.
                    fileStream.Close();
                }
                catch (IOException E)
                {
                    Console.WriteLine($"I/O Exception: {E.Message}");
                }
                catch (UnauthorizedAccessException E)
                {
                    Console.WriteLine($"Access Exception: {E.Message}");
                }
            }              

               // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                builder.Append(hashValue[i].ToString("x2"));
            }
            return builder.ToString();
        }
        

        // method for computing sha256 hash
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            outputText.Text = output;
        }

        // keep global variable updated with text change
        private void fileHashGood_TextChanged(object sender, EventArgs e)
        {
            hashCodeGood = fileHashGood.Text;
        }
    }
}
