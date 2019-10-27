namespace ScannitSharp.Models
{
    /// <summary>
    /// The direction is transit vehicle is traveling in.
    /// Note: this is speculation. The official documentation is unclear on the exact meaning of this.
    /// </summary>
    public enum BoardingDirection : uint
    {
        /// <summary>
        /// This vehicle is traveling toward the start of its route.
        /// </summary>
        TowardEnd = 0,

        /// <summary>
        /// This vehicle is traveling toward the end of its route.
        /// </summary>
        TowardStart = 1,
    }

    /// <summary>
    /// The language a ticket or travel card should be displayed in.
    /// </summary>
    public enum Language : uint
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Finnish = 0,
        Swedish = 1,
        English = 2,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// The type of a transaction in which the travel card was used to board a vehicle.
    /// </summary>
    public enum TransactionType : uint
    {
        /// <summary>
        /// Represents a season pass transaction.
        /// </summary>
        SeasonPass = 0,

        /// <summary>
        /// Represents a single ticket transaction.
        /// </summary>
        ValueTicket = 1,
    }

    /// <summary>
    /// The HSL fare zone(s) in which a ticket is valid.
    /// </summary>
    public enum ValidityZone
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ZoneA = 0,
        ZoneB = 1,
        ZoneC = 2,
        ZoneD = 3,
        ZoneE = 4,
        ZoneF = 5,
        ZoneG = 6,
        ZoneH = 7,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// The vehicle type on which this ticket is valid.
    /// </summary>
    public enum VehicleType
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Undefined = 0,
        Bus = 1,
        Tram = 5,
        Metro = 6,
        Train = 7,

        /// <summary>
        /// Used for the ferry to Suomenlinna.
        /// </summary>
        Ferry = 8,

        /// <summary>
        /// Uncertain how this differs from 'Metro'. Might be unused, or legacy.
        /// </summary>
        ULine = 9,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// A value indicating whether a season pass or ticket is using the old-style fares and zones, or new-style ones.
    /// </summary>
    public enum ProductCodeKind : uint
    {
        /// <summary>
        /// Represents the old-style fares and zones.
        /// </summary>
        FaresFor2010 = 0,

        /// <summary>
        /// Represents the new-style fares and zones. (Don't be fooled by the year.)
        /// </summary>
        FaresFor2014 = 1,
    }

    /// <summary>
    /// How a validity area should be interpreted.
    /// </summary>
    public enum ValidityAreaKind : uint
    {
        /// <summary>
        /// Represents an old, region-style fare zone.
        /// </summary>
        OldZone = 0,

        /// <summary>
        /// Represents a vehicle type validity.
        /// </summary>
        VehicleType = 1,

        /// <summary>
        /// Represent a new, ABC-style fare zone.
        /// </summary>
        NewZone = 2,
    }

    /// <summary>
    /// How a boarding location value should be interpreted.
    /// </summary>
    public enum BoardingLocationKind : uint
    {
        /// <summary>
        /// No relevant boarding information.
        /// </summary>
        NoneOrReserved = 0,

        /// <summary>
        /// A boarded bus's number.
        /// </summary>
        BusNumber = 1,

        /// <summary>
        /// A boarded train's number.
        /// </summary>
        TrainNumber = 2,

        /// <summary>
        /// An entered platform's number.
        /// </summary>
        PlatformNumber = 3,
    }

    /// <summary>
    /// How a validity length value should be interpreted.
    /// </summary>
    public enum ValidityLengthKind : uint
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Minutes = 0,
        Hours = 1,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// 24-hour periods that begin an end a specific hour and minute,
        /// a.k.a a Finnish 'vuorokausi'.
        /// </summary>
        TwentyFourHourPeriods = 2,

        /// <summary>
        /// 24-hour periods that begin and end at midnight.
        /// </summary>
        Days = 3,
    }

    /// <summary>
    /// How a sale device value should be interpreted.
    /// </summary>
    public enum SaleDeviceKind : uint
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        ServicePointSalesDevice = 0,
        DriverTicketMachine = 1,
        CardReader = 2,
        TicketMachine = 3,
        Server = 4,
        HSLSmallEquipment = 5,
        ExternalServiceEquipment = 6,
        Reserved = 7,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// How a boarding area value should be interpreted.
    /// </summary>
    public enum BoardingAreaKind : uint
    {
        /// <summary>
        /// This boarding area was a new ABC-style zone.
        /// </summary>
        Zone = 0,

        /// <summary>
        /// This boarding area was a vehicle.
        /// </summary>
        Vehicle = 1,

        /// <summary>
        /// This boarding area was an old region-style zone.
        /// </summary>
        ZoneCircle = 2,
    }
}
