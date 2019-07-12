using System;
using System.Collections.Generic;

namespace MainApp.EF.DatabaseFirst
{
    public partial class CachedWords
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int? AnagramId { get; set; }

        public Words Anagram { get; set; }
    }
}
