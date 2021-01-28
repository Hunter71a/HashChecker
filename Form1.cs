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

        // event handler for exitButton to exit program safely
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void processFileButton_Click(object sender, EventArgs e)
        {
            if (fileHashGood.Text == "")
            {
                MessageBox.Show("You need to include the hash key to check your file", "Incorrect Information",
                                   MessageBoxButtons.OK, MessageBoxIcon.Stop);
                fileHashGood.Focus();
            }
            outputText.Text = $"Entered hash key is {fileHashGood.Text} \n\nDude, you're totally screwed";
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            outputText.Text = output;
        }       
    }
}
