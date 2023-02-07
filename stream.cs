namespace Synthizer
{
    public class StreamHandle : SynthizerObject
    {
        public StreamHandle() : base(0) { }
        public void FromFile(string path)
        {
            synthizer.CHECKED(synthizer.syz_createStreamHandleFromFile(ref handle, path, 0, 0));
        }
        public void FromMemory(byte[] data)
        {
            ulong count = (ulong)data.Length;
            synthizer.CHECKED(synthizer.syz_createStreamHandleFromMemory(ref handle, count, data, 0, 0));
        }
    }
}
