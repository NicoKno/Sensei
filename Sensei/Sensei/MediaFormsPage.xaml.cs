using System;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sensei
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaFormsPage : ContentPage
    {
        private SpeechRecognition _speechRecognition = new SpeechRecognition();

        private IPlaybackController _playbackController => CrossMediaManager.Current.PlaybackController;

        public MediaFormsPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _playbackController.Play();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var recordedText = await _speechRecognition.Recognise();

            LabelSpoken.Text = $"Spoken: {recordedText}";
        }
    }
}