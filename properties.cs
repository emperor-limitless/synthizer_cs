using System;
namespace Synthizer
{
    public class PropertyBase
    {
        internal SynthizerObject instance;
        internal Properties property;
        public PropertyBase(SynthizerObject i, Properties p)
        {
            instance = i;
            property = p;
        }
    }
    public class BoolProperty : PropertyBase
    {
        public BoolProperty(SynthizerObject i, Properties p) : base(i, p) { }
        public bool Value
        {
            get
            {
                int temp = 0;
                synthizer.CHECKED(synthizer.syz_getI(ref temp, instance.GetHandle(), (int)property));
                return temp == 1;
            }
            set
            {
                synthizer.CHECKED(synthizer.syz_setI(instance.GetHandle(), (int)property, value ? 1 : 0));
            }
        }
    }
    public class IntProperty : PropertyBase
    {
        public IntProperty(SynthizerObject i, Properties p) : base(i, p) { }
        public int Value
        {
            get
            {
                int temp = 0;
                synthizer.CHECKED(synthizer.syz_getI(ref temp, instance.GetHandle(), (int)property));
                return temp;
            }
            set
            {
                synthizer.CHECKED(synthizer.syz_setI(instance.GetHandle(), (int)property, value));
            }
        }
    }
    public class DoubleProperty : PropertyBase
    {
        public DoubleProperty(SynthizerObject i, Properties p) : base(i, p) { }
        public double Value
        {
            get
            {
                double temp = 0;
                synthizer.CHECKED(synthizer.syz_getD(ref temp, instance.GetHandle(), (int)property));
                return temp;
            }
            set
            {
                synthizer.CHECKED(synthizer.syz_setD(instance.GetHandle(), (int)property, value));
            }
        }
    }
    public class Double3Property : PropertyBase
    {
        public Double3Property(SynthizerObject i, Properties p) : base(i, p) { }
        public ValueTuple<double, double, double> Value
        {
            get
            {
                double x = 0, y = 0, z = 0;
                synthizer.CHECKED(synthizer.syz_getD3(ref x, ref y, ref z, instance.GetHandle(), (int)property));
                return (x, y, z);
            }
            set
            {
                synthizer.CHECKED(synthizer.syz_setD3(instance.GetHandle(), (int)property, value.Item1, value.Item2, value.Item3));
            }
        }
    }
    public class Double6Property : PropertyBase
    {
        public Double6Property(SynthizerObject i, Properties p) : base(i, p) { }
        public ValueTuple<double, double, double, double, double, double> Value
        {
            get
            {
                double x1 = 0.0, y1 = 0.0, z1 = 0.0, x2 = 0.0, y2 = 0.0, z2 = 0.0;
                synthizer.CHECKED(synthizer.syz_getD6(ref x1, ref y1, ref z1, ref x2, ref y2, ref z2, instance.GetHandle(), (int)property));
                return (x1, y1, z1, x2, y2, z2);
            }
            set
            {
                synthizer.CHECKED(synthizer.syz_setD6(instance.GetHandle(), (int)property, value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6));
            }
        }
    }
    public class ObjectProperty : PropertyBase
    {
        public ObjectProperty(SynthizerObject i, Properties p) : base(i, p) { }
        public SynthizerObject Value
        {
            set
            {
                synthizer.CHECKED(synthizer.syz_setO(instance.GetHandle(), (int)property, value.GetHandle()));
            }
        }
    }
}
