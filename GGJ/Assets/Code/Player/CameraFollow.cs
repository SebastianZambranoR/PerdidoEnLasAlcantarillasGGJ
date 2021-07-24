using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    PlayerStats Stats;

    private void Awake()
    {
        Stats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        Stats.IsDeath += Stats_IsDeath;
    }

    private void OnDisable()
    {
        Stats.IsDeath -= Stats_IsDeath;
    }

    private void Stats_IsDeath()
    {
        mainCamera.gameObject.GetComponent<AudioListener>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }
}
