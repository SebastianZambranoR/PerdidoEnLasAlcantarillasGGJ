using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int stamina = 100;
    [SerializeField] private int oxygen = 100;
    [SerializeField] private int healt = 100;
    float Contador = 0.3f;
    float Cuentador;
    bool A = false;
    SpriteRenderer Colorsito;

    [SerializeField] private RechargingBar StaminaBar;
    [SerializeField] private RechargingBar OxygenBar;
    [SerializeField] private RechargingBar HealtBar;


    float currentStamina;
    float CurrentHealt;
    float CurrenOxygen;

    float asphyxiatedDamageTime = 1f;
    float asphyxiatedDamageTimer;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    private Coroutine staminaRegen;
    private Coroutine oxygenRegen;



    public  delegate void OnDeathHandler();
    public  event OnDeathHandler IsDeath;

    // Start is called before the first frame update
    void Start()
    {
        Colorsito = GetComponentInChildren<SpriteRenderer>();
        currentStamina = stamina;
        CurrentHealt = healt;
        CurrenOxygen = oxygen;
    }

    private void Update()
    {
        if (A)
        {
            cambiarColor();
        }
    }
    private void OnEnable()
    {
        IsDeath += Death;
    }
    private void OnDisable()
    {
        IsDeath -= Death;
    }

    void Death()
    {
        StopAllCoroutines();
        DecreaceStamina(100);

        
    }


    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    public void DecreaceStamina(int amount)
    {
        if(currentStamina - amount > 0)
        {
            currentStamina -= amount;
            StaminaBar.DecreaseValue(amount);

            if (staminaRegen != null)
                StopCoroutine(staminaRegen);

            staminaRegen = StartCoroutine(RegenStamina());
        }
        else
        {
            currentStamina = 0;
            StaminaBar.DecreaseValue(amount);

        }
        
    }

    public void DecreaceHealt(int amount)
    {
        if(CurrentHealt - amount >= 0)
        {
            CurrentHealt -= amount;
            HealtBar.DecreaseValue(amount);
            Cuentador = 0;
            A = true;
        }
        else
        {
            IsDeath.Invoke();
        }
    }

    public void IncreaseHealt(int amount)
    {
        if(CurrentHealt + amount >= healt)
        {
            CurrentHealt = healt;
        }
        else
        {
            CurrentHealt += healt;
        }
    }
    
    public void DecreaceOxygen(float amount)
    {
        if(CurrenOxygen - amount >= 0)
        {
            CurrenOxygen -= amount;
            OxygenBar.DecreaseValue(amount);

            if (oxygenRegen != null)
                StopCoroutine(oxygenRegen);

            oxygenRegen = StartCoroutine(RegenOxygen());
        }
        else
        {
            asphyxiated();
        }

       
    }


    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < stamina)
        {
            currentStamina += stamina / 100;
            StaminaBar.RegenValue(currentStamina);
            yield return regenTick;
        }
        staminaRegen = null;
    }

    private IEnumerator RegenOxygen()
    {
        yield return new WaitForSeconds(2);

        while(CurrenOxygen < oxygen)
        {
            CurrenOxygen += oxygen / 100;
            OxygenBar.RegenValue(CurrenOxygen);
            yield return regenTick;
        }
        oxygenRegen= null;
    }

    private void asphyxiated()
    {
        asphyxiatedDamageTimer += Time.deltaTime;
        if(asphyxiatedDamageTimer > asphyxiatedDamageTime)
        {
            DecreaceHealt(20);
            asphyxiatedDamageTimer = 0;
        }
    }
    private void cambiarColor()
    {
        Colorsito.color = Color.red;
        Cuentador += Time.deltaTime;
        if (Cuentador > Contador)
        {
            Colorsito.color = Color.white;
            A = false;
        }
    }
}
