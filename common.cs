namespace Synthizer
{
    public class SynthizerObject
    {
        internal ulong handle;
        internal SynthizerObject(ulong handle)
        {
            this.handle = handle;
        }
        public void Destroy()
        {
            synthizer.CHECKED(synthizer.syz_handleDecRef(handle));
        }
        public ulong GetHandle()
        {
            return handle;
        }
        public void ConfigDeleteBehavior(bool linger, double lingerTimeout)
        {
            syz_DeleteBehaviorConfig cfg = new();
            cfg.linger = linger ? 1 : 0;
            cfg.lingerTimeout = lingerTimeout;
            synthizer.syz_initDeleteBehaviorConfig(ref cfg);
            synthizer.CHECKED(synthizer.syz_configDeleteBehavior(handle, ref cfg));
        }
    }
    public class Pausable : SynthizerObject
    {
        public DoubleProperty CurrentTime;
        public DoubleProperty SuggestedAutomationTime;
        internal Pausable(ulong handle) : base(handle)
        {
            CurrentTime = new(this, Properties.currentTime);
            SuggestedAutomationTime = new(this, Properties.SuggestedAutomationTime);
        }
        public void Play()
        {
            synthizer.CHECKED(synthizer.syz_play(GetHandle()));
        }
        public void Pause()
        {
            synthizer.CHECKED(synthizer.syz_pause(GetHandle()));
        }
    }
}
