using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSResetPosition : MonoBehaviour
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
        if (other.gameObject.tag == "Player")
        {
            GameController.Instance.gameObject.SetActive(false);
            GameController.Instance.transform.position = GameController.Instance.respawnPosition;
            GameController.Instance.gameObject.SetActive(true);
        }
    }
}
