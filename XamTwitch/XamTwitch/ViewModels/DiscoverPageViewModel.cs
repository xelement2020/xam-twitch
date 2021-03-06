﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.MobCAT;
using Microsoft.MobCAT.MVVM;
using XamTwitch.Models;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class DiscoverPageViewModel : BaseNavigationViewModel
    {
        private readonly ITwitchHttpService _twitchHttpService;

        private ObservableCollection<TwitchStream> _streams;
        public ObservableCollection<TwitchStream> Streams
        {
            get => _streams;
            set => RaiseAndUpdate(ref _streams, value);
        }

        private TwitchStream _selectedStream;
        public TwitchStream SelectedStream
        {
            get => _selectedStream;
            set
            {
                if (RaiseAndUpdate(ref _selectedStream, value) && value != null)
                {
                    var playerViewModel = new PlayerPageViewModel();
                    playerViewModel.Stream = value;
                    this.Navigation.PushAsync(playerViewModel);
                }
            }
        }

        public DiscoverPageViewModel()
        {
            _twitchHttpService = ServiceContainer.Resolve<ITwitchHttpService>();
        }

        public override async Task InitAsync()
        {
            var gamesResult = await _twitchHttpService.GetTwitchStreamsAsync();
            Streams = new ObservableCollection<TwitchStream>(gamesResult.Data);
        }
    }
}
