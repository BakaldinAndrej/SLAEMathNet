using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLAEMathNet
{
    interface ISLAEsolver
    {
        double[] SolveSLAE(double[,] a, double[] b);
        ErrorCode ParseSLAE(string file, ref double[,] a, ref double[] b);
    }
}
