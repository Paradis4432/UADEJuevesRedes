using System;
using UnityEngine;

namespace Tools.Serdes.Impls {
    public class Vector3IntSerdes : ISerdes<Vector3Int, int[]> {
        private static readonly Vector3IntSerdes Impl = new();

        public int[] Serialize(Vector3Int f) {
            return new[] {
                f.x,
                f.y,
                f.z
            };
        }

        public Vector3Int Deserialize(int[] t) {
            if (t.Length != 3) throw new ArgumentException("invalid t size");
            return new Vector3Int(
                t[0],
                t[1],
                t[2]
            );
        }

        // TODO abstract
        public static Vector3IntSerdes GetImpl() {
            return Impl;
        }
    }
}