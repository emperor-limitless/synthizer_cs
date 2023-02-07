namespace Synthizer
{
    public class Context : Pausable
    {
        public DoubleProperty Gain, DefaultDistanceRef, DefaultDistanceMax, DefaultRolloff, DefaultClosenessBoost, DefaultClosenessBoostDistance;
        public Double3Property Position;
        public Double6Property Orientation;
        public IntProperty DefaultDistanceModel, DefaultPannerStrategy;
        public Context() : base(0)
        {
            synthizer.CHECKED(synthizer.syz_createContext(ref handle, 0, 0));
            Gain = new(this, Properties.Gain);
            DefaultDistanceRef = new(this, Properties.DefaultDistanceRef);
            DefaultDistanceMax = new(this, Properties.DefaultDistanceMax);
            DefaultRolloff = new(this, Properties.DefaultRolloff);
            DefaultClosenessBoost = new(this, Properties.DefaultClosenessBoost);
            DefaultClosenessBoostDistance = new(this, Properties.DefaultClosenessBoostDistance);
            Position = new(this, Properties.Position);
            Orientation = new(this, Properties.Orientation);
            DefaultDistanceModel = new(this, Properties.DefaultDistanceModel);
            DefaultPannerStrategy = new(this, Properties.DefaultPannerStrategy);
        }
    }
}
