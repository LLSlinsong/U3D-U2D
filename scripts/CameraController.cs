using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; set; }
    public CinemachineBrain TheCinemachinBrain { get;  set; }

  

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        TheCinemachinBrain = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
