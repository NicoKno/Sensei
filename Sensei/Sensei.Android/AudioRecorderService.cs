using Android.Media;
using Sensei.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AudioRecorderService))]
namespace Sensei.Droid
{
    public class AudioRecorderService : IAudioRecorderService
    {
        private readonly MediaRecorder _recorder;

        public AudioRecorderService()
        {
            _recorder = new MediaRecorder();

            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
            _recorder.SetAudioEncoder(AudioEncoder.AmrNb);
            _recorder.SetOutputFile(OutputFilePath);

            _recorder.Prepare();
        }

        public void Start()
        {
            _recorder.Start();
        }

        public void Stop()
        {
            _recorder.Stop();
            _recorder.Reset();
        }

        public void Play()
        {
            throw new System.NotImplementedException();
        }

        public string OutputFilePath => "/sdcard/audioFile.3gp";
        public bool OutputFileExists => System.IO.File.Exists(OutputFilePath);

        public int MaxAmplitude => _recorder.MaxAmplitude;
    }
}