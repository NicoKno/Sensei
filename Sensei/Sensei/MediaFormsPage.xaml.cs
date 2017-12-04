using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Xamarin.Forms;

namespace Sensei
{
    public partial class MediaFormsPage : ContentPage
    {

        private IPlaybackController _playbackController => CrossMediaManager.Current.PlaybackController;
        private readonly IAudioRecorderService _recorder = DependencyService.Get<IAudioRecorderService>();

        public MediaFormsPage()
        {
            InitializeComponent();
        }

        private void PlayButtonClicked(object sender, EventArgs e)
        {
            _playbackController.Play();
        }

        private bool _isRecording = false;

        private void StartRecordingClicked(object sender, EventArgs e)
        {
            if (!_isRecording)
            {
                _isRecording = true;
                _recorder.Start();
                ButtonRecord.Text = "Stop Recording";
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