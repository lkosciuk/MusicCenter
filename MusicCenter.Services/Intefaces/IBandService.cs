using MusicCenter.Common.RequestModels;
using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Common.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;

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

        void UpdateAlbum(UpdateAlbumViewModel model);

        BandSingleListViewModel GetBandSingleListViewModel(string BandName);

        void AddSingle(AddSingleViewModel model);

        void DeleteSingle(int SingleId);

        BandSingleViewModel GetBandSingleViewModel(int SingleId);

        void UpdateSingle(BandSingleViewModel model);

        string[] GetAllBandNames();

        List<BandsPanelViewModel> GetNewestBands();

        List<AlbumsPanelViewModel> GetNewestAlbums();

        List<SongsPanelViewModel> GetNewestSingles();

        IEnumerable<SearchViewModel> SearchBands(string query);

        IEnumerable<SearchViewModel> SearchAlbums(string query);

        IEnumerable<SearchViewModel> SearchSongs(string query);

        SearchItemDetailsViewModel GetSearchDetailsViewModel(int searchItemId, string searchItemCategory);

        PagedList<BandListItemViewModel> GetBandListByPageNuber(DataListFilterModel filter, int pageNumber);

        List<string> SearchGenreNames(string query);

        List<string> SearchBandNames(string query);

        PagedList<AlbumListItemViewModel> GetAlbumListByPageNuber(DataListFilterModel filter, int pageNumber);

        List<string> SearchAlbumNames(string query);

        PagedList<SongListItemViewModel> GetSongListByPageNuber(DataListFilterModel filter, int pageNumber);

        List<string> SearchSongNames(string query);
    }
}
