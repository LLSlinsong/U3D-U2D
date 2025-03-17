using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimArea : MonoBehaviour
{
    bool Swimming = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Instance.myAnimator.SetBool("Swimming", Swimming);
        PlayerController.Instance.swiming = Swimming;


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Swimming = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Swimming = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Swimming = false;
        }
    }
    
}
