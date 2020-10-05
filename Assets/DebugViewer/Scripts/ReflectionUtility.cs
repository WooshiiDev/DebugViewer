using System;
using System.Reflection;
using UnityEngine;
using JetBrains.Annotations;

public static class UnityReflectionUtil
    {
    /// <summary>
    /// Get all fields in the given type
    /// </summary>
    /// <param name="type">The type we need fields from</param>
    /// <param name="flags">Flags for field search</param>
    /// <returns></returns>
    public static FieldInfo[] GetFields([NotNull] Type type, BindingFlags flags = BindingFlags.Public)
        {
        //Assembly assem = type.Assembly;
        if (type == null)
            {
            Debug.LogError ("Cannot find type of null!");
            return null;
            }

        return type.GetFields (flags);
        }

    }
