using UnityEngine;

namespace Game {

    internal sealed class PlayerSpeed : MonoBehaviour {

        [SerializeField] internal float VerySlow = 1.0f;
        [SerializeField] internal float Slow = 1.25f;
        [SerializeField] internal float Neutral = 1.5f;
        [SerializeField] internal float Fast = 1.75f;
        [SerializeField] internal float VeryFast = 2.0f;

    }
}
