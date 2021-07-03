using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    private float movementH;
    public float lookSpeed = 3f;
    private Vector2 rotation = Vector2.zero;

    public Animator anim;
    public AnimatorStateInfo currentSt;

    public enum animState {stateIdle = 0, stateWalk = 1, stateRun = 2, stateJump = 3};
    public animState currentState;

    public Rigidbody rb;
    public GameObject cam, firstPersonCam, thirdPersonCam;

    public bool unsheathed;
    public GameObject swordUnsh, swordSh;

    public static bool playerAlive;

    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = 5f;
        runSpeed = 10f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        cam = thirdPersonCam;

        unsheathed = false;
        swordUnsh.SetActive(false);  

        playerAlive = true;
  
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive)
        {
            //allow movement only within terrain borders 
            //clamp will return whichever position the player is currently at, unless it is outside
            //the borders, then it will reset the player back to the border itself        
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 125f, 1083f), 
                transform.position.y, Mathf.Clamp(transform.position.z, -25f, 920f));
                
            Move();
            Animate();
        }
    }

    void Move()
    {
        //w, d for moving
        if (Input.GetKey("w"))
        {
            if (Input.GetKey(KeyCode.LeftShift)) movementH = runSpeed;
            else movementH = walkSpeed;
        }
        else if(Input.GetKey("s")) movementH = -walkSpeed;
        else movementH = 0f;

        transform.position += transform.forward * Time.deltaTime * movementH;
        
        //mouse look
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0,rotation.y) * lookSpeed;
        cam.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);

        //the second condition is to prevent starting another jump animation by pressing 
        //spacebar while already in the middle of a jump
        if (Input.GetKeyDown("space") && currentState != animState.stateJump)
        {
            anim.SetTrigger("Jump");
            currentState = animState.stateJump;
        }

        //Firs person and third person camera switch
        if (Input.GetKeyDown("f"))
        {
            if (cam == thirdPersonCam) 
            {
                cam = firstPersonCam;
                firstPersonCam.SetActive(true);
                thirdPersonCam.SetActive(false);
            }
            else if (cam == firstPersonCam) 
            {
                cam = thirdPersonCam;
                thirdPersonCam.SetActive(true);
                firstPersonCam.SetActive(false);
            }
        }
    }

    void Animate()
    {
        currentSt = anim.GetCurrentAnimatorStateInfo(0);

        if (currentSt.nameHash == Animator.StringToHash("Base Layer.Jump Over"))
            currentState = animState.stateJump;
        else if (movementH == runSpeed) currentState = animState.stateRun;
        else if (movementH == walkSpeed || movementH == -walkSpeed) currentState = animState.stateWalk;
        else if (movementH == 0f) currentState = animState.stateIdle;
        
        if (currentState == animState.stateIdle)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walking", false); 
            anim.SetBool("Running", false);                       
        } 
        else if (currentState == animState.stateWalk)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
        }
        else if (currentState == animState.stateRun)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false); 
            anim.SetBool("Running", true);  
        }

        if (PickupPart.hasSword) 
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                if (!unsheathed) 
                {
                    anim.SetTrigger("Unsheathe");
                    unsheathed = true;
                } 
                else 
                {
                    anim.SetTrigger("Sheathe");
                    unsheathed = false;
                }
            }

            if (unsheathed) 
            {
                if(Input.GetMouseButtonDown(0) && currentState == animState.stateIdle) 
                {
                    anim.SetTrigger("Slash1");
                }

                if(Input.GetMouseButtonDown(1)) 
                {
                    anim.SetTrigger("Slash2");
                }
            }
        }

    }

    //these two functions are called by animation events when "Sheathe" and "Unsheathe" 
    //animations play out (lines 131-143)
    public void SwordUnsheathe()
    {
        swordUnsh.SetActive(true);
        swordSh.SetActive(false);
    }

    public void SwordSheathe()
    {
        swordSh.SetActive(true);
        swordUnsh.SetActive(false);
    }
}
