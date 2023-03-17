﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrankHileman.MedianFilter2D;

namespace FrankHileman.MedianFilter2DTests
{
	[TestClass]
	public class QuickTest
	{
		static void compare(int l, double[] output, double[] expected)
		{
			for (int i = 0; i < l; ++i)
			{
				Assert.IsTrue(output[i] == expected[i]
					|| (double.IsNaN(output[i]) && double.IsNaN(expected[i])),
					"mismatch: " + i + ": " + output[i] + " vs. " + expected[i]);
			}
		}

		static void check_1d(int x, int hx, double[] input, double[] output, double[] expected)
		{
			new Filter().median_filter_1d(x, hx, 0, input, output);
			compare(x, output, expected);
		}

		static void check_2d(int x, int y, int hx, int hy, double[] input, double[] output, double[] expected)
		{
			new Filter().median_filter_2d(x, y, hx, hy, 0, input, output);
			compare(x * y, output, expected);
		}

		[TestMethod]
		public void Test0()
		{
			const int x = 1;
			const int y = 1;
			double X = double.NaN;
			double[] input = new double[x * y];
			double[] output = new double[x * y];
			check_2d(x, y, 0, 0, input, output, input);
			check_2d(x, y, 1, 1, input, output, input);
			check_2d(x, y, 100, 0, input, output, input);
			check_2d(x, y, 0, 100, input, output, input);
			check_2d(x, y, 100, 100, input, output, input);
			input[0] = 1;
			check_2d(x, y, 0, 0, input, output, input);
			check_2d(x, y, 1, 1, input, output, input);
			check_2d(x, y, 100, 0, input, output, input);
			check_2d(x, y, 0, 100, input, output, input);
			check_2d(x, y, 100, 100, input, output, input);
			input[0] = X;
			check_2d(x, y, 0, 0, input, output, input);
			check_2d(x, y, 1, 1, input, output, input);
			check_2d(x, y, 100, 0, input, output, input);
			check_2d(x, y, 0, 100, input, output, input);
			check_2d(x, y, 100, 100, input, output, input);
		}

