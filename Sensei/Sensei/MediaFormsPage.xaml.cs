using System;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Xamarin.Forms;

namespace Sensei
{
    public partial class MediaFormsPage : ContentPage
    {
        private IPlaybackController _playbackController => CrossMediaManager.Current.PlaybackController;
        private readonly IAudioRecorderService _recorder = DependencyService.Get<IAudioRecorderService>();

        private bool _isRecording;

        public MediaFormsPage()
        {
            InitializeComponent();
        }

        private void PlayButtonClicked(object sender, EventArgs e)
        {
            if (!_isRecording)
            {
                _playbackController.Play();

                _isRecording = true;
                _recorder.Start();

                ButtonPlay.Text = "Stop Training";

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    LabelSpoken.Text = $"Amplitude: {_recorder.MaxAmplitude}";
                    return true;
                });

            }
            else
            {
                _recorder.Stop();
                ButtonRecord.Text = "Record";

                LabelSpoken.Text = $"FileExists: {_recorder.OutputFileExists}";
            }
        }

        
    }
}