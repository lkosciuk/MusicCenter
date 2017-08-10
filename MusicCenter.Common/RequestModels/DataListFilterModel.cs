using System;
using System.Collections.Generic;

namespace MusicCenter.Common.RequestModels
{
    public class DataListFilterModel
    {
        public string Names { get; set; }
        public string GenreNames { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool HasValue
        {
            get
            {
                return (!string.IsNullOrEmpty(Names) || !string.IsNullOrEmpty(GenreNames) || DateFrom.HasValue || DateTo.HasValue);
            }
        }
    }
}
