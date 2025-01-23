using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : Entity
{
    [SerializeField] float moveSpeed = 5.0f;

    Rigidbody2D playerRB;
    public struct PlayerActions {
        public InputAction move;
        public InputAction throwCard;
    }
    PlayerActions playerControls;
    private PlayerInput playerInput;
    public Vector2 moveInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        playerControls.move = playerInput.actions["Move"];
    }
    private void OnDisable()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        moveInput = playerControls.move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        playerRB.linearVelocity = moveInput * moveSpeed;
    }
}
