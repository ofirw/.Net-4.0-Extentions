namespace Net_4._0_Extentions
{
    using System;

    public interface IRange<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
        bool Overlaps(IRange<T> range);
    }

    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        public Range(T start, T end)
        {
            this.Start = start;
            this.End = end;
        }

        public T Start { get; private set; }
        public T End { get; private set; }

        
        public bool Includes(T value)
        {
            return (this.Start.CompareTo(value) <= 0 && this.End.CompareTo(value) >= 0);
        }

        public bool Includes(IRange<T> range)
        {
            return (this.Includes(range.Start) && this.Includes(range.End));
        }

        public bool Overlaps(IRange<T> range)
        {
            return this.Includes(range.Start) || this.Includes(range.End) || range.Includes(this.Start);
        }
    }
}