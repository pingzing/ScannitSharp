using OneOf;
using System;

namespace ScannitSharp.Bindings.Models.ValidityLengths
{
    public static class ValidityLength
    {
        public static OneOf<Minutes, Hours, TwentyFourHourPeriods, Days> Create(ValidityLengthKind kind, byte value)
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
    public class Minutes
    {
        public ValidityLengthKind Kind => ValidityLengthKind.Minutes;
        public byte Value { get; set; }
    }

    public class Hours
    {
        public ValidityLengthKind Kind => ValidityLengthKind.Hours;
        public byte Value { get; set; }
    }

    public class TwentyFourHourPeriods
    {
        public ValidityLengthKind Kind => ValidityLengthKind.TwentyFourHourPeriods;
        public byte Value { get; set; }
    }

    public class Days
    {
        public ValidityLengthKind Kind => ValidityLengthKind.Days;
        public byte Value { get; set; }
    }
}
