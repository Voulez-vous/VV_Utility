namespace VV.Scoring.Data
{
    /// <summary>
    /// ISerializedValue classes are a set of class to allow Editor SerializedReference usages.
    /// Used for editor tools.
    /// </summary>
    public interface ISerializedValue
    {
        object GetValue();
    }
}