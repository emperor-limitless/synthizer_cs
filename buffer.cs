namespace Synthizer
{
    public class Buffer : SynthizerObject
    {
        public Buffer() : base(0) { }
        public void FromFile(string path)
        {
            synthizer.CHECKED(synthizer.syz_createBufferFromFile(ref handle, path, 0, 0));
        }
    }
}
