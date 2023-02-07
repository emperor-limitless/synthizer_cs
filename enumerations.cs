public enum LoggingBackend
{
    NONE,
    STDERR,
}
public enum LogLevel
{
    error = 0,
    warn = 10,
    info = 20,
    debug = 30,
}
public enum ObjectType
{
    Context,
    Buffer,
    BufferGenerator,
    StreamingGenerator,
    NoiseGenerator,
    DirectSource,
    AngularPannedSource,
    ScalarPannedSource,
    Source3D,
    GlobalEcho,
    GlobalFdnReverb,
    StreamHandle,
    AutomationBatch,
    FastSineBankGenerator,
}

public enum PannerStrategy
{
    Delegate,
    Hrtf,
    Stereo,
    Count,
}

/*
 * Distance models, modeled after the WebAudio spec.
 * */
public enum DistanceModel
{
    None,
    Linear,
    Exponential,
    Inverse,
    Count,
}
public enum NoiseType
{
    Uniform,
    Vm,
    FilteredBrown,
    Count,
}

public enum Properties
{
    Azimuth,
    Buffer,
    Elevation,
    Gain,
    DefaultPannerStrategy,
    PanningScalar,
    PlaybackPosition,
    Position,
    Orientation,
    ClosenessBoost,
    ClosenessBoostDistance,
    DistanceMax,
    DistanceModel,
    DistanceRef,
    Rolloff,
    DefaultClosenessBoost,
    DefaultClosenessBoostDistance,
    DefaultDistanceMax,
    DefaultDistanceModel,
    DefaultDistanceRef,
    DefaultRolloff,
    Looping,
    NoiseType,
    PitchBend,
    InputFilterEnabled,
    InputFilterCutoff,
    MeanFreePath,
    T60,
    LateReflectionsLfRolloff,
    LateReflectionsLfReference,
    LateReflectionsHfRolloff,
    LateReflectionsHfReference,
    LateReflectionsDiffusion,
    LateReflectionsModulationDepth,
    LateReflectionsModulationFrequency,
    LateReflectionsDelay,
    Filter,
    FilterDirect,
    FilterEffects,
    FilterInput,
    currentTime,
    SuggestedAutomationTime,
    Frequency,
}

public enum EventTypes
{
    /* Internal detail: Invalid must always be 0. */
    Invalid,
    Looped,
    Finished,
    UserAutomation,
}

public enum InterpolationTypes
{
    None,
    Linear,
}

public enum AutomationCommands
{
    AppendProperty,
    SendUserEvent,
    ClearProperty,
    ClearEvents,
    ClearAllProperties,
}
