using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibirDaño : MonoBehaviour
{

    SpriteRenderer Colorsito;
    Cocodrilo cocodrilo;
    // Start is called before the first frame update
    void Awake()
    {
        Colorsito = GetComponent<SpriteRenderer>();
        cocodrilo = GetComponent<Cocodrilo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
