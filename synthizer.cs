using System;
using System.Runtime.InteropServices;

namespace Synthizer
{
    [Serializable]
    public class SynthizerError : Exception
    {
        string message;
        int code;
        public SynthizerError(string message, int code) : base(String.Format("SynthizerError: {0}[{1}]", message, code))
        {
            this.message = message;
            this.code = code;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct syz_LibraryConfig
    {
        public uint logLevel;
        public uint loggingBackend;
        public String libsndfile_path;
    }
    public enum SyzLoggingBackend
    {
        NONE,
        STDERR,
    }
    public enum SyzLogLevel
    {
        error = 0,
        warn = 10,
        info = 20,
        debug = 30,
    }
    public enum SYZ_OBJECT_TYPE
    {
        SYZ_OTYPE_CONTEXT,
        SYZ_OTYPE_BUFFER,
        SYZ_OTYPE_BUFFER_GENERATOR,
        SYZ_OTYPE_STREAMING_GENERATOR,
        SYZ_OTYPE_NOISE_GENERATOR,
        SYZ_OTYPE_DIRECT_SOURCE,
        SYZ_OTYPE_ANGULAR_PANNED_SOURCE,
        SYZ_OTYPE_SCALAR_PANNED_SOURCE,
        SYZ_OTYPE_SOURCE_3D,
        SYZ_OTYPE_GLOBAL_ECHO,
        SYZ_OTYPE_GLOBAL_FDN_REVERB,
        SYZ_OTYPE_STREAM_HANDLE,
        SYZ_OTYPE_AUTOMATION_BATCH,
        SYZ_OTYPE_FAST_SINE_BANK_GENERATOR,
    }

    public enum SYZ_PANNER_STRATEGY
    {
        SYZ_PANNER_STRATEGY_DELEGATE,
        SYZ_PANNER_STRATEGY_HRTF,
        SYZ_PANNER_STRATEGY_STEREO,
        SYZ_PANNER_STRATEGY_COUNT,
    }

    /*
     * Distance models, modeled after the WebAudio spec.
     * */
    public enum SYZ_DISTANCE_MODEL
    {
        SYZ_DISTANCE_MODEL_NONE,
        SYZ_DISTANCE_MODEL_LINEAR,
        SYZ_DISTANCE_MODEL_EXPONENTIAL,
        SYZ_DISTANCE_MODEL_INVERSE,
        SYZ_DISTANCE_MODEL_COUNT,
    }

    public enum SYZ_NOISE_TYPE
    {
        SYZ_NOISE_TYPE_UNIFORM,
        SYZ_NOISE_TYPE_VM,
        SYZ_NOISE_TYPE_FILTERED_BROWN,
        SYZ_NOISE_TYPE_COUNT,
    }

    public enum SYZ_PROPERTIES
    {
        SYZ_P_AZIMUTH,
        SYZ_P_BUFFER,
        SYZ_P_ELEVATION,
        SYZ_P_GAIN,
        SYZ_P_DEFAULT_PANNER_STRATEGY,
        SYZ_P_PANNING_SCALAR,
        SYZ_P_PLAYBACK_POSITION,
        SYZ_P_POSITION,
        SYZ_P_ORIENTATION,
        SYZ_P_CLOSENESS_BOOST,
        SYZ_P_CLOSENESS_BOOST_DISTANCE,
        SYZ_P_DISTANCE_MAX,
        SYZ_P_DISTANCE_MODEL,
        SYZ_P_DISTANCE_REF,
        SYZ_P_ROLLOFF,
        SYZ_P_DEFAULT_CLOSENESS_BOOST,
        SYZ_P_DEFAULT_CLOSENESS_BOOST_DISTANCE,
        SYZ_P_DEFAULT_DISTANCE_MAX,
        SYZ_P_DEFAULT_DISTANCE_MODEL,
        SYZ_P_DEFAULT_DISTANCE_REF,
        SYZ_P_DEFAULT_ROLLOFF,
        SYZ_P_LOOPING,
        SYZ_P_NOISE_TYPE,
        SYZ_P_PITCH_BEND,
        SYZ_P_INPUT_FILTER_ENABLED,
        SYZ_P_INPUT_FILTER_CUTOFF,
        SYZ_P_MEAN_FREE_PATH,
        SYZ_P_T60,
        SYZ_P_LATE_REFLECTIONS_LF_ROLLOFF,
        SYZ_P_LATE_REFLECTIONS_LF_REFERENCE,
        SYZ_P_LATE_REFLECTIONS_HF_ROLLOFF,
        SYZ_P_LATE_REFLECTIONS_HF_REFERENCE,
        SYZ_P_LATE_REFLECTIONS_DIFFUSION,
        SYZ_P_LATE_REFLECTIONS_MODULATION_DEPTH,
        SYZ_P_LATE_REFLECTIONS_MODULATION_FREQUENCY,
        SYZ_P_LATE_REFLECTIONS_DELAY,
        SYZ_P_FILTER,
        SYZ_P_FILTER_DIRECT,
        SYZ_P_FILTER_EFFECTS,
        SYZ_P_FILTER_INPUT,
        SYZ_P_CURRENT_TIME,
        SYZ_P_SUGGESTED_AUTOMATION_TIME,
        SYZ_P_FREQUENCY,
    }

    public enum SYZ_EVENT_TYPES
    {
        /* Internal detail: Invalid must always be 0. */
        SYZ_EVENT_TYPE_INVALID,
        SYZ_EVENT_TYPE_LOOPED,
        SYZ_EVENT_TYPE_FINISHED,
        SYZ_EVENT_TYPE_USER_AUTOMATION,
    }

    public enum SYZ_INTERPOLATION_TYPES
    {
        SYZ_INTERPOLATION_TYPE_NONE,
        SYZ_INTERPOLATION_TYPE_LINEAR,
    }

    public enum SYZ_AUTOMATION_COMMANDS
    {
        SYZ_AUTOMATION_COMMAND_APPEND_PROPERTY,
        SYZ_AUTOMATION_COMMAND_SEND_USER_EVENT,
        SYZ_AUTOMATION_COMMAND_CLEAR_PROPERTY,
        SYZ_AUTOMATION_COMMAND_CLEAR_EVENTS,
        SYZ_AUTOMATION_COMMAND_CLEAR_ALL_PROPERTIES,
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct syz_DeleteBehaviorConfig
    {
        public int linger;
        public double lingerTimeout;
    }
    public static class synthizer
    {
        public static void CHECKED(int ret)
        {
            if (ret != 0)
            {
                throw new SynthizerError(syz_getLastErrorMessage(), ret);
            }
        }
        public static void initialize(LogLevel ll = LogLevel.error, LoggingBackend lb = LoggingBackend.NONE, string libsndfile_path = "")
        {
            syz_LibraryConfig cfg = new();
            synthizer.syz_libraryConfigSetDefaults(ref cfg);
            cfg.logLevel = (uint)ll;
            cfg.loggingBackend = (uint)lb;
            if (libsndfile_path != "")
                cfg.libsndfile_path = libsndfile_path;
            synthizer.CHECKED(synthizer.syz_initializeWithConfig(ref cfg));
        }
        public static void shutdown() => synthizer.syz_shutdown();
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern void syz_libraryConfigSetDefaults(ref syz_LibraryConfig cfg);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_initialize();
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_initializeWithConfig(ref syz_LibraryConfig cfg);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_shutdown();
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern void syz_initDeleteBehaviorConfig(ref syz_DeleteBehaviorConfig cfg);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_configDeleteBehavior(ulong handle, ref syz_DeleteBehaviorConfig cfg);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_handleDecRef(ulong handle);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_handleIncRef(ulong handle);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern string syz_getLastErrorMessage();
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createContext(ref ulong handle, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createStreamHandleFromMemory(ref ulong handle, ulong dataLength, byte[] data, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createStreamHandleFromFile(ref ulong handle, string path, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createStreamingGeneratorFromFile(ref ulong handle, ulong context, string path, nint config, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createStreamingGeneratorFromStreamHandle(ref ulong handle, ulong context, ulong streamHandle, nint config, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createBufferFromEncodedData(ref ulong handle, ulong dataLength, byte[] data, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createBufferFromFile(ref ulong handle, string path, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createBufferGenerator(ref ulong handle, ulong context, nint config, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_sourceAddGenerator(ulong source, ulong generator);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_setI(ulong handle, int property, int value);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_getI(ref int outRef, ulong handle, int property);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_setD(ulong handle, int property, double value);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_getD(ref double outRef, ulong handle, int property);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_setO(ulong handle, int property, ulong value);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_getD3(ref double x, ref double y, ref double z, ulong handle, int property);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_setD3(ulong handle, int property, double x, double y, double z);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_getD6(ref double x1, ref double y1, ref double z1, ref double x2, ref double y2, ref double z2, ulong handle, int property);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_setD6(ulong handle, int property, double x1, double y1, double z1, double x2, double y2, double z2);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_play(ulong handle);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_pause(ulong handle);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createDirectSource(ref ulong outRef, ulong context, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createAngularPannedSource(ref ulong outRef, ulong context, int pannerStrategy, double azimuth, double elevation, nint config, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createScalarPannedSource(ref ulong outRef, ulong context, int pannerStrategy, double panningScalar, nint config, nint userdata, nint freeCallback);
        [DllImport("synthizer", CallingConvention = CallingConvention.Cdecl)]
        public static extern int syz_createSource3D(ref ulong outRef, ulong context, int pannerStrategy, double x, double y, double z, nint config, nint userdata, nint freeCallback);
    }
}
