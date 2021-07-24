using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenControl : MonoBehaviour
{
    [SerializeField] Image deathImage;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButtom;
    
    void OnDeath()
    {
        deathImage.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButtom.gameObject.SetActive(true);
    }
}
