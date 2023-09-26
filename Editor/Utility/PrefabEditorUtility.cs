using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class PrefabEditorUtility
{
    public static string GetMenuItemKey(string menu_key, int id, string name)
    {
        return $"{menu_key}-{id}-{name}";
    }


}
