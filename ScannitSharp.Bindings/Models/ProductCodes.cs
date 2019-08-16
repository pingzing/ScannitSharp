using OneOf;
using System;

namespace ScannitSharp.Bindings.Models.ProductCodes
{

    public static class ProductCode
    {
        public static OneOf<FaresFor2010, FaresFor2014> Create(ProductCodeKind kind, ushort value)
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

    public class FaresFor2010
    {
        public ProductCodeKind Kind => ProductCodeKind.FaresFor2010;
        public ushort Value { get; set; }
    }

    public class FaresFor2014
    {
        public ProductCodeKind Kind => ProductCodeKind.FaresFor2014;
        public ushort Value { get; set; }
    }
}
