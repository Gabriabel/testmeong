using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private Vector3 globalNetworkPosition;
    private Vector3 localNetworkPosition;
    private Quaternion globalNetworkRotation;
    private Quaternion localNetworkRotation;

    private Vector3 globalStoredPosition;
    private Vector3 localStoredPosition;

    private float globalDistance;
    private float localDistance;
    private float globalAngle;
    private float localAngle;

    private Vector3 globalDirection;
    private Vector3 localDirection;

    public GameObject avatar;

    [HideInInspector]
    public Transform playerGlobal, playerLocal;

    void Start()
    {
        if (photonView.IsMine)
        {
            playerGlobal = GameObject.Find("OVRPlayerController").transform;
            playerLocal = playerGlobal.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");

            transform.SetParent(playerLocal);

            globalNetworkPosition = Vector3.zero;
            globalNetworkRotation = Quaternion.identity;
            localNetworkPosition = Vector3.zero;
            localNetworkRotation = Quaternion.identity;

            globalStoredPosition = playerGlobal.position;
            localStoredPosition = avatar.transform.localPosition;
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.MoveTowards(transform.position, globalNetworkPosition, globalDistance * (1.0f / PhotonNetwork.SerializationRate));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, globalNetworkRotation, globalAngle * (1.0f / PhotonNetwork.SerializationRate));

            avatar.transform.localPosition = Vector3.MoveTowards(avatar.transform.localPosition, localNetworkPosition, localDistance * (1.0f / PhotonNetwork.SerializationRate));
            avatar.transform.localRotation = Quaternion.RotateTowards(avatar.transform.localRotation, localNetworkRotation, localAngle * (1.0f / PhotonNetwork.SerializationRate));
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            globalDirection = playerGlobal.position - globalStoredPosition;
            globalStoredPosition = playerGlobal.position;

            localDirection = avatar.transform.localPosition - localStoredPosition;
            localStoredPosition = avatar.transform.localPosition;
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);

            stream.SendNext(globalDirection);
            stream.SendNext(localDirection);
        }
        else
        {
            globalNetworkPosition = (Vector3)stream.ReceiveNext();
            globalNetworkRotation = (Quaternion)stream.ReceiveNext();
            localNetworkPosition = (Vector3)stream.ReceiveNext();
            localNetworkRotation = (Quaternion)stream.ReceiveNext();

            globalDirection = (Vector3)stream.ReceiveNext();
            localDirection = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            globalNetworkPosition += globalDirection * lag;
            localNetworkPosition += localDirection * lag;

            globalDistance = Vector3.Distance(transform.position, globalNetworkPosition);
            globalAngle = Quaternion.Angle(transform.rotation, globalNetworkRotation);
            localDistance = Vector3.Distance(avatar.transform.localPosition, localNetworkPosition);
            localAngle = Quaternion.Angle(avatar.transform.localRotation, localNetworkRotation);
        }
    }
}
