using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModifyHealth : MonoBehaviour
{
    [SerializeField] private int healthValue = -1;
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
        if(other.tag=="Enemy")
        {
            other.GetComponent<EnemyHealthController>().UpdateHealth(healthValue);
        }
    }
}
