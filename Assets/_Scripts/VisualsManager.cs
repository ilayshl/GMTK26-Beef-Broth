using System.Collections.Generic;
using UnityEngine;

public class VisualsManager : Singleton<VisualsManager>
{
    private readonly HashSet<int> usedHats = new();
    private readonly HashSet<int> usedColors = new();
    private readonly HashSet<int> usedFaces = new();

    public int GetUniqueHat(int count)
    {
        return GetUniqueIndex(count, usedHats);
    }

    public int GetUniqueColor(int count)
    {
        return GetUniqueIndex(count, usedColors);
    }

    public int GetUniqueFace(int count)
    {
        return GetUniqueIndex(count, usedFaces);
    }

    private int GetUniqueIndex(int count, HashSet<int> used)
    {
        if (used.Count >= count)
            return Random.Range(0, count); // no unique choices left

        int index;
        do
        {
            index = Random.Range(0, count);
        }
        while (used.Contains(index));

        used.Add(index);
        return index;
    }

    public void ReleaseHat(int index) => usedHats.Remove(index);
    public void ReleaseColor(int index) => usedColors.Remove(index);
    public void ReleaseFace(int index) => usedFaces.Remove(index);
}