namespace ScannitSharp.UwpExample
{
    public static class HslCommands
    {
        // Commands

        /// <summary>
        /// DESFire GetVersion command.
        /// </summary>
        public static byte[] GetVersionCommand { get; } = { 0x90, 0x60, 0x00, 0x00, 0x00 };

        /// <summary>
        /// DESFire command to return all installed application IDs on the card.
        /// </summary>
        public static byte[] GetApplicationIDsCommand { get; } = { 0x90, 0x6A, 0x00, 0x00, 0x00 };

        /// <summary>
        /// DESFire Select Application command for selecting the HSL application on the card.
        /// Returns <see cref="OkResponse"/> on success.
        /// </summary>
        public static byte[] SelectHslCommand { get; } = { 0x90, 0x5A, 0x00, 0x00, 0x03, 0x14, 0x20, 0xEF, 0x00 };

        /// <summary>
        /// Command to read app info file, which contains application version, card name, etc.
        /// </summary>
        public static byte[] ReadAppInfoCommand { get; } = { 0x90, 0xBD, 0x00, 0x00, 0x07, 0x08, 0x00, 0x00, 0x00, 0x0B, 0x00, 0x00, 0x00 };

        public static byte[] ReadControlInfoCommand { get; } = { 0x90, 0xBD, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, };

        /// <summary>
        /// Command to read the season pass file on the card.
        /// </summary>
        public static byte[] ReadPeriodPassCommand { get; } = { 0x90, 0xBD, 0x00, 0x00, 0x07, 0x01, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00 };

        /// <summary>
        /// Command to read the stored value on the card.
        /// </summary>
        public static byte[] ReadStoredValueCommand { get; } = { 0x90, 0xBD, 0x00, 0x00, 0x07, 0x02, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00 };

        /// <summary>
        /// Command to read the active eTicket on the card.
        /// </summary>
        public static byte[] ReadETicketCommand { get; } = { 0x90, 0xBD, 0x00, 0x00, 0x07, 0x03, 0x00, 0x00, 0x00, 0x2D, 0x00, 0x00, 0x00 };
        /// <summary>
        /// Command to read the 8 most recent transactions on the card.
        /// </summary>
        public static byte[] ReadHistoryCommand { get; } = { 0x90, 0xBB, 0x00, 0x00, 0x07, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        /// <summary>
        /// Reads the remaining bytes-to-be-sent if a read request returned <see cref="MoreData"/>.
        /// </summary>
        public static byte[] ReadNextCommand { get; } = { 0x90, 0xAF, 0x00, 0x00, 0x00 };

        // Responses
        /// <summary>
        /// DESFire OPERATION_OK response. In Hex: 91 00
        /// </summary>
        public static byte[] OkResponse { get; } = { 0x91, 0x00 };

        /// <summary>
        /// DESFire error response. Not sure what it's known as internally. In Hex: 91 9D
        /// </summary>
        public static byte[] ErrorResponse { get; } = { 0x91, 0x9D };

        /// <summary>
        /// DESFire ADDTIONAL_FRAME response. Indicates that more data is expected to be sent. In Hex: 91 AF
        /// </summary>
        public static byte[] MoreData { get; } = { 0x91, 0xAF };
    }
}
