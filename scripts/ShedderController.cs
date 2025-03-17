using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShedderController : MonoBehaviour
{
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
            
            GameController.Instance.Respawn();
        }
    }
}
