using MusicCenter.Common.ViewModels.Band;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Intefaces
{
    public interface IBandService
    {

        List<BandListItemViewModel> GetUserBandList(string email);
    }
}
