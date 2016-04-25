using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public GameObject wrap;
    public GameObject cam;
    public float turnspeed = 200f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    private float cameraAngle;
    private float turnAngle;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        cam.transform.rotation = new Quaternion(0, 0, 0, 0);
        cameraAngle = wrap.transform.rotation.eulerAngles.y;
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
   
        movement = Quaternion.AngleAxis(cameraAngle, Vector3.up) * movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
        
    }

    void Turning()
    {
        turnAngle = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            turnAngle = -turnspeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            turnAngle = turnspeed;
        }
     
        playerRigidbody.transform.Rotate(new Vector3(0, turnAngle, 0) * Time.deltaTime);
        wrap.transform.Rotate(new Vector3(0, turnAngle, 0) * Time.deltaTime);

    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        Debug.Log(walking);
        anim.SetBool("IsWalking", walking);
    }
};
