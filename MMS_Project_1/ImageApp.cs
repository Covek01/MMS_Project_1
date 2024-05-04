using MMS_Project_1.Compressor;

namespace MMS_Project_1
{
    public partial class ImageApp : Form
    {
        private ISampler _sampler;
        private Bitmap _image;
        public ImageApp(ISampler sampler = null)
        {
            InitializeComponent();
            _sampler = sampler;
        }

        private void dropDownButtonFilters_Click(object sender, EventArgs e)
        {

        }

        private void loadPictureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                pictureBox1.Image = new Bitmap(open.FileName);
                lblImageName.Text = open.FileName;
            }
        }

        private void btnCompressAndSave_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            byte[] compressedData = _sampler.Downsample(bmp);

            byte[] b = { 0x00 };
            WriteDataToFile(compressedData);
        }

        private static void WriteDataToFile(byte[] data)
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
    }
}
