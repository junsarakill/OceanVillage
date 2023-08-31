using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBlock : MonoBehaviour
{
    //플레이어가 진입하면 환경음 재생
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound(AudioManager.Sounds.AMBIENT);
        }
    }
}
