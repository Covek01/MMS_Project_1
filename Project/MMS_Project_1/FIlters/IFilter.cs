using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS_Project_1.FIlters
{
    internal interface IFilter
    {
        Task PerformFilter(Bitmap image);
    }
}
