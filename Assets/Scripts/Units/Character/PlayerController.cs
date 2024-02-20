using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Units.Character
{
    public class PlayerController : AUnit
    {
        public float speed = 5f;
        public float jumpForce = 10f;
        public bool IsMove;
        public static Camera CameraRef;
        public static Vector3 mousePos;
        private bool _isFire;

        private Rigidbody2D rb;
        private bool isGrounded;
        private LayerMask groundLayer;
        private Vector2 _moveVector = Vector2.zero;
    
        private UIManager _UIManager;

        protected void Start()
        {
            base.Start();
            CameraRef = Camera.main;
            rb = GetComponent<Rigidbody2D>();
            groundLayer = LayerMask.GetMask("Ground");
            InputManager.PlayerActions.Enable();
            Debug.Log("Input man: " + InputManager.PlayerActions.enabled);

            _anim = GetComponent<Animator>();
        }


        private void Awake()
        {
            if(GameObject.FindWithTag("UIManager")!= null)
                _UIManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
            // Debug.Log("UIManager: " + _UIManager);

            health.SetEnableValues(_UIManager);
        }

        private void OnEnable()
        {
            StartCoroutine(SubcribeRoutine());
            Debug.Log("Subcribe");
        }

        private void OnDisable()
        {
            InputManager.PlayerActions.Move.performed -= StartMove;
            InputManager.PlayerActions.Move.canceled -= EndMove;
            InputManager.PlayerActions.Jump.performed -= Jump;
            // InputManager.PlayerActions.Fire.performed -= Attack;
        
            InputManager.PlayerActions.Disable();
        }

        private void EndMove(InputAction.CallbackContext context)
        {
            IsMove = false;
            _moveVector = Vector3.zero;
            Debug.Log("End");
        }
    
        private void StartMove(InputAction.CallbackContext context)
        {
            IsMove = true;
            Debug.Log("Start");
            Vector2 readValue = context.ReadValue<Vector2>();
            _moveVector = new Vector2(readValue.x, readValue.y) * speed;
        }


        private void OnMove()
        {
            rb.velocity = new Vector2(_moveVector.x, rb.velocity.y);
            Quaternion.LookRotation(new Vector3(_moveVector.x, 0, 0));
            // Debug.Log("Move");
        }

        private void Jump(InputAction.CallbackContext callbackContext)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer);
            if (IsMove) OnMove();

            if (Input.GetKey(KeyCode.K))
            {
                var damage = new DamageStats();
                damage.hitDamage = 10;
                health.TakeHit(damage);
            }
            AnimatorController();
        
            mousePos = CameraRef.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;

            Vector3 directionToMouse = new Vector3(mousePos.x, position.y, mousePos.z) - position;
            Quaternion rotation = directionToMouse.x < 0 ? new Quaternion(0, 180, 0, 0) : new Quaternion(0, 0, 0, 0);

            transform.rotation = rotation;
        
        }

        IEnumerator SubcribeRoutine()
        {
            yield return new WaitForSeconds(1);
            InputManager.PlayerActions.Move.performed += StartMove;
            InputManager.PlayerActions.Move.canceled += EndMove;
            InputManager.PlayerActions.Jump.performed += Jump;
            // InputManager.PlayerActions.Fire.performed += Attack;

            InputManager.PlayerActions.Enable();
        }

        private void Attack(InputAction.CallbackContext obj)
        {
            Debug.Log("fire!");
            _anim.SetTrigger("attack");
        }


        private void AnimatorController()
        {
            // _anim.SetTrigger("");
            _anim.SetBool("isJump", !isGrounded);
            _anim.SetBool("isRun", IsMove && isGrounded);
        }
    }
}

