using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeifyWealth : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private int wealthValue = 1;
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
            AudioController.Instance.PlaySFX(0);
            GameController.Instance.UpdateWealth(wealthValue);
           if(coin!= null)
            coin.SetActive(false); 
        }
    }
}
