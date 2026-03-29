using System.Collections.Generic;

/// <summary>
/// Reusable
/// </summary>
[System.Serializable]
public class DebugMenu
{
    public Dictionary<DebugEntryKEY, bool> dict = new Dictionary<DebugEntryKEY, bool>();

    public DebugMenu(List<DebugEntry> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            dict.Add(list[i].key, list[i].value);
        }
    }
}