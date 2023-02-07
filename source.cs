using System;
namespace Synthizer
{
    public class Source : Pausable
    {
        public DoubleProperty Gain;
        public Source() : base(0)
        {
            Gain = new(this, Properties.Gain);
        }
        public void AddGenerator(Generator gen)
        {
            synthizer.CHECKED(synthizer.syz_sourceAddGenerator(GetHandle(), gen.GetHandle()));
        }
    }
    public class DirectSource : Source
    {
        public DirectSource(Context ctx)
        {
            synthizer.CHECKED(synthizer.syz_createDirectSource(ref handle, ctx.GetHandle(), 0, 0));
        }
    }
    public class Source3D : Source
    {
        public DoubleProperty DistanceRef, DistanceMax, Rolloff, ClosenessBoost, ClosenessBoostDistance;
        public Double3Property Position;
        public Double6Property Orientation;
        public IntProperty DistanceModel;
        public Source3D(Context ctx, PannerStrategy pannerStrategy = PannerStrategy.Delegate, double x = 0.0, double y = 0.0, double z = 0.0) : base()
        {
            synthizer.CHECKED(synthizer.syz_createSource3D(ref handle, ctx.GetHandle(), (int)pannerStrategy, x, y, z, 0, 0, 0));
            ClosenessBoost = new(this, Properties.ClosenessBoost);
            ClosenessBoostDistance = new(this, Properties.ClosenessBoostDistance);
            Rolloff = new(this, Properties.Rolloff);
            DistanceRef = new(this, Properties.DistanceRef);
            DistanceMax = new(this, Properties.DistanceMax);
            Position = new(this, Properties.Position);
            Orientation = new(this, Properties.Orientation);
        }
    }
}
