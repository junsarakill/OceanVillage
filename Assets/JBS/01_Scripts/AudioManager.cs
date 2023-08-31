using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    //브금, 환경음, 효과음
    [SerializeField] AudioSource bgm,amb,sfx;

    //오디오 리스트
    [SerializeField] List<AudioClip> audioList;

    public enum Sounds
    {
        BACKGROUND, AMBIENT, FISH_CATCH, FISHING_START, GET_SCORE
    }

    //소리 재생
    public void PlaySound(Sounds sound)
    {
        if(sound == Sounds.BACKGROUND)
        {
            bgm.clip = audioList[(int) sound];
            bgm.Play();
        }
        else if(sound == Sounds.AMBIENT)
        {
            amb.clip = audioList[(int) sound];
            amb.Play();
        }
        else
        {
            sfx.PlayOneShot(audioList[(int) sound]);
        }
    }


    private void Start() {
        //배경 음악 재생
        PlaySound(Sounds.BACKGROUND);
    }
}
