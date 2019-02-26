using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class NumericExtensions {

    public static float Round(this float value, int digits = 0) {
        return (float)Math.Round(value, digits);
    }

    public static double Round(this double value, int digits = 0) {
        return Math.Round(value, digits);
    }
}
