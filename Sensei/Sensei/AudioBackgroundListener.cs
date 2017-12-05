using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sensei
{
    public class AudioBackgroundListener
    {
        private readonly IAudioRecorderService _recorder = DependencyService.Get<IAudioRecorderService>();

        public void StartListener()
        {
            _recorder.Start();


        }


    }
}
