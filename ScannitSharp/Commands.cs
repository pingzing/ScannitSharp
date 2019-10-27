using System;
using System.Runtime.InteropServices;

namespace ScannitSharp
{
    /// <summary>
    /// DESFire APDU commands that the HSL card accepts.
    /// </summary>
    public static class Commands
    {
        private const int _getVersionLength = 5;
        private static byte[] _getVersionCommand;

        /// <summary>
        /// DESFire GetVersion command.
        /// </summary>
        public static byte[] GetVersionCommand
        {
            get
            {
                if (_getVersionCommand == null)
                {
                    _getVersionCommand = new byte[_getVersionLength];
                    IntPtr cmdPtr = Native.get_GET_VERSION_COMMAND();
                    Marshal.Copy(cmdPtr, _getVersionCommand, 0, _getVersionLength);
                }
                return _getVersionCommand;
            }
        }

        private const int _getApplicationIdsLength = 5;
        private static byte[] _getApplicationIdsCommand;
        /// <summary>
        /// DESFire command to return all installed application IDs on the card.
        /// </summary>
        public static byte[] GetApplicationIdsCommand
        {
            get
            {
                if (_getApplicationIdsCommand == null)
                {
                    _getApplicationIdsCommand = new byte[_getApplicationIdsLength];
                    IntPtr cmdPtr = Native.get_GET_APPLICATION_IDS_COMMAND();
                    Marshal.Copy(cmdPtr, _getApplicationIdsCommand, 0, _getApplicationIdsLength);
                }
                return _getApplicationIdsCommand;
            }
        }

        private const int _selectHslLength = 9;
        private static byte[] _selectHslCommand;
        /// <summary>
        /// DESFire Select Application command for selecting the HSL application on the card.
        /// Returns <see cref="OkResponse"/> on success.
        /// </summary>
        public static byte[] SelectHslCommand
        {
            get
            {
                if (_selectHslCommand == null)
                {
                    _selectHslCommand = new byte[_selectHslLength];
                    IntPtr cmdPtr = Native.get_SELECT_HSL_COMMAND();
                    Marshal.Copy(cmdPtr, _selectHslCommand, 0, _selectHslLength);
                }
                return _selectHslCommand;
            }
        }

        private const int _readAppInfoLength = 13;
        private static byte[] _readAppInfoCommand;
        /// <summary>
        /// Command to read app info file, which contains application version, card name, etc.
        /// </summary>
        public static byte[] ReadAppInfoCommand
        {
            get
            {
                if (_readAppInfoCommand == null)
                {
                    _readAppInfoCommand = new byte[_readAppInfoLength];
                    IntPtr cmdPtr = Native.get_READ_APP_INFO_COMMAND();
                    Marshal.Copy(cmdPtr, _readAppInfoCommand, 0, _readAppInfoLength);
                }
                return _readAppInfoCommand;
            }
        }

        private const int _readControlInfoLength = 13;
        private static byte[] _readControlInfoCommand;
        /// <summary>
        /// Command to read the control info file from the card.
        /// </summary>
        public static byte[] ReadControlInfoCommand
        {
            get
            {
                if (_readControlInfoCommand == null)
                {
                    _readControlInfoCommand = new byte[_readControlInfoLength];
                    IntPtr cmdPtr = Native.get_READ_CONTROL_INFO_COMMAND();
                    Marshal.Copy(cmdPtr, _readControlInfoCommand, 0, _readControlInfoLength);
                }
                return _readControlInfoCommand;
            }
        }

        private const int _readPeriodPassLength = 13;
        private static byte[] _readPeriodPassCommand;
        /// <summary>
        /// Command to read the season pass file on the card.
        /// </summary>
        public static byte[] ReadPeriodPassCommand
        {
            get
            {
                if (_readPeriodPassCommand == null)
                {
                    _readPeriodPassCommand = new byte[_readPeriodPassLength];
                    IntPtr cmdPtr = Native.get_READ_PERIOD_PASS_COMMAND();
                    Marshal.Copy(cmdPtr, _readPeriodPassCommand, 0, _readPeriodPassLength);
                }
                return _readPeriodPassCommand;
            }
        }

