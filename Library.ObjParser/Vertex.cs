﻿using System;

namespace Library.ObjParser
{
    public class Vertex : IType
    {
        public const int MinimumDataLength = 4;
        public const string Prefix = "v";

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public int Index { get; set; }

        public void LoadFromStringArray(string[] data)
        {
            if (data.Length < MinimumDataLength)
                throw new ArgumentException("Input array must be of minimum length " + MinimumDataLength, "data");

            if (!data[0].ToLower().Equals(Prefix))
                throw new ArgumentException("Data prefix must be '" + Prefix + "'", "data");

            bool success;

            double x, y, z;

            success = double.TryParse(data[1], out x);
            if (!success) throw new ArgumentException("Could not parse X parameter as double");

            success = double.TryParse(data[2], out y);
            if (!success) throw new ArgumentException("Could not parse Y parameter as double");

            success = double.TryParse(data[3], out z);
            if (!success) throw new ArgumentException("Could not parse Z parameter as double");

            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return string.Format("v {0} {1} {2}", X, Y, Z);
        }
    }
}
