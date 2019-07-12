using System;
using System.Collections.Generic;

namespace MainApp.EF.DatabaseFirst
{
    public partial class UserLog
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string Word { get; set; }
        public DateTime? SearchTime { get; set; }
    }
}
