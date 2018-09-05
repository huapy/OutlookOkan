﻿using CsvHelper.Configuration;

namespace OutlookOkan.Types
{
    public class AutoCcBccKeyword
    {
        public string Keyword { get; set; }
        public CcOrBcc CcOrBcc { get; set; }
        public string AutoAddAddress { get; set; }
    }

    public sealed class AutoCcBccKeywordMap : ClassMap<AutoCcBccKeyword>
    {
        public AutoCcBccKeywordMap()
        {
            Map(m => m.Keyword).Index(0);
            Map(m => m.CcOrBcc).Index(1);
            Map(m => m.AutoAddAddress).Index(2);
        }
    }
}