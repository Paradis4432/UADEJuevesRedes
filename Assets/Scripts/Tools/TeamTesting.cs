using Teams;
using UnityEngine;

namespace Tools {
    public class TeamTesting : MonoBehaviour {
        public ETeam Team;

        private void Awake() {
#if UNITY_EDITOR
            PlayerTeamManager.SaveTeam(Team);
#endif
        }
    }
}