using UnityEngine;
using Photon.Pun;

public class NetworkSmoothing : MonoBehaviourPunCallbacks, IPunObservable
{

    private Vector3 realPos = Vector3.zero;
    private Quaternion realRot = Quaternion.identity;

    private Vector3 lastPos;
    private Vector3 velocity;

    [Range(0.0f, 1.0f)]
    public float predictionCoeff = 1.0f; //How much the game should predict an observed object's velocity: between 0 and 1

    void Start()
    {
        realPos = this.transform.position;
        realRot = this.transform.rotation;
        //predictionCoeff = Mathf.Clamp(predictionCoeff, 0.0f, 1.0f);  //Uncomment this to ensure the prediction is clamped
    }

    public void Reset()
    {
        realPos = this.transform.position;
        realRot = this.transform.rotation;
        lastPos = realPos;
        velocity = Vector3.zero;
    }

    void Update()
    {
        lastPos = realPos;
        if (!photonView.IsMine)
        {
            //Set the position & rotation based on the data that was received
            transform.position = Vector3.Lerp(transform.position, realPos + (predictionCoeff * velocity * Time.deltaTime), Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRot, Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && photonView.IsMine)
        {
            //Send position over network
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //Send velocity over network
            stream.SendNext((realPos - lastPos) / Time.deltaTime);
        }
        else
        {
            //Receive positions
            realPos = (Vector3)(stream.ReceiveNext());
            realRot = (Quaternion)(stream.ReceiveNext());
            //Receive velocity
            velocity = (Vector3)(stream.ReceiveNext());
        }
    }
}