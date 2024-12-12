using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenes.Gameplay.Repo {
    public class AbstractRepository<T> {
        protected static readonly List<T> Elements = new();

        public static void Register(T e) {
            Elements.Add(e);
        }

        public static void Unregister(T e) {
            Elements.Remove(e);
        }

        public static T FindBy(Func<T, bool> pred) {
            return Elements.Where(pred.Invoke).FirstOrDefault();
        }
        
        public static void Clear() {
            Elements.Clear();
        }
        
    }
}