using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeifyHealth : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private int healthValue = 0;
    [SerializeField] private int sfxToPlay = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            HealthController.Instance.UpdateHealth(healthValue);
            
            if (heart != null)
            {
                AudioController.Instance.PlaySFX(0);
                heart.SetActive(false);
            }else
            AudioController.Instance.PlaySFX(3);
        }
        
    }
}
