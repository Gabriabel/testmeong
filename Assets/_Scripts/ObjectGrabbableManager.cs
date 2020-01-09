using UnityEngine;
using Photon.Pun;
using OculusSampleFramework;

public class ObjectGrabbableManager : MonoBehaviourPunCallbacks
{
    void Update() {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) {
            base.photonView.RequestOwnership();
        }
    }
}
