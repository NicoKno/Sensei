namespace Sensei
{
    public interface IAudioRecorderService
    {
        void Start();
        void Stop();
        void Play();
        string OutputFilePath { get; }
        bool OutputFileExists { get; }
    }
}
