using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoss : MonoBehaviour
{
    AudioSource bossmusic;
    [SerializeField] AudioClip clip;

    private void Awake()
    {
        bossmusic = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        bossmusic.clip = clip;
        bossmusic.Play(0);
    }

}
