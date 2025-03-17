using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Serialization;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject checkpointOff;
    [SerializeField] private GameObject checkpointOn;

    [SerializeField] private Vector3 positionOffset;

    void Awake()
    {
        checkpointOff.SetActive(true);
        checkpointOn.SetActive(false);
        positionOffset = new Vector3(1.0f, 0.0f, 0.0f);
    }
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
        if (other.CompareTag ("Player"))
        {
            AudioController.Instance.PlaySFX(0);
            GameController.Instance.respawnPosition = transform.position + positionOffset;

            CheckPoint[] checkpoints = FindObjectsOfType<CheckPoint>();
            foreach(CheckPoint cp in checkpoints)
            {
                cp.checkpointOff.SetActive(true);
                cp.checkpointOn.SetActive(false);
            }
            checkpointOff.SetActive(false);
            checkpointOn.SetActive(true);
        }
    }
}
