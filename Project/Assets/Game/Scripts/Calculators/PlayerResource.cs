using System.Collections;
using System.Collections.Generic;
using NSubstitute.Routing.Handlers;
using UnityEngine;

namespace Game {

    internal abstract class PlayerResource : MonoBehaviour {

        [SerializeField] public FloatResourceRx ReactiveResource;

    }
}
