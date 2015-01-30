namespace Net_4._0_Extentions
{
    using System;

    public static class MathExtensions
    {
        public static int FloorToMultiplierOf(this double value, int snap)
        {
            return (int)(Math.Floor(value / snap) * snap);
        }

        public static int CeilingToMultiplierOf(this double value, int snap)
        {
            return (int)CeilingToMultiplierOf(value, (double)snap);
        }

        public static int FloorToMultiplierOf(this float value, int snap)
        {
            return FloorToMultiplierOf((double)value, snap);
        }

        public static int CeilingToMultiplierOf(this float value, int snap)
        {
            return CeilingToMultiplierOf((double)value, snap);
        }

        public static int FloorToMultiplierOf(this int value, int snap)
        {
            return FloorToMultiplierOf((double)value, snap);
        }

        public static int CeilingToMultiplierOf(this int value, int snap)
        {
            return CeilingToMultiplierOf((double)value, snap);
        }

        public static double CeilingToMultiplierOf(this double value, double snap)
        {
            return (int)(Math.Ceiling(value / snap) * snap);
        }
    }
}