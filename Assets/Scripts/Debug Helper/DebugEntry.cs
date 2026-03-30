
/// <summary>
/// Reusable
/// </summary>
[System.Serializable]
public class DebugEntry
{
    public DebugEntryKEY key;
    public bool value;

    public DebugEntry(DebugEntryKEY key)
    {
        this.key = key;
    }
}