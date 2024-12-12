using System;
using System.Collections.Generic;
using System.Linq;
using Scenes.Gameplay.Enemies;
using Tools;
using UnityEngine;

namespace Scenes.Gameplay.Repo {
    public abstract class EnemyRepository : AbstractRepository<Enemy> {
        public static Enemy FindNearestOf(GameObject o, float r) {
            float cf = float.MaxValue;

            Enemy c = FindBy(e => {
                float d = GeneralUtils.Distance(e.transform.position, o.transform.position);
                if (r < d || !(d <= cf)) return false;
                cf = d;
                return true;
            });

            return c;
        }

    }
}