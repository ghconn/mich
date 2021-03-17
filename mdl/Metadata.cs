using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl.video
{
    public class Metadata
    {
        public TimeSpan Duration { get; }
        public Video VideoData { get; }
        public Audio AudioData { get; }

        public class Video
        {
            public string Format { get; }
            public string ColorModel { get; }
            public string FrameSize { get; }
            public int? BitRateKbs { get; }
            public double Fps { get; }
        }
        public class Audio
        {
            public string Format { get; }
            public string SampleRate { get; }
            public string ChannelOutput { get; }
            public int BitRateKbs { get; }
        }
    }
}
