using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDorTraps : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject music;


    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            music.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
