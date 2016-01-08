using MusicCenter.Common.ViewModels.Concert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Intefaces
{
    public interface IConcertService
    {

        BandConcertListViewModel GetBandConcertListViewModel(string BandName);

        BandConcertViewModel GetBandConcertViewModel(string BandName);
    }
}
