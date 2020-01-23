using System;
using System.Collections.Generic;
using System.Text;

namespace ChessOnline.Models.Primitives
{
    public struct Vector
    {
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; private set; }
        public double Y { get; private set; }
        public Vector Clone() => new Vector(X, Y);
        public Vector Update(double x, double y)
        {
            X = x;
            Y = y;
            return this;
        }
        public Vector Sum(Vector vector)
        {
            X += vector.X;
            Y += vector.Y;
            return this;
        }
        public Vector Subtract(Vector vector) => Sum(new Vector(-vector.X, -vector.Y));
        public override string ToString()
        {
            return X + "-" + Y;
        }
    }
}
