using System.Collections.Generic;
using UnityEngine;

namespace OBNet {

    /// <summary>
    /// Utility script that unpacks the network objects into the root of the 
    /// scene for Don't Destroy On Load.
    /// </summary>
    internal class OBNetworkUnpacker : MonoBehaviour {

        void Awake() {
            int count = transform.childCount;

            Transform[] objects = new Transform[count];

            // List objects
            for(int i=0; i<transform.childCount; i++) {
                Transform gameObjectTransform = transform.GetChild(i);
                objects[i] = gameObjectTransform;
            }

            // Unpack objects
            foreach (var go in objects) {
                if (go != null) {
                    go.SetParent(null, false);
                }
            }

            // Destroy unpacker
            Destroy(gameObject);
        }

    }

}