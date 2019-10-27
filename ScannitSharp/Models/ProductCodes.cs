using OneOf;
using System;

namespace ScannitSharp.Models.ProductCodes
{

    internal static class ProductCode
    {
        internal static OneOf<FaresFor2010, FaresFor2014> Create(ProductCodeKind kind, ushort value)
        {
            switch (kind)
            {
                case ProductCodeKind.FaresFor2010:
                    return new FaresFor2010 { Value = value };
                case ProductCodeKind.FaresFor2014:
                    return new FaresFor2014 { Value = value };
                default:
                    throw new ArgumentException($"Given ProductCodeKind '{kind}' not supported.", nameof(kind));
            }
        }
    }

    /// <summary>
    /// Represents old-style fares and zones.
    /// </summary>
    public class FaresFor2010
    {
        /// <summary>
        /// Represents old-style fares and zones.
        /// </summary>
        public ushort Value { get; set; }
    }

    /// <summary>
    /// Represents new, ABC-style fares and zones.
    /// </summary>
    public class FaresFor2014
    {
        /// <summary>
        /// Represents new, ABC-style fares and zones.
        /// </summary>
        public ushort Value { get; set; }
    }
}
