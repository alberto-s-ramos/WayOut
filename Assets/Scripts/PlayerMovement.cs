using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI; using UnityEngine.SceneManagement;   public class PlayerMovement : MonoBehaviour {       public Transform playerCam, centerPoint;       //CAMERA     public float mouseSensivity;     public float cameraHeight;      //MOVEMENT     private float moveFB, moveLR;     public float moveSpeed ;     public float rotationSpeed = 5f;     public bool canJump=false;      //JUMPING     public float gravity ;     public float jumpForce ;
    private float verticalVelocity;
    //private bool grounded=true; 
    //COMPONENTS
    private Animator anim;     private CharacterController cc;

    private GameObject gameManager;

    //VARIABLES
    bool walking;

    void Start(){         anim = transform.GetComponentInChildren<Animator>();
        cc = transform.GetComponent<CharacterController>();          gameManager = GameObject.Find("GameManager");          walking = false;     }

    void FixedUpdate(){
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        cc.Move(moveVector * Time.deltaTime);
        verticalVelocity -= 0.3f * gravity * Time.deltaTime;

        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;


        MoveCharacter(moveLR, moveFB);

    }

    void MoveCharacter(float x, float y)
    {
        if (gameManager.GetComponent<Menu>().hasStarted() && !gameManager.GetComponent<Menu>().hasFinished())
        {
            anim.SetFloat("Vel_Y", y);
            anim.SetFloat("Vel_X", x);

            if (x != 0 || y != 0)
                walking = true;
            else if (x == 0 && y == 0)
                walking = false;



            Vector3 movement = new Vector3(0, 0, 0);

            if (!Input.GetMouseButton(1))
            {
                if((y!=0) && !equalAngles(transform.eulerAngles.y, centerPoint.eulerAngles.y, 1.5f))
                {
                    Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * rotationSpeed);
                }
                if (x != 0 )
                {
                    transform.Rotate(Vector3.up * Time.deltaTime * x * (rotationSpeed + 3), Space.World);
                    centerPoint.Rotate(Vector3.up * Time.deltaTime * x * (rotationSpeed + 3), Space.World);
                }
                movement = new Vector3(0, 0, y);
            }
            else if (Input.GetMouseButton(1))
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * rotationSpeed);
                }
                movement = new Vector3(x, 0, y);


            }
            movement = transform.rotation * movement;
            transform.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        }

    }


          void Jump()
    {
        if (canJump)
        {
            if (cc.isGrounded)
            {
              if (Input.GetKeyDown(KeyCode.Space))
              {
                 verticalVelocity = jumpForce;
                    anim.Play("Jump");
              }
            }else
            {
              verticalVelocity -= 1f * gravity * Time.deltaTime;
            }
        }
        
    }      public Animator getAnimator()
    {         return anim;
    }


    public static bool equalAngles(float angle1, float angle2, float acceptableRange)
    {
        if ((angle1 < angle2 + acceptableRange && angle1 > angle2 - acceptableRange) || angle1==angle2)
            return true;
        else
            return false;
    }

    public bool isWalking()
    {
        if (walking && !gameManager.GetComponent<Menu>().hasFinished())
            return true;
        else
            return false;
    }


} 