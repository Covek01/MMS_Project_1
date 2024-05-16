using MMS_Project_1.Compressor;
using System.Windows.Forms;
using MMS_Project_1.Utils;
using MMS_Project_1.Converters;
using MMS_Project_1.FIlters;
using Microsoft.VisualBasic;
using System;

namespace MMS_Project_1
{
    public partial class ImageApp : Form
    {
        private ISampler _sampler;
        private ICompressor _compressor;
        private static object _locker = new object();

        private Bitmap _image;
        public ImageApp(ISampler sampler = null, ICompressor compressor = null)
        {
            InitializeComponent();
            _sampler = sampler;
            _compressor = compressor;
        }



        private void loadPictureButton_Click(object sender, EventArgs e)
        {
            bool status = false;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                status = true;

            }
            var timespan = TimeSpan.FromMilliseconds(900);
            Task.Run(async () =>
            {
                if (Monitor.TryEnter(_locker, timespan))
                {
                    try
                    {
                        if (status)
                        {
                            // display image in picture box
                            pictureBox1.Image = new Bitmap(open.FileName);
                            lblImageName.Text = open.FileName;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(_locker);
                    }
                }
                else
                {
                    MessageBox.Show("Other image process is currently working");
                }
            });
        }

        private async void btnCompressAndSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            var timespan = TimeSpan.FromMilliseconds(900);

            if (Monitor.TryEnter(_locker, timespan))
            {
                try
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Image);
                    byte[] downsampledData = _sampler.Downsample(bmp);
                    byte[] compressedData = _compressor.Compress(downsampledData);

                    ReadingWritingUtil.WriteDataToFile(compressedData);
                }
                finally
                {
                    Monitor.Exit(_locker);
                }
            }
            else
            {
                MessageBox.Show("Other image process is currently working");
            }



        }



        private void loadAndDecompressButton_Click(object sender, EventArgs e)
        {
            var timespan = TimeSpan.FromMilliseconds(900);
            byte[] input = ReadingWritingUtil.ReadBinaryFile();

            Task.Run(async () =>
            {
                if (Monitor.TryEnter(_locker, timespan))
                {
                    try
                    {
                        byte[] decompressed = _compressor.Decompress(input);
                        Bitmap bmp = _sampler.Upsample(decompressed);
                        pictureBox1.Image = bmp;
                    }
                    finally
                    {
                        Monitor.Exit(_locker);
                    }
                }
                else
                {
                    MessageBox.Show("Other image process is currently working");
                }
            });

        }

        //private async void testButton_Click(object sender, EventArgs e)
        //{
        //    byte[] input = ReadingWritingUtil.ReadBinaryFile();
        //    byte[] compressedData = _compressor.Compress(input);
        //    ReadingWritingUtil.WriteDataToFile(compressedData);
        //    byte[] test = _compressor.Decompress(compressedData);
        //    ReadingWritingUtil.WriteDataToFile(test);

        //}

        private async void sIerraDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("IMAGE NOT SELECTED!");
                return;
            }

            var timespan = TimeSpan.FromMilliseconds(900);
            Task.Run(async () =>
            {
                if (Monitor.TryEnter(_locker, timespan))
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Image);
                        await SierraDithering.PerformFilter(bmp);
                        pictureBox1.Image = bmp;
                    }
                    finally
                    {
                        Monitor.Exit(_locker);
                    }
                }
                else
                {
                    MessageBox.Show("Other image process is currently working");
                }
            });


            //Task task = new Task(() => SierraDithering.PerformFilter(bmp);
            //task.Run;
        }


        private async void meanRemovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("IMAGE NOT SELECTED!");
                return;
            }
            var timespan = TimeSpan.FromMilliseconds(900);
            Task.Run(async () =>
            {
                
                if (Monitor.TryEnter(_locker, timespan))
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Image);
                        Bitmap newBmp = await MeanRemoval.Convolution3x3(bmp);
                        pictureBox1.Image = newBmp;
                    }
                    finally
                    {
                        Monitor.Exit(_locker);
                    }
                }
                else
                {
                    MessageBox.Show("Other image process is currently working");
                }
            });


        }

        private async void edgeEnhanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("IMAGE NOT SELECTED!");
                return;
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox("Select value for Edge echance",
                       "Choose value",
                       "Default",
                       0,
                       0);
            int value;
            if (!int.TryParse(input, out value))
                value = 127;

            var timespan = TimeSpan.FromMilliseconds(900);
            Task.Run(async () =>
            {
                if (Monitor.TryEnter(_locker, timespan))
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(pictureBox1.Image);
                        Bitmap newBmp = await EdgeEnchance.Convolution3x3(bmp, (byte)value);
                        Color a = newBmp.GetPixel(1, 1);
                        pictureBox1.Image = newBmp;
                    }
                    finally
                    {
                        Monitor.Exit(_locker);
                    }
                }
                else
                {
                    MessageBox.Show("Other image process is currently working");
                }
            });


        }

        //public async Task TestButtonAsyncFunction(object sender, EventArgs e)
        //{
        //    byte[] input = ReadingWritingUtil.ReadBinaryFile();

        //}
    }
}