        private const int _readStoredValueLength = 13;
        private static byte[] _readStoredValueCommand;
        /// <summary>
        /// Command to read the stored value on the card.
        /// </summary>
        public static byte[] ReadStoredValueCommand
        {
            get
            {
                if (_readStoredValueCommand == null)
                {
                    _readStoredValueCommand = new byte[_readStoredValueLength];
                    IntPtr cmdPtr = Native.get_READ_STORED_VALUE_COMMAND();
                    Marshal.Copy(cmdPtr, _readStoredValueCommand, 0, _readStoredValueLength);
                }
                return _readStoredValueCommand;
            }
        }

        private const int _readETicketLength = 13;
        private static byte[] _readETicketCommand;
        /// <summary>
        /// Command to read the active eTicket on the card.
        /// </summary>
        public static byte[] ReadETicketCommand
        {
            get
            {
                if (_readETicketCommand == null)
                {
                    _readETicketCommand = new byte[_readETicketLength];
                    IntPtr cmdPtr = Native.get_READ_E_TICKET_COMMAND();
                    Marshal.Copy(cmdPtr, _readETicketCommand, 0, _readETicketLength);
                }
                return _readETicketCommand;
            }
        }

        private const int _readHistoryLength = 13;
        private static byte[] _readHistoryCommand;
        /// <summary>
        /// Command to read the 8 most recent transactions on the card.
        /// </summary>
        public static byte[] ReadHistoryCommand
        {
            get
            {
                if (_readHistoryCommand == null)
                {
                    _readHistoryCommand = new byte[_readHistoryLength];
                    IntPtr cmdPtr = Native.get_READ_HISTORY_COMMAND();
                    Marshal.Copy(cmdPtr, _readHistoryCommand, 0, _readHistoryLength);
                }
                return _readHistoryCommand;
            }
        }

        private const int _readNextLength = 5;
        private static byte[] _readNextCommand;
        /// <summary>
        /// Reads the remaining bytes-to-be-sent if a read request returned a MoreData response.
        /// </summary>
        public static byte[] ReadNextCommand
        {
            get
            {
                if (_readNextCommand == null)
                {
                    _readNextCommand = new byte[_readNextLength];
                    IntPtr cmdPtr = Native.get_READ_NEXT_COMMAND();
                    Marshal.Copy(cmdPtr, _readNextCommand, 0, _readNextLength);
                }
                return _readNextCommand;
            }
        }

        private const int _okResponseLength = 2;
        private static byte[] _okResponse;
        /// <summary>
        /// DESFire OPERATION_OK response.
        /// </summary>
        public static byte[] OkResponse
        {
            get
            {
                if (_okResponse == null)
                {
                    _okResponse = new byte[_okResponseLength];
                    IntPtr cmdPtr = Native.get_OK_RESPONSE();
                    Marshal.Copy(cmdPtr, _okResponse, 0, _okResponseLength);
                }
                return _okResponse;
            }
        }

        private const int _errorResponseLength = 2;
        private static byte[] _errorResponse;
        /// <summary>
        /// DESFire error response.
        /// </summary>
        public static byte[] ErrorResponse
        {
            get
            {
                if (_errorResponse == null)
                {
                    _errorResponse = new byte[_errorResponseLength];
                    IntPtr cmdPtr = Native.get_ERROR_RESPONSE();
                    Marshal.Copy(cmdPtr, _errorResponse, 0, _errorResponseLength);
                }
                return _errorResponse;
            }
        }

        private const int _moreDataResponseLength = 2;
        private static byte[] _moreDataResponse;
        /// <summary>
        /// DESFire ADDTIONAL_FRAME response. Indicates that there is more data, if the caller would like to ask for it.
        /// </summary>
        public static byte[] MoreDataResponse
        {
            get
            {
                if (_moreDataResponse == null)
                {
                    _moreDataResponse = new byte[_moreDataResponseLength];
                    IntPtr cmdPtr = Native.get_MORE_DATA_RESPONSE();
                    Marshal.Copy(cmdPtr, _moreDataResponse, 0, _moreDataResponseLength);
                }
                return _moreDataResponse;
            }
        }

    }
}
