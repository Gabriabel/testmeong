using Photon.Pun;
using System.IO;
using UnityEngine;

public class InstantiatePlayers : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "NetworkPlayer"), Vector3.zero, Quaternion.identity);
    }
}