using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReproducirSonido : MonoBehaviour
{
    [SerializeField] AudioClip Audio;
    AudioSource UP;
    // Start is called before the first frame update
    void Awake()
    {
        UP = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void reproducir()
    {
        UP.clip = Audio;
        UP.Play(0);
    }

    void GameStart()
    {
        SceneManager.LoadScene("Game");
    }
}

