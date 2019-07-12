﻿using System;
using System.Collections.Generic;

namespace MainApp.EF.DatabaseFirst
{
    public partial class Words
    {
        public Words()
        {
            CachedWords = new HashSet<CachedWords>();
        }

        public int Id { get; set; }
        public string Word { get; set; }

        public ICollection<CachedWords> CachedWords { get; set; }
    }
}
