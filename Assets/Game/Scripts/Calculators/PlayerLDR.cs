using UnityEngine;
using UniRx;

namespace Game {

    internal sealed class PlayerLDR : MonoBehaviour {
        
        [SerializeField] internal float MIN_LDR = -0.5f;
        [SerializeField] internal float BETWEEN_MIN_DEFAULT_LDR = -0.7f;
        [SerializeField] internal float DEFAULT_LDR = -1.0f;
        [SerializeField] internal float BETWEEN_MAX_DEFAULT_LDR = -1.5f;
        [SerializeField] internal float MAX_LDR = -1.7f;

    }
}
