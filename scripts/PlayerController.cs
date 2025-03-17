using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Transactions;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    #region Camera Data
    private Camera theCamera;
     float minFov = 15f;
  float maxFov = 90f;
 float sensitivity = 10f;
    #endregion

    #region Movement Data
    [SerializeField] public float moveSpeed = 10.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravityScale = 5.0f;
    [SerializeField] public float rotateSpeed = 5.0f;
    [SerializeField] public bool swiming = false;
    private Vector3 moveDirection = Vector3.zero;
    #endregion

    #region Player Data

    // Start is called before the first frame update

    private CharacterController myCharacterController;
    [SerializeField] private GameObject myModel;
    public Animator myAnimator;
    #endregion
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (theCamera==null)
        {
            
            theCamera = Camera.main;
        }
        myCharacterController = GetComponent<CharacterController>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Debug.Log(Time.deltaTime);
        Rotate();
        Jump();
        Cam();
    }
    public void Cam()
    {

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
   public void Move()
    {
        float currentY = moveDirection.y;
        moveDirection = (Input.GetAxis("Horizontal") * transform.right) + (Input.GetAxis("Vertical") * transform.forward);
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = currentY;
        myCharacterController.Move(moveDirection * Time.deltaTime);
        myAnimator.SetBool("Left",Input.GetKey("a"));
        myAnimator.SetBool("Right", Input.GetKey("d"));
        myAnimator.SetBool("Back", Input.GetKey("s"));
        myAnimator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
    }
    public void Jump()
    {
        if (myCharacterController.isGrounded || swiming/*transform.position.y <= 0.1f*/)
        {
            
            moveDirection.y = 0.0f;
            if (Input.GetButtonDown("Jump")/*Input.GetKeyDown(KeyCode.Space*/)
            {
                moveDirection.y = jumpForce;
               // myCharacterController.Move(moveDirection * Time.deltaTime);
            }
        }
        // moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        if(!swiming)
        moveDirection.y += Physics.gravity.y * gravityScale*Time.deltaTime ;

        myAnimator.SetBool("GoingDown", moveDirection.y<0);
        myAnimator.SetBool("Grounded", myCharacterController.isGrounded);
    }
     public void Rotate()
    {
        if(!(moveDirection.x.Equals(0.0f)&&moveDirection.z.Equals(0.0f)))
        {
            transform.rotation = Quaternion.Euler(0.0f, theCamera.transform.eulerAngles.y, 0.0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
            myModel.transform.rotation = Quaternion.Slerp(myModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
    }
}

