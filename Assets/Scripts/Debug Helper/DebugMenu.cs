using System.Collections.Generic;

/// <summary>
/// Reusable
/// 
/// Role?
/// For now, this script is to debug the specific part in the code for the player.
/// In the future, this script should be able to use with other entity beside player.
/// 
/// How?
/// We want to create dictionary to debug each part of the code.
/// By making menu of debug in the inspector, we can checked/unchecked the box we want to debug specifically.
/// The said box will be using the dict's key as the name. 
/// So we will assign the proper name to the key so developer have easier time debug the code.  
/// 
/// Benefit?
/// So we don't have to comeback and comment/uncomment the debug's line of code forever. 
/// 
/// Key's name Example?
/// "IsCCGrounded" => debug if the CharacterController.isGrounded() is working properly.
/// Look at DebugEntryKey for more info.
/// 
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

    public bool IsDebugEnabled(DebugEntryKEY key)
    {
        bool isEnabled;
        return dict.TryGetValue(key, out isEnabled) && isEnabled;
    }
}