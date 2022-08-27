using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Helper.Dto.Request.Account
{
    public class BiometricLoginRequestDto
    {
        public string BiometricKey { get; set; }
        public string Device_UniqueID { get; set; }
        public string Device_Name { get; set; }
        public string Device_Model { get; set; }
        public string Platform_OS { get; set; }
    }
}
