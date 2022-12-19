using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineAudioControl : MonoBehaviour
{
    public AudioClip throwball;
    private AudioSource _audioSource;

    private PhotonView _pv;
    // Start is called before the first frame update
    void Start()
    {
        _pv = this.gameObject.GetComponent<PhotonView>();

        _audioSource = this.gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.volume = DataBase.EffectVolume1;
        _audioSource.clip = throwball;
    }

    public void CallRpcAudioThrowball()
    {
        _pv.RPC("RpcAudioThrowball", RpcTarget.All);
    }

    [PunRPC]
    void RpcAudioThrowball(PhotonMessageInfo info)
    {
        _audioSource.Play();
    }
}
