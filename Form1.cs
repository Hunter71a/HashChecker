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

        // initialize global class variables to hold textbox values, control variables, and access points
        private bool hashCodeMatches = false;
        private bool testingComplete = false;
        private bool generateKeyMode = false;
        private string hashCodeGood = "";
        private string output = "After selecting your file and entering the hash key, relax while the Hash-O-Matic XL3000 does all the work";
        private string completeReport = "";
        private string hashesOnly= "";
        private string matchedHashReport = $"";
        private string testFileLocation = "";
        private string fileName = "";
        private string generatedKey = "";
        private Byte[] hashValue;
        private FileInfo src = null;
        private TimeSpan totalTime;

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
                calculateRawHashValues();                
            }
        }


        private void calculateRawHashValues()
        {
            ComputeSHA256();
            hashesOnly = $"\t<SHA256>\n{generatedKey}\n\n";
            ComputeSHA384();
            hashesOnly += $"\t<SHA384>\n{generatedKey}\n\n";
            ComputeSHA512();
            hashesOnly += $"\t<SHA512>\n{generatedKey}\n\n";
            ComputeMD5();
            hashesOnly += $"\t<MD5>\n{generatedKey}\n\n";
            ComputeSHA1();
            hashesOnly += $"\t<SHA1>\n{generatedKey}\n\n";
            outputText.Text = $"Assorted Hash values for selected file: {fileName}\n\n";
            outputText.Text += $"{hashesOnly}";
        }


        /*  
         *  Check hash from fileHashGood and compare to powershell generated results
         *  
         *  use regex to help identify hash key and reject ones that are impossible with MessageBox
         *  
         */
        private void processFileButton_Click(object sender, EventArgs e)
        {
            testingComplete = false;

            if (fileHashGood.Text == "")
            {
                MessageBox.Show("You need to include the hash key to check your file", "Incorrect Information",
                                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
                fileHashGood.Focus();
            }

            if (src != null)
            {
                while (hashCodeMatches == false && testingComplete == false)
                {
                    ComputeSHA256();
                    ComputeSHA384();
                    ComputeSHA512();
                    ComputeMD5();
                    ComputeSHA1();

                    testingComplete = true;
                }
                
                outputText.Text = $"Total time to complete all tests (H:M:S): {totalTime}\n";
                if (!hashCodeMatches) 
                {
                    outputText.Text += "\n*******    NO MATCH WAS FOUND!    *******";
                        } 
                outputText.Text += matchedHashReport + completeReport;
            }


            /* maintain old code block for reference

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
                        if (generatedKey == hashCodeGood)
                        {
                            outputText.Text = $"**************** MATCH DETECTED ********************\n\nIt's a match! \nHash Key Entered: / Hashed Value\n\n{hashCodeGood}\n{generatedKey}"; 
                        }
                        else
                        {
                            outputText.Text = $"*****  ALERT! HASH KEY MISMATCH!  *****\n\n\nHash Key Entered: / Hashed Value\n\n{hashCodeGood}\n{generatedKey} ";
                        }
                        // Write the name and hash value of the file to the console.
                      //  outputText.Text = $"Entered hash key is {fileHashGood} \n" +
  //  $"and the file selected is {testFileLocation}\n {src.Name}\n raw hash = {hashValue} \n after hexadecimal conversion to utf-8: \n {BytesToString(hashValue)}";
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
            */

            //else
            //{
            //    outputText.Text = $"The source file could not be found\nFile Name = {src.Name}  ";
            //}

        }


        // Void method for computing SHA256 hash key 
        private void ComputeSHA256()
        {
            DateTime start = DateTime.Now;
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
                    DateTime end = DateTime.Now;
                    TimeSpan diff = end - start;
                    totalTime += diff;
                    if (generatedKey == hashCodeGood)
                    {
                        matchedHashReport += $"\n*******  SUCCESS: SHA256 MATCH FOUND!  *******";
                        matchedHashReport += $" \nHash Key Entered: / Hashed Value\n\n{hashCodeGood}\n{generatedKey}";
                        hashCodeMatches = true;
                        matchedHashReport += $"\n\nCurrently, the best public attacks break preimage resistance for 52 out of 64 rounds of SHA-256 or 57 out of 80 rounds of SHA-512,\n and collision resistance for 46 out of 64 rounds of SHA-256";
                        matchedHashReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }
                    else
                    {
                        completeReport += $"\n\n*****  SHA256 HASH KEY MISMATCH!  *****\n\nSHA256 hashing failed to match key\nHash Key Entered vs Hashed Value\n{hashCodeGood}\n{generatedKey} ";
                        completeReport += $"\nTime to complete hash in H:M:S:   {diff}"; 
                    }

                    // Write the name and hash value of the file to the console.
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

        // Void method for computing SHA384 
        private void ComputeSHA384()
        {
            DateTime start = DateTime.Now;
            using (SHA384 mySHA384 = SHA384.Create())
            {
                try
                {
                    // Create a fileStream for the file.
                    FileStream fileStream = src.Open(FileMode.Open);
                    // Be sure it's positioned to the beginning of the stream.
                    fileStream.Position = 0;
                    // Compute the hash of the fileStream.

                    hashValue = mySHA384.ComputeHash(fileStream);
                    generatedKey = BytesToString(hashValue);
                    DateTime end = DateTime.Now;
                    TimeSpan diff = end - start;
                    totalTime += diff;
                    if (generatedKey == hashCodeGood)
                    {
                        matchedHashReport += $"\n*******  SUCCESS: SHA384 MATCH FOUND!  *******";
                        matchedHashReport += $" \nHash Key Entered: / Hashed Value\n\n{hashCodeGood}\n{generatedKey}";
                        hashCodeMatches = true;
                        matchedHashReport += $"\n\nCurrently, the best public attacks break preimage resistance for 52 out of 64 rounds of SHA-256 or 57 out of 80 rounds of SHA-512,\n and collision resistance for 46 out of 64 rounds of SHA-256";
                        matchedHashReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }
                    else
                    {
                        completeReport += $"\n\n*****  SHA364 HASH KEY MISMATCH!  *****\n\nSHA384 hashing failed to match key\nHash Key Entered vs Hashed Value\n{hashCodeGood}\n{generatedKey} ";
                        completeReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }

                    // Write the name and hash value of the file to the console.
                    //  outputText.Text = $"Entered hash key is {fileHashGood} \n" +
                    //  $"and the file selected is {testFileLocation}\n {src.Name}\n raw hash = {hashValue} \n after hexadecimal conversion to utf-8: \n {BytesToString(hashValue)}";
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

        // Void method for computing SHA384 
        private void ComputeSHA512()
        {
            DateTime start = DateTime.Now;
            using (SHA512 mySHA512 = SHA512.Create())
            {
                try
                {
                    // Create a fileStream for the file.
                    FileStream fileStream = src.Open(FileMode.Open);
                    // Be sure it's positioned to the beginning of the stream.
                    fileStream.Position = 0;
                    // Compute the hash of the fileStream.

                    hashValue = mySHA512.ComputeHash(fileStream);
                    generatedKey = BytesToString(hashValue);
                    DateTime end = DateTime.Now;
                    TimeSpan diff = end - start;
                    totalTime += diff;
                    if (generatedKey == hashCodeGood)
                    {
                        matchedHashReport += $"\n*******  SUCCESS: SHA512 MATCH FOUND!  *******";
                        matchedHashReport += $" \nHash Key Entered: / Hashed Value\n\n{hashCodeGood}\n{generatedKey}";
                        hashCodeMatches = true;
                        matchedHashReport += $"\n\nCurrently, the best public attacks break preimage resistance for 52 out of 64 rounds of SHA-256 or 57 out of 80 rounds of SHA-512,\n and collision resistance for 46 out of 64 rounds of SHA-256";
                        matchedHashReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }
                    else
                    {
                        completeReport += $"\n\n*****  SHA512 HASH KEY MISMATCH!  *****\n\nSHA512 hashing failed to match key\nHash Key Entered vs Hashed Value\n{hashCodeGood}\n{generatedKey} ";
                        completeReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }

                    // Write the name and hash value of the file to the console.
                    //  outputText.Text = $"Entered hash key is {fileHashGood} \n" +
                    //  $"and the file selected is {testFileLocation}\n {src.Name}\n raw hash = {hashValue} \n after hexadecimal conversion to utf-8: \n {BytesToString(hashValue)}";
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


        // Void method for computing MD5 hash key 
        private void ComputeMD5()
        {
            DateTime start = DateTime.Now;
            using (MD5 myMD5 = MD5.Create())
            {
                try
                {
                    // Create a fileStream for the file.
                    FileStream fileStream = src.Open(FileMode.Open);
                    // Be sure it's positioned to the beginning of the stream.
                    fileStream.Position = 0;
                    // Compute the hash of the fileStream.

                    hashValue = myMD5.ComputeHash(fileStream);
                    generatedKey = BytesToString(hashValue);
                    DateTime end = DateTime.Now;
                    TimeSpan diff = end - start;
                    totalTime += diff;
                    if (generatedKey == hashCodeGood)
                    {
                        matchedHashReport += $"\n*******  SUCCESS: MD5 MATCH FOUND!  *******";
                        matchedHashReport += $" \nHash Key Entered: / Hashed Value\n\n{hashCodeGood}\n{generatedKey}";
                        hashCodeMatches = true;
                        matchedHashReport += $"\nWARNING: MD5 is deprecated and not considered secure";
                        matchedHashReport += $"\n\nCurrently, the best public attacks break preimage resistance for 52 out of 64 rounds of SHA-256 or 57 out of 80 rounds of SHA-512,\n and collision resistance for 46 out of 64 rounds of SHA-256";
                        matchedHashReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }
                    else
                    {
                        completeReport += $"\n\n*****  MD5 HASH KEY MISMATCH!  *****\n\nMD5 hashing failed to match key\nHash Key Entered vs Hashed Value\n{hashCodeGood}\n{generatedKey} ";
                        completeReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }

                    // Write the name and hash value of the file to the console.
                    //  outputText.Text = $"Entered hash key is {fileHashGood} \n" +
                    //  $"and the file selected is {testFileLocation}\n {src.Name}\n raw hash = {hashValue} \n after hexadecimal conversion to utf-8: \n {BytesToString(hashValue)}";
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

        // Void method for computing SHA1 hash key 
        private void ComputeSHA1()
        {
            DateTime start = DateTime.Now;
            using (SHA1 mySHA1 = SHA1.Create())
            {
                try
                {
                    // Create a fileStream for the file.
                    FileStream fileStream = src.Open(FileMode.Open);
                    // Be sure it's positioned to the beginning of the stream.
                    fileStream.Position = 0;
                    // Compute the hash of the fileStream.

                    hashValue = mySHA1.ComputeHash(fileStream);
                    generatedKey = BytesToString(hashValue);
                    DateTime end = DateTime.Now;
                    TimeSpan diff = end - start;
                    totalTime += diff;
                    if (generatedKey == hashCodeGood)
                    {
                        matchedHashReport += $"\n*******  SUCCESS: SHA1 MATCH FOUND!  *******";
                        matchedHashReport += $" \nHash Key Entered: / Hashed Value\n\n{hashCodeGood}\n{generatedKey}";
                        hashCodeMatches = true;
                        matchedHashReport += $"\nWARNING: SHA1 is deprecated and considered not secure";
                        matchedHashReport += $"\n\nCurrently, the best public attacks break preimage resistance for 52 out of 64 rounds of SHA-256 or 57 out of 80 rounds of SHA-512,\n and collision resistance for 46 out of 64 rounds of SHA-256";
                        matchedHashReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }
                    else
                    {
                        completeReport += $"\n\n*****  SHA1 HASH KEY MISMATCH!  *****\n\nSHA1 hashing failed to match key\nHash Key Entered vs Hashed Value\n{hashCodeGood}\n{generatedKey} ";
                        completeReport += $"\nTime to complete hash in H:M:S:   {diff}";
                    }

                    // Write the name and hash value of the file to the console.
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

        // method for computing sha256 hash
        private string ComputeSha256HashWithPassedParameters(string rawData)
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
