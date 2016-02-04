﻿using MusicCenter.Common.ViewModels.Concert;
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

        UpdateConcertViewModel GetUpdateConcertViewModel(int ConcertId);

        void AddConcert(AddConcertViewModel model);

        ConcertViewModel GetConcertViewModel(int ConcertId);

        bool IsVisitorConcertOwner(string BandName, int ConcertId);

        void DeleteConcert(int ConcertId);

        void UpdateConcert(UpdateConcertViewModel model);
    }
}