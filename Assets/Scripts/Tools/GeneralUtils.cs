using System;
using UnityEngine;

namespace Tools {
    public abstract class GeneralUtils {
        public static float Distance(Vector3 a, Vector3 b) {
            float num1 = a.x - b.x;
            float num3 = a.y - b.y;
            return (float)Math.Sqrt(num1 * (double)num1 + num3 * (double)num3);
        }
    }
}