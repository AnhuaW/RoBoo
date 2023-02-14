using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[InitializeOnLoad]
class EECS494HelperProjectSetup
{
    static void RegisterModuleDefine(string symbol)
    {
        symbol = symbol.ToUpperInvariant().Trim();
        string current_defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(
                            EditorUserBuildSettings.selectedBuildTargetGroup
                         );
        HashSet<string> current_defines_set = new HashSet<string>(current_defines.Split(';'));
        current_defines_set.Add(symbol);
        current_defines = "";
        foreach (string define in current_defines_set)
            current_defines += define + ";";

        PlayerSettings.SetScriptingDefineSymbolsForGroup(
            EditorUserBuildSettings.selectedBuildTargetGroup,
            current_defines
        );
    }

    static EECS494HelperProjectSetup()
    {
        RegisterModuleDefine("EECS494Helper");
        EditorApplication.update += EditorUpdate;
    }

    static void EditorUpdate()
    {
        EnforcePrefabExistsInScene("EECS494Helper");
    }

    static void EnforcePrefabExistsInScene(string name)
    {
        if (Application.isPlaying)
            return;

        if (GameObject.Find(name) == null)
        {
            GameObject application_prefab = Resources.Load<GameObject>(name);
            if (application_prefab == null)
            {
                Debug.LogError("Scene-Critical prefab could not be loaded: [" + name + "]");
                return;
            }

            GameObject new_required_object = PrefabUtility.InstantiatePrefab(application_prefab) as GameObject;
            new_required_object.name = name;
        }
    }
}
