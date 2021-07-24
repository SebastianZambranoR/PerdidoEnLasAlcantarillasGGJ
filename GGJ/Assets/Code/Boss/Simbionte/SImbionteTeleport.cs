using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SImbionteTeleport : MonoBehaviour
{
    [SerializeField] Transform[] teleportPoints;

    Transform target;

    public delegate void OnTeleportHandler();
    public event OnTeleportHandler teleported;

    private void OnEnable()
    {
        target = teleportPoints[GetRandom()];
       
    }

    void Teleport()
    {
        transform.position = target.position;
        teleported.Invoke();
        this.enabled = false;
    }

    int GetRandom()
    {
        return Random.Range(0, teleportPoints.Length);
    }
}
