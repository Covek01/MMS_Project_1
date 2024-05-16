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
            loadAndDecompressButton = new ToolStripMenuItem();
            filtersOpenButton = new ToolStripDropDownButton();
            sIerraDitheringToolStripMenuItem = new ToolStripMenuItem();
            meanRemovalToolStripMenuItem = new ToolStripMenuItem();
            edgeEnhanceToolStripMenuItem = new ToolStripMenuItem();
            lblImgNameText = new Label();
            lblImageName = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, 4);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { loadPictureButton, dropDownButtonOptions, filtersOpenButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1529, 27);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // loadPictureButton
            // 
            loadPictureButton.Image = (Image)resources.GetObject("loadPictureButton.Image");
            loadPictureButton.ImageTransparentColor = Color.Magenta;
            loadPictureButton.Name = "loadPictureButton";
            loadPictureButton.Size = new Size(116, 24);
            loadPictureButton.Text = "Load picture";
            loadPictureButton.Click += loadPictureButton_Click;
            // 
            // dropDownButtonOptions
            // 
            dropDownButtonOptions.DisplayStyle = ToolStripItemDisplayStyle.Text;
            dropDownButtonOptions.DropDownItems.AddRange(new ToolStripItem[] { btnCompressAndSave, loadAndDecompressButton });
            dropDownButtonOptions.Image = (Image)resources.GetObject("dropDownButtonOptions.Image");
            dropDownButtonOptions.ImageTransparentColor = Color.Magenta;
            dropDownButtonOptions.Name = "dropDownButtonOptions";
            dropDownButtonOptions.Size = new Size(75, 24);
            dropDownButtonOptions.Text = "Options";
            // 
            // btnCompressAndSave
            // 
            btnCompressAndSave.Name = "btnCompressAndSave";
            btnCompressAndSave.Size = new Size(238, 26);
            btnCompressAndSave.Text = "Compress and save";
            btnCompressAndSave.Click += btnCompressAndSave_Click;
            // 
            // loadAndDecompressButton
            // 
            loadAndDecompressButton.Name = "loadAndDecompressButton";
            loadAndDecompressButton.Size = new Size(238, 26);
            loadAndDecompressButton.Text = "Load and decompress";
            loadAndDecompressButton.Click += loadAndDecompressButton_Click;
            // 
            // filtersOpenButton
            // 
            filtersOpenButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            filtersOpenButton.DropDownItems.AddRange(new ToolStripItem[] { sIerraDitheringToolStripMenuItem, meanRemovalToolStripMenuItem, edgeEnhanceToolStripMenuItem });
            filtersOpenButton.Image = (Image)resources.GetObject("filtersOpenButton.Image");
            filtersOpenButton.ImageTransparentColor = Color.Magenta;
            filtersOpenButton.Name = "filtersOpenButton";
            filtersOpenButton.Size = new Size(62, 24);
            filtersOpenButton.Text = "Filters";
            // 
            // sIerraDitheringToolStripMenuItem
            // 
            sIerraDitheringToolStripMenuItem.Name = "sIerraDitheringToolStripMenuItem";
            sIerraDitheringToolStripMenuItem.Size = new Size(196, 26);
            sIerraDitheringToolStripMenuItem.Text = "Sierra Dithering";
            sIerraDitheringToolStripMenuItem.Click += sIerraDitheringToolStripMenuItem_Click;
            // 
            // meanRemovalToolStripMenuItem
            // 
            meanRemovalToolStripMenuItem.Name = "meanRemovalToolStripMenuItem";
            meanRemovalToolStripMenuItem.Size = new Size(196, 26);
            meanRemovalToolStripMenuItem.Text = "Mean removal";
            meanRemovalToolStripMenuItem.Click += meanRemovalToolStripMenuItem_Click;
            // 
            // edgeEnhanceToolStripMenuItem
            // 
            edgeEnhanceToolStripMenuItem.Name = "edgeEnhanceToolStripMenuItem";
            edgeEnhanceToolStripMenuItem.Size = new Size(196, 26);
            edgeEnhanceToolStripMenuItem.Text = "Edge enhance";
            edgeEnhanceToolStripMenuItem.Click += edgeEnhanceToolStripMenuItem_Click;
            // 
            // lblImgNameText
            // 
            lblImgNameText.AutoSize = true;
            lblImgNameText.Location = new Point(14, 49);
            lblImgNameText.Name = "lblImgNameText";
            lblImgNameText.Size = new Size(95, 20);
            lblImgNameText.TabIndex = 2;
            lblImgNameText.Text = "Image name:";
            // 
            // lblImageName
            // 
            lblImageName.AutoSize = true;
            lblImageName.Location = new Point(118, 49);
            lblImageName.Name = "lblImageName";
            lblImageName.Size = new Size(45, 20);
            lblImageName.TabIndex = 3;
            lblImageName.Text = "None";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(14, 86);
            panel1.Name = "panel1";
            panel1.Size = new Size(1503, 862);
            panel1.TabIndex = 4;
            // 
            // ImageApp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1529, 975);
            Controls.Add(panel1);
            Controls.Add(lblImageName);
            Controls.Add(lblImgNameText);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ImageApp";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private ToolStripMenuItem loadAndDecompressButton;
        private ToolStripDropDownButton filtersOpenButton;
        private ToolStripMenuItem sIerraDitheringToolStripMenuItem;
        private ToolStripMenuItem meanRemovalToolStripMenuItem;
        private ToolStripMenuItem edgeEnhanceToolStripMenuItem;
        private Panel panel1;
    }
}
