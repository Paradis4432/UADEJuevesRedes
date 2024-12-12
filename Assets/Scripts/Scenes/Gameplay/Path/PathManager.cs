using System.Collections.Generic;
using Attributes;
using Tools;
using UnityEngine;

namespace Scenes.Gameplay.Path {
    public class PathManager : MonoBehaviour {
        [Ignore] private static readonly List<Transform> Points = new();
        [SerializeField] private Transform path;

        private void Start() {
            Debug.Log(Points.Count);
            if (Points.Count > 0) return;
            Reflection.ValidateClassFields(this);

            foreach (Transform o in path) Points.Add(o);
        }

        public static Transform GetNext(Transform target) {
            int i = Points.IndexOf(target);
            if (i == -1 || i >= Points.Count - 1)
                return target;
            return Points[i + 1];
        }

        public static Transform GetNext(Transform target, Vector3 pos) {
            int i = Points.IndexOf(target);
            if (i == -1 || i >= Points.Count - 1)
                return target;

            return GeneralUtils.Distance(pos, target.position) <= 0.05f ? Points[i + 1] : target;
        }

        public static Transform GetFirst() {
            return Points[0];
        }

        public static bool IsLast(Transform target, Vector3 p) {
            return Points[Points.IndexOf(target)].position == p;
        }
    }
}