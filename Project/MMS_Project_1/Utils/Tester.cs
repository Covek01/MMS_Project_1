using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.Utils
{
    public class Tester
    {
        public static Bitmap GenerateBitmap()
        {
            Bitmap bitmap = new Bitmap(4, 3);

            // Define the gradient colors
            /*  Color[,] colors = new Color[3, 5]
              {
                  { Color.FromArgb(174, 223, 222), Color.FromArgb(51, 153, 155), Color.FromArgb(102, 204, 55), Color.FromArgb(153, 255, 255), Color.FromArgb(204, 255, 255) },
                  { Color.FromArgb(174, 223, 222), Color.FromArgb(51, 153, 155), Color.FromArgb(102, 204, 55), Color.FromArgb(153, 255, 255), Color.FromArgb(204, 255, 255) },
                  { Color.FromArgb(174, 223, 222), Color.FromArgb(51, 153, 155), Color.FromArgb(102, 204, 55), Color.FromArgb(153, 255, 255), Color.FromArgb(204, 255, 255) },  
              };*/

/*            Color[,] colors = new Color[3, 5]
             {
                { Color.FromArgb(0, 0, 110), Color.FromArgb(10, 0, 120), Color.FromArgb(20, 5, 130), Color.FromArgb(30, 5, 140), Color.FromArgb(40, 0, 150) },
                { Color.FromArgb(0, 10, 110), Color.FromArgb(10, 10, 120), Color.FromArgb(20, 10, 130), Color.FromArgb(30, 10, 140), Color.FromArgb(40, 10, 150) },
                { Color.FromArgb(0, 30, 110), Color.FromArgb(10, 30, 120), Color.FromArgb(20, 30, 130), Color.FromArgb(30, 30, 140), Color.FromArgb(40, 30, 150) }
             };*/

            Color[,] colors = new Color[3, 4]
            {
                { Color.FromArgb(10, 0, 120), Color.FromArgb(20, 5, 130), Color.FromArgb(30, 5, 140), Color.FromArgb(40, 0, 150) },
                { Color.FromArgb(10, 10, 120), Color.FromArgb(20, 10, 130), Color.FromArgb(30, 10, 140), Color.FromArgb(40, 10, 150) },
                {  Color.FromArgb(10, 30, 120), Color.FromArgb(20, 30, 130), Color.FromArgb(30, 30, 140), Color.FromArgb(40, 30, 150) }
            };


            // Set colors to the bitmap
            for (int x = 0; x < bitmap.Height; x++)
            {
                for (int y = 0; y < bitmap.Width; y++)
                {
                    bitmap.SetPixel(y, x, colors[x, y]);
                }
            }

            return bitmap;
        }
    }
}
