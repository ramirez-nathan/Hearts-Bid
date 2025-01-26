using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : Entity
{
    [SerializeField] float moveSpeed = 5.0f;

    Rigidbody2D playerRB;
    public struct PlayerActions {
        public InputAction move; // WASD
        public InputAction throwCard; // left click
        public InputAction playAllHands; // right click
        public InputAction unloadHand; // E
        public InputAction dodge; // space
    }

    PlayerActions playerControls;
    private PlayerInput playerInput;
    public Vector2 moveInput;

    //tracks whether or not we are dodging for update loops 
    public struct DodgeState
    {
        public bool isDodging;
        public int dodgeFramesRemaining;
    }

    private DodgeState dodgeState;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        playerControls.move = playerInput.actions["Move"];
        playerControls.throwCard = playerInput.actions["ThrowCard"];
        playerControls.playAllHands = playerInput.actions["PlayAllHands"];
        playerControls.unloadHand = playerInput.actions["UnloadHand"];
        playerControls.dodge = playerInput.actions["Dodge"];

        playerControls.dodge.started += Dodge;
        
    }   
    private void OnDisable()
    {
        playerControls.dodge.started -= Dodge;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //added to handle the diagnol speedup problem 
        Vector2 normalizedInput = moveInput.normalized;
        
        moveInput = playerControls.move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (dodgeState.isDodging)
        {
            Debug.Log("Dodged in fixed update");
            playerRB.linearVelocity = (moveInput * moveSpeed) * 3;
            if (dodgeState.dodgeFramesRemaining > 0)
            {
                dodgeState.dodgeFramesRemaining--;
            }

            if (dodgeState.dodgeFramesRemaining == 0)
            {
                dodgeState.isDodging = false;
            }
            
        }
        else {
            playerRB.linearVelocity = moveInput * moveSpeed;
        }
        
    }


    void Dodge(InputAction.CallbackContext context)
    {
        if (!dodgeState.isDodging) //cant dodge while dodging
        {
            //could use a tuple of bool and # frames to increase speed for? 
            dodgeState.isDodging = true;
            dodgeState.dodgeFramesRemaining = 10;
            Debug.Log("Dodged");
        }
    }
}
