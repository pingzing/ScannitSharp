namespace ScannitSharp.Bindings.Models
{
    public enum BoardingDirection : uint
    {
        TowardEnd = 0,
        TowardStart = 1,
    }

    public enum Language : uint
    {
        Finnish = 0,
        Swedish = 1,
        English = 2,
    }

    public enum TransactionType : uint
    {
        SeasonPass = 0,
        ValueTicket = 1,
    }

    /// <summary>
    /// The HSL fare zone(s) in which a ticket is valid.
    /// </summary>
    public enum ValidityZone
    {
        ZoneA = 0,
        ZoneB = 1,
        ZoneC = 2,
        ZoneD = 3,
        ZoneE = 4,
        ZoneF = 5,
        ZoneG = 6,
        ZoneH = 7,
    }

    /// <summary>
    /// The vehicle type on which this ticket is valid.
    /// </summary>
    public enum VehicleType
    {
        Undefined = 0,
        Bus = 1,
        Tram = 5,
        Metro = 6,
        Train = 7,
        Ferry = 8,
        ULine = 9,
    }

    public enum ProductCodeKind : uint
    {
        FaresFor2010 = 0,
        FaresFor2014 = 1,
    }

    public enum ValidityAreaKind : uint
    {
        OldZone = 0,
        VehicleType = 1,
        NewZone = 2,
    }

    public enum BoardingLocationKind : uint
    {
        NoneOrReserved = 0,
        BusNumber = 1,
        TrainNumber = 2,
        PlatformNumber = 3,
    }

    public enum ValidityLengthKind : uint
    {
        Minutes = 0,
        Hours = 1,
        TwentyFourHourPeriods = 2,
        Days = 3,
    }

    public enum SaleDeviceKind : uint
    {
        ServicePointSalesDevice = 0,
        DriverTicketMachine = 1,
        CardReader = 2,
        TicketMachine = 3,
        Server = 4,
        HSLSmallEquipment = 5,
        ExternalServiceEquipment = 6,
        Reserved = 7,
    }

    public enum BoardingAreaKind : uint
    {
        Zone = 0,
        Vehicle = 1,
        ZoneCircle = 2,
    }
}
