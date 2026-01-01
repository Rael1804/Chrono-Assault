using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*FALTAN ANIMACIONES*/
public class PlayerMove : MonoBehaviour
{
    private CharacterController characterController;
    public CameraSwitch cameraSwitch;

    public GameOverMenu gameOverMenu;

    private float hurtDuration = 2f; 
    private float hurtTimer;
    private bool isHurt;
    //Ground
    public Transform GroundCheck;
    public float GroundCheckRadius = 0.3f;
    public LayerMask GroundLayer;
    //Jump
    private float gravity = -9.81f;
    public float jumpHeight = 4;
    //Velocity
    private Vector3 velocity;
    public float speed = 5f;
    public float runSpeed = 10f;
    //Animator
    private Animator animator;
    //Gun
    public RigBuilder rigBuilder;
    public RigLayer nonAimRig;
    public RigLayer firstPersonRig;
    public GameObject gunNonAim;
    public GameObject gunFirstPerson;
    private bool aim = false;
    public bool hasGun = false;
    private bool firstPerson = false;
    public bool hasDie = false;


    public float vida;
    public float maxvida;
    public float impactForce = 25f;
    private Rigidbody rb;
    public CapsuleCollider collider;
    public List<GameObject> Imagenes;
    public Slider barraVida;
    public EnemiesCounter ec;

    private void Start() {
        vida = maxvida;
        animator = GetComponent<Animator>();    
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        Imagenes[0].SetActive(true);
        Imagenes[1].SetActive(true);
        
    }

    private void Update()
    {
        if (!hasDie) {
            Movement();
            Jump();
        }

        barraVida.value = vida;

        if (isHurt) {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer <= 0f) {
                isHurt = false;
                animator.SetBool("hurt", false);
            }
        }
    }

    //Movimiento
    private void Movement() {
        

        float move_x = Input.GetAxis("Horizontal");
        float move_z = Input.GetAxis("Vertical");
        if (move_x > 0.0f || move_z > 0.0f) {
            animator.SetBool("hurt", false);
        }

        if (IsGrounded() && velocity.y < 0) {
            velocity.y = -2f;
        }

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

        if (Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("running", true);  
        } else {
            animator.SetBool("running", false);  
        }

        Vector3 move = transform.right * move_x + transform.forward * move_z;
        characterController.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        animator.SetFloat("velocityX", move_x);  
        animator.SetFloat("velocityY", move_z);    

        
    }

    private bool IsGrounded() {
        return Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer);
    }

    //Saltar
    private void Jump() {
        if (IsGrounded()) {
            if (Input.GetButtonDown("Jump") && IsGrounded()) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
                animator.SetBool("jump", true);
            }
            animator.SetBool("ground", true);
        } else {
            Falling();
        }
    }

    private void Falling() {
        animator.SetBool("ground", false);
        animator.SetBool("jump", false);
    }


    public void ChangeFirstPerson(bool state) {
        firstPerson = state;
        if (firstPerson) {
            rigBuilder.layers.Clear();
            rigBuilder.layers.Add(firstPersonRig);
            gunFirstPerson.SetActive(true);
            gunNonAim.SetActive(false);
        } else {
            rigBuilder.layers.Clear();
            aim = false;
            gunFirstPerson.SetActive(false);
            gunNonAim.SetActive(true);
            rigBuilder.layers.Add(nonAimRig);
        }
        rigBuilder.Build();
    }  

    public bool IsFirstPerson() {
        return firstPerson;
    }  


    public void TakeDamage(float damage)
    {

        if (vida <= 0 && !hasDie) {
            Die();
        } else {
            if (!IsFirstPerson()) {
                StartHurt();
            }
            
            vida -= damage; 
        }
    }

    private void StartHurt()
    {
        isHurt = true;
        hurtTimer = hurtDuration;
        animator.SetBool("hurt", true);
    }


    private void Die() {
        hasDie = true;
        ec.guardar();
        if (cameraSwitch.GetFirstPerson()) {
            cameraSwitch.SetFirstPerson(false);
            cameraSwitch.DoSwitchCamera();
        }
        rigBuilder.layers.Clear();
        gunFirstPerson.SetActive(false);
        gunNonAim.SetActive(false);
        animator.SetTrigger("die");
        Invoke("DiedMenu",2f);  
    }

    private void DiedMenu() {

        gameOverMenu.GameOverOn();
    }

    public void MaxHealth() {
        vida = maxvida;
    }

}



