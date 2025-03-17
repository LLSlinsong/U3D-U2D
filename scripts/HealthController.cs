using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int sfxToPlay = 1;
    public static HealthController Instance { get; set; }
    [SerializeField] private int maxHealth = 5;
    public int CurrentHealth { get; set; }
    private bool hasShield = false;
    [SerializeField] private float hasShieldTime = 3.0f;
    private float hasShieldCounter = 0.0f;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance.healthSlider.maxValue = maxHealth;
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasShieldCounter>0.0f)
        {
            hasShieldCounter -= Time.deltaTime;
            if(hasShieldCounter<=0.0f)
            {
                hasShield = false;
            }
        }
    }
    public void UpdateHealth(int update)
    {
        //CurrentHealth += update;
        if((!hasShield)||(hasShield&(update>0)))
        {
            CurrentHealth += update;
            if(update<0)
            {
                hasShield = true;
                hasShieldCounter = hasShieldTime;
            }
        }
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            
            GameController.Instance.Respawn();
        }
        else if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
        UpdateUI();
    }
    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        UpdateUI();
    }
    public void UpdateUI()
    {
        UIController.Instance.healthText.text = CurrentHealth.ToString();
        UIController.Instance.healthSlider.value = CurrentHealth;
    }
}
