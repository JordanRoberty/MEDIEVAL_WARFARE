using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A static class for Gameobject helpful methods
/// </summary>
public static class GameObjectUtils
{
    /// <summary>
    /// Destroy the gameObject linked to this transform.
    /// Use it like so:
    /// <code>
    /// transform.Destroy();
    /// </code>
    /// </summary>
    ///
    public static void destroy(this Transform t)
    {
        if (Application.isPlaying)
        {
            GameObject.Destroy(t.gameObject);
        }
        else
        {
            GameObject.DestroyImmediate(t.gameObject);
        }
    }

    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    ///
    public static void destroy_children(this Transform t)
    {
        foreach (Transform child in t) child.destroy();
    }
}

