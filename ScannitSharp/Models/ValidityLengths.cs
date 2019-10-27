using OneOf;
using System;

namespace ScannitSharp.Models.ValidityLengths
{
    internal static class ValidityLength
    {
        internal static OneOf<Minutes, Hours, TwentyFourHourPeriods, Days> Create(ValidityLengthKind kind, byte value)
        {
            switch (kind)
            {
                case ValidityLengthKind.Minutes:
                    return new Minutes { Value = value };
                case ValidityLengthKind.Hours:
                    return new Hours { Value = value };
                case ValidityLengthKind.TwentyFourHourPeriods:
                    return new TwentyFourHourPeriods { Value = value };
                case ValidityLengthKind.Days:
                    return new Days { Value = value };
                default:
                    throw new ArgumentException($"ValidityLengthKind '{kind}' is unsupported.", nameof(kind));
            }
        }
    }


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Minutes
    {
        public byte Value { get; set; }
    }

    public class Hours
    {
        public byte Value { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// 24-hour periods that begin an end a specific hour and minute,
    /// a.k.a a Finnish 'vuorokausi'.
    /// </summary>
    public class TwentyFourHourPeriods
    {
        /// <summary>
        /// 24-hour periods that begin an end a specific hour and minute,
        /// a.k.a a Finnish 'vuorokausi'.
        /// </summary>
        public byte Value { get; set; }
    }

    /// <summary>
    /// 24-hour periods that begin and end at midnight.
    /// </summary>
    public class Days
    {
        /// <summary>
        /// 24-hour periods that begin and end at midnight.
        /// </summary>
        public byte Value { get; set; }
    }
}
