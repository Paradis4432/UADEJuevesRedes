using System.Collections.Generic;
using Scenes.Gameplay.Towers;

namespace Scenes.Gameplay.Repo {
    public class TowerRepository /*: AbstractRepository<Tower>*/ {
        // es necesario tener un index, abstract repo no soporta esto
        public static readonly Dictionary<int, Tower> Towers = new();
    }
}