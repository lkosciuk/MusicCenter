using MusicCenter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.Extensions
{
    public static class EnumExt
    {
        public static string ShowResourcesString(this SearchCategory type)
        {
            switch (type)
            {
                case SearchCategory.Bands:
                    return Resources.Global.Bands;
                case SearchCategory.Albums:
                    return Resources.Global.Albums;
                case SearchCategory.Songs:
                    return Resources.Global.Songs;
                case SearchCategory.Concerts:
                    return Resources.Global.Concerts;
                default:
                    return "";
            }
        }
    }
}
