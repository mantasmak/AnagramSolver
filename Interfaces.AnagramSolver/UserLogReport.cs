using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class UserLogReport
    {
        public string UserIp { get; set; }
        public DateTime SearchTime { get; set; }
        public string Word { get; set; }
        public List<string> Anagrams { get; set; }
    }
}
