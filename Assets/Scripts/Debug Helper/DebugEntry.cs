
/// <summary>
/// Reusable
/// </summary>
namespace DebugMenu
{
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
}
