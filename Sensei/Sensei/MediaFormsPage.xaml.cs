using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.EventArguments;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Sensei
{
    public partial class MediaFormsPage : ContentPage
    {
        private IPlaybackController _playbackController => CrossMediaManager.Current.PlaybackController;
        private IMediaManager _mediaManager = CrossMediaManager.Current;
        private readonly IAudioRecorderService _recorder = DependencyService.Get<IAudioRecorderService>();
        private readonly IVideoSourceProvider _videoSource = DependencyService.Get<IVideoSourceProvider>();

        private readonly Queue<VideoStep> _queue = new Queue<VideoStep>();

        private bool _isRecording;

        public MediaFormsPage()
        {
            InitializeComponent();

            var videoPath = _videoSource.GetVideoSource("Heian_Shodan.mp4");

            VideoView.Source = videoPath;

            _mediaManager.PlayingChanged += MediaManagerOnPlayingChanged;

            _queue.Enqueue(new VideoStep { StopLow = 15850, StopHigh = 16500 });
            _queue.Enqueue(new VideoStep { StopLow = 17220, StopHigh = 17995 });
            _queue.Enqueue(new VideoStep { StopLow = 18997, StopHigh = 19555 });
            _queue.Enqueue(new VideoStep { StopLow = 20285, StopHigh = 20758 });
            _queue.Enqueue(new VideoStep { StopLow = 21593, StopHigh = 22323 });
        }

        private async void MediaManagerOnPlayingChanged(object sender, PlayingChangedEventArgs playingChangedEventArgs)
        {
            LabelTime.Text = $"Time: {playingChangedEventArgs.Position.TotalSeconds / 1000}";

            if (!_queue.Any())
            {
                return;
            }

            if (playingChangedEventArgs.Position.TotalSeconds >= _queue.Peek().StopLow && playingChangedEventArgs.Position.TotalSeconds <= _queue.Peek().StopHigh)
            {
                _queue.Dequeue();
                await _playbackController.PlayPause();
            }
            else if (playingChangedEventArgs.Position.TotalSeconds >= _queue.Peek().StopHigh)
            {
                await _playbackController.SeekTo(_queue.Peek().StopLow / 1000);

                _queue.Dequeue();

                await _playbackController.PlayPause();
            }
        }

        private async void PlayButtonClicked(object sender, EventArgs e)
        {
            if (!_isRecording)
            {
                await _playbackController.Play();

                _isRecording = true;
                _recorder.Start();

                ButtonPlay.Text = "Stop Training";

                StartVoiceListener();

            }
            else
            {
                _recorder.Stop();

                await _playbackController.Stop();

                _isRecording = false;
                _recorder.Stop();

                ButtonPlay.Text = "Start Training";

                LabelSpoken.Text = $"FileExists: {_recorder.OutputFileExists}";
            }
        }

        private void StartVoiceListener()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                if (_recorder.MaxAmplitude > 10000)
                {
                    _playbackController.PlayPause();
                }

                LabelSpoken.Text = $"Amplitude: {_recorder.MaxAmplitude}";
                return true;
            });
        }
    }
}