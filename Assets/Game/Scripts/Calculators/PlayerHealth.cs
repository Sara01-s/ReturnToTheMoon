using UnityEngine;
using UniRx;

namespace Game {
    
    internal sealed class PlayerHealth : MonoBehaviour { // TODO
       
        internal ReactiveProperty<float> CurrentHealth; 

        private float MAX_HEALTH;

        private void Awake() {
            CurrentHealth = MAX_HEALTH;
        }

        private void OnEnable() {

        }
        private void OnDisable() {
        
        }
       
        private void Die() {

        }

    }
}
