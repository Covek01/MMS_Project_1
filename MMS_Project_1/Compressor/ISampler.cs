using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.Compressor
{
    public interface ISampler
    {
        byte[] Downsample(Bitmap image);
        Bitmap Upsample(byte[] image);

    }
}
