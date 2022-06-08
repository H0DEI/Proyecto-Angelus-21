using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MyExtensions
{
    public static void ShuffleRange<Habilidad>(this List<KeyValuePair<Habilidad, bool>> list, int startIndex, int count)
    {
        int n = startIndex + count;
        int limit = startIndex + 1;
        while (n > limit)
        {
            n--;
            int k = Random.Range(startIndex, n + 1);
            KeyValuePair<Habilidad, bool> value = list.ElementAt(k);
            list[k] = list.ElementAt(n);
            list[n] = value;
        }
    }
}
