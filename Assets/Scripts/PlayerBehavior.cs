using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    private bool shootMouse;

    public LayerMask groundLayer;
    public bool jumpRequested;
    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private CapsuleCollider _col;

    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;
    private GameBehavior _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true; // Флаг, указывающий на то, что был запрошен прыжок
        }

        if (Input.GetMouseButtonDown(0))
        {
            shootMouse = true;
        }
        /* 
        this.transform.Translate(Vector3.forward *vInput *  Time.deltaTime);
        this.transform.Rotate(Vector3.up *hInput * Time.deltaTime);
        
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime); */
    }

    void FixedUpdate()
    {
        if (shootMouse)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.linearVelocity = this.transform.forward * bulletSpeed;
            shootMouse = false;
        }
        
        if (jumpRequested)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            Debug.LogFormat("Jump executed!");
            jumpRequested = false;
            playerJump();
        }
            /*if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
                Debug.LogFormat("Eze");
            }*/

        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);


        
    }
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            //1
            _gameManager.HP -= 1;
        }
    }
}

        