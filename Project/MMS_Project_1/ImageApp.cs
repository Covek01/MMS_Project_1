using MMS_Project_1.Compressor;
using System.Windows.Forms;
using MMS_Project_1.Utils;
using MMS_Project_1.Converters;

namespace MMS_Project_1
{
    public partial class ImageApp : Form
    {
        private ISampler _sampler;
        private ICompressor _compressor;

        private Bitmap _image;
        public ImageApp(ISampler sampler = null, ICompressor compressor = null)
        {
            InitializeComponent();
            _sampler = sampler;
            _compressor = compressor;
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

        private async void btnCompressAndSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            Bitmap bmp = new Bitmap(pictureBox1.Image);
            //Bitmap bmp = Tester.GenerateBitmap();
            byte[] downsampledData = _sampler.Downsample(bmp);
            byte[] compressedData = _compressor.Compress(downsampledData);

            ReadingWritingUtil.WriteDataToFile(compressedData);
        }



        private void loadAndDecompressButton_Click(object sender, EventArgs e)
        {
            byte[] input = ReadingWritingUtil.ReadBinaryFile();
            byte[] decompressed = _compressor.Decompress(input);
            Bitmap bmp = _sampler.Upsample(decompressed);
            pictureBox1.Image = bmp;
        }

        private async void testButton_Click(object sender, EventArgs e)
        {
            byte[] input = ReadingWritingUtil.ReadBinaryFile();
            byte[] compressedData = _compressor.Compress(input);
            ReadingWritingUtil.WriteDataToFile(compressedData);
            byte[] test = _compressor.Decompress(compressedData);
            ReadingWritingUtil.WriteDataToFile(test);

        }

        //public async Task TestButtonAsyncFunction(object sender, EventArgs e)
        //{
        //    byte[] input = ReadingWritingUtil.ReadBinaryFile();
           
        //}
    }
}
