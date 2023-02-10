using System.Collections.Generic;
using UnityEngine;

namespace Utills
{
    public static class Utill
    {
        public static T GetRandom<T>(this List<T> list)
        {
            var index = Random.Range(0, list.Count);
            return list[index];
        }
    }
}
