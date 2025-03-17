using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControl : MonoBehaviour
{
    public static VehicleControl Instance { get; private set; }
    private Camera theCamera;
    public bool canDrive = false;
    public bool Driving = false;
    private float defaltz = 0;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private GameObject myModel;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (theCamera == null)
        {
            theCamera = Camera.main;
        }
        defaltz = moveDirection.y;
    }

    // Update is called once per frame
    void Update()
    {
       
      
            Move();
            //Debug.Log(Time.deltaTime);

       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canDrive = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            canDrive = true;
        }
    }
    void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                canDrive = false;
            }
        }
    public void Move()
    {
        
        if ( canDrive)
        {
            float currentY = moveDirection.y;
            moveDirection = (Input.GetAxis("Horizontal") * transform.right) + (Input.GetAxis("Vertical") * transform.forward);
            moveDirection.Normalize();
            moveDirection *= PlayerController.Instance.moveSpeed;
            moveDirection.y = currentY;
            transform.position += moveDirection * Time.deltaTime;
        }
    }
    public void Rotate()
    {
        if (!(moveDirection.x.Equals(0.0f) && moveDirection.z.Equals(0.0f)))
        {
            transform.rotation = Quaternion.Euler(0.0f, theCamera.transform.eulerAngles.y, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
            myModel.transform.rotation = Quaternion.Slerp(myModel.transform.rotation, newRotation, PlayerController.Instance.rotateSpeed * Time.deltaTime);
        }
    }
}
