using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Intefaces
{
    public interface IConcertService
    {

        Common.ViewModels.Concert.BandConcertListViewModel GetBandConcertListViewModel(string BandName);
    }
}
