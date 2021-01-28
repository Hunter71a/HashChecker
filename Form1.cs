using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /*
         * event handler for selectFile button
         *
         * open file picker window and display filepath in textbox
         */
        private void selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileSelected.Text = openFileDialog1.FileName;
                testFileLocation = fileSelected.Text;
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
            if (fileHashGood.Text == "")
            {
                MessageBox.Show("You need to include the hash key to check your file", "Incorrect Information",
                                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
                fileHashGood.Focus();
            }
            outputText.Text = $"Entered hash key is {fileHashGood.Text} \n" +
                $"and the file selected is {testFileLocation}\n" +
                $"Dude, you're totally screwed";
        }




        // event handler for exitButton to exit program safely
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            outputText.Text = output;
        }

       
    }
}
