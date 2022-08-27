using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Helper.Dto.Response
{
    public class AppResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
    public class AppResponseCodes
    {

    }

    public class ActivityLogActions
    {
        public const string Login = "User Login";
    }
    public class BillsPaymentResponseCodes
    {
        public const int Success = 200;
        public const string Reversal = "119";
        public const string Successful = "000";
        public const string PendingStatus = "116";
    }
    public class HttpResponseCodes
    {
        public const int Success = 200;
        public const int BadRequest = 400;
        public const int RecordNotFound = 404;
        public const int DuplicateRecord = 409;
        public const int InternalError = 500;
    }
}
