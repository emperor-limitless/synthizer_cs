namespace Synthizer
{
    public class Generator : Pausable
    {
        public DoubleProperty Gain, PitchBend;
        public Generator(ulong handle) : base(handle)
        {
            Gain = new(this, Properties.Gain);
            PitchBend = new(this, Properties.PitchBend);
        }
    }
    public class BufferGenerator : Generator
    {
        public ObjectProperty Buffer;
        public DoubleProperty PlaybackPosition;
        public BoolProperty Looping;
        public BufferGenerator(Context ctx) : base(0)
        {
            synthizer.CHECKED(synthizer.syz_createBufferGenerator(ref handle, ctx.GetHandle(), 0, 0, 0));
            Buffer = new(this, Properties.Buffer);
            PlaybackPosition = new(this, Properties.PlaybackPosition);
            Looping = new(this, Properties.Looping);
        }
    }
    public class StreamingGenerator : Generator
    {
        public BoolProperty Looping;
        public DoubleProperty PlaybackPosition;
        public StreamingGenerator() : base(0)
        {
            Looping = new(this, Properties.Looping);
            PlaybackPosition = new(this, Properties.PlaybackPosition);
        }
        public void FromFile(Context ctx, string path)
        {
            synthizer.CHECKED(synthizer.syz_createStreamingGeneratorFromFile(ref handle, ctx.GetHandle(), path, 0, 0, 0));
        }
        public void FromStreamHandle(Context ctx, StreamHandle sh)
        {
            synthizer.CHECKED(synthizer.syz_createStreamingGeneratorFromStreamHandle(ref handle, ctx.GetHandle(), sh.GetHandle(), 0, 0, 0));
        }
    }
}
