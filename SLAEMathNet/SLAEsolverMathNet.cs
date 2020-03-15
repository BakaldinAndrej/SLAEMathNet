using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SLAEMathNet
{
    public enum ErrorCode : int
    {
        None = 0,
        NotEnoughtEquations = 1,
        TooManyEquations = 2
    }

    public class SLAEsolverMathNet : ISLAEsolver
    {
        public double[] SolveSLAE(double[,] a, double[] b)
        {
            var A = Matrix<double>.Build.DenseOfArray(a);
            var B = Vector<double>.Build.Dense(b);
            var x = A.Solve(B);
            return x.ToArray();
        }

        public ErrorCode ParseSLAE(string file, ref double[,] a, ref double[] b)
        {
            Regex regex = new Regex("[*=+]+|[\r\n]+");
            string[] matrix = regex.Split(file);

            //Вычленение индексов х-ов и значений
            string[] minusSplited;
            List<int> index = new List<int>();
            List<double> value = new List<double>();
            for (int i = 0; i < matrix.Length; i++)
                if (matrix[i].IndexOf('-') != -1)
                {
                    minusSplited = matrix[i].Split('-');
                    for (int j = 1; j < minusSplited.Length; j++)
                    {
                        minusSplited[j] = "-" + minusSplited[j];
                    }

                    for (int j = 0; j < minusSplited.Length; j++)
                    {
                        if (minusSplited[j] != "")
                            if (minusSplited[j].IndexOf('x') != -1)
                                index.Add(Convert.ToInt32(minusSplited[j].Trim('x')));
                            else
                                value.Add(Convert.ToDouble(minusSplited[j]));
                    }
                }
                else if (matrix[i].IndexOf('x') != -1)
                    index.Add(Convert.ToInt32(matrix[i].Trim('x')));
                else
                    value.Add(Convert.ToDouble(matrix[i]));

            //Проверка на число уравнений
            int systemSize = 1, equations;
            for (int i = 0; i < index.Count; i++)
                if (systemSize < index[i])
                    systemSize = index[i];
            equations = file.Split(new string[] { "=" }, StringSplitOptions.None).Count() - 1;
            if (systemSize > equations)
                return ErrorCode.NotEnoughtEquations;
            if (systemSize < equations)
                return ErrorCode.TooManyEquations;

            a = new double[systemSize, systemSize];
            b = new double[systemSize];
            for (int i = 0; i < systemSize; i++)
            {
                for (int j = 0; j < systemSize; j++)
                    a[i, index[j] - 1] = value[i * (systemSize + 1) + j];
                b[i] = value[i * (systemSize + 1) + systemSize];
            }
            return ErrorCode.None;
        }
    }
}
