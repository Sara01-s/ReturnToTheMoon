using UnityEngine;
using UniRx;

namespace Game {
    
    internal sealed class PlayerHealth : PlayerResource { 
       
        [SerializeField] internal float MaxHealth;

        private void Awake() {
            ReactiveResource.Value = MaxHealth;
        }

        private void OnEnable() {
            ReactiveResource.Observable.Subscribe(Die);
        }
		
        private void OnDisable() {
            ReactiveResource.Observable.Dispose();
        }

        private void Die(float health) {
            if (health > 0.1f) return;
            // TODO: Death Logic, reset level...
        }

    }
}
