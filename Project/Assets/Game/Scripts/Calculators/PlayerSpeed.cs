using UnityEngine;

namespace Game {

    internal sealed class PlayerSpeed : PlayerResource {

        [SerializeField] internal float VerySlow;
        [SerializeField] internal float Slow;
        [SerializeField] internal float Neutral;
        [SerializeField] internal float Fast;
        [SerializeField] internal float VeryFast;

        private void Awake() {
            ReactiveResource.Value = Neutral;
        }

    }
}
