using NUnit.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SLAEMathNet
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            double[] x, rightX;
            bool testPass = true;
            using (StreamReader sr = new StreamReader("..\\..\\..\\Test1.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                double[,] a = null;
                double[] b = null;

                var solver = new SLAEsolverMathNet();
                solver.ParseSLAE(file, ref a, ref b);
                x = solver.SolveSLAE(a, b);
            }
            using (StreamReader sr = new StreamReader("..\\..\\..\\Test1_answer.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                Regex regex = new Regex("[\r\n]+");
                string[] answers = regex.Split(file);
                rightX = new double[answers.Length];
                for (int i = 0; i < answers.Length; i++)
                    rightX[i] = Convert.ToDouble(answers[i]);
            }

            for (int i = 0; i < rightX.Length; i++)
                if (Math.Abs(rightX[i] - x[i]) > 0.0001)
                    testPass = false;

            Assert.True(testPass);
        }

        [Test]
        public void Test2()
        {
            double[] x, rightX;
            bool testPass = true;
            using (StreamReader sr = new StreamReader("..\\..\\..\\Test2.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                double[,] a = null;
                double[] b = null;

                var solver = new SLAEsolverMathNet();
                solver.ParseSLAE(file, ref a, ref b);
                x = solver.SolveSLAE(a, b);
            }
            using (StreamReader sr = new StreamReader("..\\..\\..\\Test2_answer.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                Regex regex = new Regex("[\r\n]+");
                string[] answers = regex.Split(file);
                rightX = new double[answers.Length];
                for (int i = 0; i < answers.Length; i++)
                    rightX[i] = Convert.ToDouble(answers[i]);
            }

            for (int i = 0; i < rightX.Length; i++)
                if (Math.Abs(rightX[i] - x[i]) > 0.0001)
                    testPass = false;

            Assert.True(testPass);
        }

        [Test]
        public void Test3()
        {
            double[] x, rightX;
            bool testPass = true;
            using (StreamReader sr = new StreamReader("..\\..\\..\\Test3.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                double[,] a = null;
                double[] b = null;

                var solver = new SLAEsolverMathNet();
                solver.ParseSLAE(file, ref a, ref b);
                x = solver.SolveSLAE(a, b);
            }
            using (StreamReader sr = new StreamReader("..\\..\\..\\Test3_answer.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                Regex regex = new Regex("[\r\n]+");
                string[] answers = regex.Split(file);
                rightX = new double[answers.Length];
                for (int i = 0; i < answers.Length; i++)
                    rightX[i] = Convert.ToDouble(answers[i]);
            }

            for (int i = 0; i < rightX.Length; i++)
                if (Math.Abs(rightX[i] - x[i]) > 0.0001)
                    testPass = false;

            Assert.True(testPass);
        }

        [Test]
        public void TestTooManyEquations()
        {
            bool testPass = false;
            using (StreamReader sr = new StreamReader("..\\..\\..\\TestTooManyEquations.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                double[,] a = null;
                double[] b = null;

                var solver = new SLAEsolverMathNet();
                if (solver.ParseSLAE(file, ref a, ref b) == ErrorCode.TooManyEquations)
                    testPass = true;
            }

            Assert.True(testPass);
        }

        [Test]
        public void TestNotEnoughtEquations()
        {
            bool testPass = false;
            using (StreamReader sr = new StreamReader("..\\..\\..\\TestNotEnoughtEquations.txt"))
            {
                string file = sr.ReadToEnd();
                file = file.Replace('.', ',');
                double[,] a = null;
                double[] b = null;

                var solver = new SLAEsolverMathNet();
                if (solver.ParseSLAE(file, ref a, ref b) == ErrorCode.NotEnoughtEquations)
                    testPass = true;
            }

            Assert.True(testPass);
        }
    }
}