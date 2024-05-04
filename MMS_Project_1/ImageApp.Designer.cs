namespace MMS_Project_1
{
    partial class ImageApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageApp));
            pictureBox1 = new PictureBox();
            toolStrip1 = new ToolStrip();
            loadPictureButton = new ToolStripButton();
            dropDownButtonOptions = new ToolStripDropDownButton();
            btnCompressAndSave = new ToolStripMenuItem();
            lblImgNameText = new Label();
            lblImageName = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 81);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1302, 638);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { loadPictureButton, dropDownButtonOptions });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1338, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // loadPictureButton
            // 
            loadPictureButton.Image = (Image)resources.GetObject("loadPictureButton.Image");
            loadPictureButton.ImageTransparentColor = Color.Magenta;
            loadPictureButton.Name = "loadPictureButton";
            loadPictureButton.Size = new Size(93, 22);
            loadPictureButton.Text = "Load picture";
            loadPictureButton.Click += loadPictureButton_Click;
            // 
            // dropDownButtonOptions
            // 
            dropDownButtonOptions.DisplayStyle = ToolStripItemDisplayStyle.Text;
            dropDownButtonOptions.DropDownItems.AddRange(new ToolStripItem[] { btnCompressAndSave });
            dropDownButtonOptions.Image = (Image)resources.GetObject("dropDownButtonOptions.Image");
            dropDownButtonOptions.ImageTransparentColor = Color.Magenta;
            dropDownButtonOptions.Name = "dropDownButtonOptions";
            dropDownButtonOptions.Size = new Size(62, 22);
            dropDownButtonOptions.Text = "Options";
            dropDownButtonOptions.Click += dropDownButtonFilters_Click;
            // 
            // btnCompressAndSave
            // 
            btnCompressAndSave.Name = "btnCompressAndSave";
            btnCompressAndSave.Size = new Size(180, 22);
            btnCompressAndSave.Text = "Compress and save";
            btnCompressAndSave.Click += btnCompressAndSave_Click;
            // 
            // lblImgNameText
            // 
            lblImgNameText.AutoSize = true;
            lblImgNameText.Location = new Point(12, 37);
            lblImgNameText.Name = "lblImgNameText";
            lblImgNameText.Size = new Size(76, 15);
            lblImgNameText.TabIndex = 2;
            lblImgNameText.Text = "Image name:";
            // 
            // lblImageName
            // 
            lblImageName.AutoSize = true;
            lblImageName.Location = new Point(103, 37);
            lblImageName.Name = "lblImageName";
            lblImageName.Size = new Size(36, 15);
            lblImageName.TabIndex = 3;
            lblImageName.Text = "None";
            // 
            // ImageApp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1338, 731);
            Controls.Add(lblImageName);
            Controls.Add(lblImgNameText);
            Controls.Add(toolStrip1);
            Controls.Add(pictureBox1);
            Name = "ImageApp";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private ToolStrip toolStrip1;
        private ToolStripButton loadPictureButton;
        private ToolStripDropDownButton dropDownButtonOptions;
        private ToolStripMenuItem btnCompressAndSave;
        private Label lblImgNameText;
        private Label lblImageName;
    }
}
