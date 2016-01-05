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

        bool IfBandExists(string name);

        void AddBand(AddBandViewModel model);

        BandPanelViewModel GetBandPanelViewModelByName(string name);

        BandProfileViewModel GetBandProfileViewModel(string BandName);
        bool IsVisitorBandOwner(string visitor, int bandId);
        void EditBandProfile(BandProfileViewModel model);

        BandAlbumListViewModel GetBandAlbums(string BandName);

        void AddAlbum(AddAlbumViewModel model);

        AlbumViewModel GetAlbumViewModelByName(string AlbumName);

        bool IsVisitorAlbumOwner(string visitor, string AlbumName);

        void DeleteAlbum(string AlbumName);

        UpdateAlbumViewModel GetUpdateAlbumViewModel(string AlbumName);
    }
}
