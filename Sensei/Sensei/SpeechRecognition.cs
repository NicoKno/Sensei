using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Cognitive.BingSpeech;
using Plugin.AudioRecorder;

namespace Sensei
{
    class SpeechRecognition
    {
        public const string SubscriptionKey = "42aac792fb984dd9b4d76a309ad62dd0";
        private BingSpeechApiClient _bingSpeech;
        private AudioRecorderService _recorder;

        public SpeechRecognition()
        {
            _bingSpeech = new BingSpeechApiClient(SubscriptionKey);
            _recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true,
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15)
            };
        }

        public async Task<string> Recognise()
        {
            try
            {
                var audioRecordTask = await _recorder.StartRecording();

                using (var audioStream = _recorder.GetAudioFileStream())
                {
                    var speechResult = await _bingSpeech.SpeechToTextSimple(audioStream, _recorder.AudioStreamDetails.SampleRate, audioRecordTask);

                    return speechResult.DisplayText;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return string.Empty;
        }
    }
}
