
namespace HashChecker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.processFileButton = new System.Windows.Forms.Button();
            this.fileHashGoodLabel = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.deluxe = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.outputLabel = new System.Windows.Forms.Label();
            this.outputText = new System.Windows.Forms.RichTextBox();
            this.selectFile = new System.Windows.Forms.Button();
            this.fileSelected = new System.Windows.Forms.RichTextBox();
            this.fileHashGood = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // processFileButton
            // 
            this.processFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processFileButton.Location = new System.Drawing.Point(182, 762);
            this.processFileButton.Name = "processFileButton";
            this.processFileButton.Size = new System.Drawing.Size(127, 47);
            this.processFileButton.TabIndex = 0;
            this.processFileButton.Text = "Check File";
            this.processFileButton.UseVisualStyleBackColor = true;
            this.processFileButton.Click += new System.EventHandler(this.processFileButton_Click);
            // 
            // fileHashGoodLabel
            // 
            this.fileHashGoodLabel.AutoSize = true;
            this.fileHashGoodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileHashGoodLabel.Location = new System.Drawing.Point(316, 280);
            this.fileHashGoodLabel.Name = "fileHashGoodLabel";
            this.fileHashGoodLabel.Size = new System.Drawing.Size(170, 26);
            this.fileHashGoodLabel.TabIndex = 2;
            this.fileHashGoodLabel.Text = "Enter hash code";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Unispace", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(227, 28);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(397, 39);
            this.title.TabIndex = 3;
            this.title.Text = "Hash-O-Matic XL3000";
            // 
            // deluxe
            // 
            this.deluxe.AutoSize = true;
            this.deluxe.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deluxe.Location = new System.Drawing.Point(630, 50);
            this.deluxe.Name = "deluxe";
            this.deluxe.Size = new System.Drawing.Size(47, 17);
            this.deluxe.TabIndex = 4;
            this.deluxe.Text = "deluxe";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(512, 762);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 47);
            this.button1.TabIndex = 6;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputLabel.Location = new System.Drawing.Point(316, 407);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(177, 25);
            this.outputLabel.TabIndex = 7;
            this.outputLabel.Text = "Output Telemetry";
            // 
            // outputText
            // 
            this.outputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputText.Location = new System.Drawing.Point(78, 435);
            this.outputText.Name = "outputText";
            this.outputText.Size = new System.Drawing.Size(678, 305);
            this.outputText.TabIndex = 8;
            this.outputText.Text = "";
            // 
            // selectFile
            // 
            this.selectFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFile.Location = new System.Drawing.Point(321, 128);
            this.selectFile.Name = "selectFile";
            this.selectFile.Size = new System.Drawing.Size(150, 36);
            this.selectFile.TabIndex = 9;
            this.selectFile.Text = "Select File";
            this.selectFile.UseVisualStyleBackColor = true;
            this.selectFile.Click += new System.EventHandler(this.selectFile_Click);
            // 
            // fileSelected
            // 
            this.fileSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.fileSelected.Location = new System.Drawing.Point(78, 183);
            this.fileSelected.Name = "fileSelected";
            this.fileSelected.Size = new System.Drawing.Size(678, 56);
            this.fileSelected.TabIndex = 11;
            this.fileSelected.Text = "";
            // 
            // fileHashGood
            // 
            this.fileHashGood.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileHashGood.Location = new System.Drawing.Point(78, 318);
            this.fileHashGood.Name = "fileHashGood";
            this.fileHashGood.Size = new System.Drawing.Size(678, 59);
            this.fileHashGood.TabIndex = 12;
            this.fileHashGood.Text = "";
            this.fileHashGood.TextChanged += new System.EventHandler(this.fileHashGood_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 837);
            this.Controls.Add(this.fileHashGood);
            this.Controls.Add(this.fileSelected);
            this.Controls.Add(this.selectFile);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deluxe);
            this.Controls.Add(this.title);
            this.Controls.Add(this.fileHashGoodLabel);
            this.Controls.Add(this.processFileButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Hash-O-Matic XL3000";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button processFileButton;
        private System.Windows.Forms.Label fileHashGoodLabel;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label deluxe;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.RichTextBox outputText;
        private System.Windows.Forms.Button selectFile;
        private System.Windows.Forms.RichTextBox fileSelected;
        private System.Windows.Forms.RichTextBox fileHashGood;
    }
}