		[TestMethod]
		public void Test1()
		{
			const int x = 10;
			const int y = 5;
			double H = 0.5;
			double[] input =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,0,0,0,0,0,
				0,0,0,1,1,1,0,0,2,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] zero =
			{
				0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0
			};
			double[] exp01 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,1,0
			};
			double[] exp10 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,0,0,0,0,0,
				0,0,0,1,1,1,0,0,0,1,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp20 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				0,0,0,1,1,1,1,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp30 =
			{
				0,0,H,0,0,0,0,0,0,0,
				0,0,H,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				0,0,H,0,0,1,1,H,0,0,
				0,0,H,0,0,0,0,0,0,0
			};
			double[] exp40 =
			{
				0,H,0,0,0,0,0,0,0,0,
				0,H,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				0,H,0,0,0,0,H,1,H,0,
				0,H,0,0,0,0,0,0,0,0
			};
			double[] exp50 =
			{
				H,0,0,0,0,0,0,0,0,0,
				H,0,0,0,0,0,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				H,0,0,0,0,0,0,H,1,H,
				H,0,0,0,0,0,0,0,0,0
			};
			double[] exp11 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp1 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,0,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp2 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,0,0,0,0,0,0,0,
				0,0,0,1,1,1,1,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] output = new double[x * y];
			check_2d(x, y, 0, 0, input, output, input);
			check_2d(x, y, 0, 1, input, output, exp01);
			check_2d(x, y, 0, 2, input, output, exp11);
			check_2d(x, y, 0, 3, input, output, exp11);
			check_2d(x, y, 0, 4, input, output, exp11);
			check_2d(x, y, 0, 99, input, output, exp11);
			check_2d(x, y, 1, 0, input, output, exp10);
			check_2d(x, y, 2, 0, input, output, exp20);
			check_2d(x, y, 3, 0, input, output, exp30);
			check_2d(x, y, 4, 0, input, output, exp40);
			check_2d(x, y, 5, 0, input, output, exp50);
			check_2d(x, y, 8, 0, input, output, zero);
			check_2d(x, y, 99, 0, input, output, zero);
			check_2d(x, y, 1, 1, input, output, exp11);
			check_2d(x, y, 2, 2, input, output, exp11);
			check_2d(x, y, 4, 4, input, output, zero);
			check_2d(x * y, 1, 1, 0, input, output, exp1);
			check_2d(x * y, 1, 1, 1, input, output, exp1);
			check_2d(x * y, 1, 1, 99, input, output, exp1);
			check_2d(x * y, 1, 2, 0, input, output, exp2);
			check_2d(x * y, 1, 2, 1, input, output, exp2);
			check_2d(x * y, 1, 2, 99, input, output, exp2);
			check_2d(1, x * y, 0, 1, input, output, exp1);
			check_2d(1, x * y, 1, 1, input, output, exp1);
			check_2d(1, x * y, 99, 1, input, output, exp1);
			check_2d(1, x * y, 0, 2, input, output, exp2);
			check_2d(1, x * y, 1, 2, input, output, exp2);
			check_2d(1, x * y, 99, 2, input, output, exp2);
			check_1d(x * y, 1, input, output, exp1);
			check_1d(x * y, 2, input, output, exp2);
		}

		[TestMethod]
		public void Test2()
		{
			const int x = 10;
			const int y = 5;
			double X = double.NaN;
			double H = 0.5;
			double[] input =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,X,0,0,0,0,
				0,0,0,1,1,1,0,0,2,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp01 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,1,0
			};
			double[] exp10 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,H,0,0,0,0,
				0,0,0,1,1,1,0,0,0,1,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp11 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp1 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,H,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp2 =
			{
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,H,H,H,0,0,0,0,
				0,0,0,1,1,1,1,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] output = new double[x * y];
			check_2d(x, y, 0, 0, input, output, input);
			check_2d(x, y, 0, 1, input, output, exp01);
			check_2d(x, y, 1, 0, input, output, exp10);
			check_2d(x, y, 1, 1, input, output, exp11);
			check_2d(x * y, 1, 1, 0, input, output, exp1);
			check_2d(x * y, 1, 1, 1, input, output, exp1);
			check_2d(x * y, 1, 1, 99, input, output, exp1);
			check_2d(x * y, 1, 2, 0, input, output, exp2);
			check_2d(x * y, 1, 2, 1, input, output, exp2);
			check_2d(x * y, 1, 2, 99, input, output, exp2);
			check_2d(1, x * y, 0, 1, input, output, exp1);
			check_2d(1, x * y, 1, 1, input, output, exp1);
			check_2d(1, x * y, 99, 1, input, output, exp1);
			check_2d(1, x * y, 0, 2, input, output, exp2);
			check_2d(1, x * y, 1, 2, input, output, exp2);
			check_2d(1, x * y, 99, 2, input, output, exp2);
			check_1d(x * y, 1, input, output, exp1);
			check_1d(x * y, 2, input, output, exp2);
		}

		[TestMethod]
		public void Test3()
		{
			const int x = 10;
			const int y = 5;
			double X = double.NaN;
			double H = 0.5;
			double[] input =
			{
				0,0,0,1,1,X,X,X,X,X,
				0,0,0,1,1,1,X,X,X,X,
				0,0,0,1,1,0,0,X,X,X,
				0,0,0,1,1,1,0,0,X,X,
				0,0,0,1,1,1,0,0,0,X
			};
			double[] exp01 =
			{
				0,0,0,1,1,1,X,X,X,X,
				0,0,0,1,1,H,0,X,X,X,
				0,0,0,1,1,1,0,0,X,X,
				0,0,0,1,1,1,0,0,0,X,
				0,0,0,1,1,1,0,0,0,X
			};
			double[] exp10 =
			{
				0,0,0,1,1,1,X,X,X,X,
				0,0,0,1,1,1,1,X,X,X,
				0,0,0,1,1,0,0,0,X,X,
				0,0,0,1,1,1,0,0,0,X,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp11 =
			{
				0,0,0,1,1,1,1,X,X,X,
				0,0,0,1,1,1,0,0,X,X,
				0,0,0,1,1,1,0,0,0,X,
				0,0,0,1,1,1,0,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] exp22 =
			{
				0,0,0,H,1,1,1,0,0,X,
				0,0,0,1,1,1,1,0,0,0,
				0,0,0,1,1,1,1,0,0,0,
				0,0,0,1,1,1,H,0,0,0,
				0,0,0,1,1,1,0,0,0,0
			};
			double[] output = new double[x * y];
			check_2d(x, y, 0, 0, input, output, input);
			check_2d(x, y, 0, 1, input, output, exp01);
			check_2d(x, y, 1, 0, input, output, exp10);
			check_2d(x, y, 1, 1, input, output, exp11);
			check_2d(x, y, 2, 2, input, output, exp22);
		}
	}
}