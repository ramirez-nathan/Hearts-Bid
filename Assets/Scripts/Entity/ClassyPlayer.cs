using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ClassyPlayer : Entity
{
    [SerializeField] public float moveSpeed = 5.0f;

    //physics and movements
    Rigidbody2D playerRB;
    private PlayerInput playerInput;
    public Vector2 moveInput;


    //ability classes 
    private DodgeAbility dodgeAbility;  
    private ThrowCardAbility throwCardAbility;
    private EnemyTrackingAbility enemyTrackingAbility;


    public struct PlayerActions
    {
        public InputAction move; // WASD
        public InputAction throwCard; // left click
        public InputAction playAllHands; // right click
        public InputAction unloadHand; // E
        public InputAction dodge; // space

    }
    PlayerActions playerControls;




    private void Awake()
    {

        playerInput = GetComponent<PlayerInput>();
        playerRB = GetComponent<Rigidbody2D>();

        // Get the DodgeAbility component
        dodgeAbility = GetComponent<DodgeAbility>();
        throwCardAbility = GetComponent<ThrowCardAbility>();
        enemyTrackingAbility = GetComponent<EnemyTrackingAbility>();

    }
    private void OnEnable()
    {
        playerControls.move = playerInput.actions["Move"];
        playerControls.throwCard = playerInput.actions["ThrowCard"];
        playerControls.playAllHands = playerInput.actions["PlayAllHands"];
        playerControls.unloadHand = playerInput.actions["UnloadHand"];
        playerControls.dodge = playerInput.actions["Dodge"];

        playerControls.dodge.started += Dodge;
        playerControls.throwCard.started += ThrowCard;
        enemyTrackingAbility.TryActivate();


    }
    private void OnDisable()
    {
        playerControls.dodge.started -= Dodge;
        playerControls.throwCard.started -= ThrowCard;
        enemyTrackingAbility.Deactivate();


    }


    void Update()
    {

        //added to handle the diagnol speedup problem 
        Vector2 normalizedInput = moveInput.normalized;
        moveInput = playerControls.move.ReadValue<Vector2>();


    }

    void FixedUpdate()
    {

        playerRB.linearVelocity = new Vector2(moveInput.x, moveInput.y) * moveSpeed; // Apply movement to Rigidbody

    }


    // This method is called when the dodge input (spacebar) is pressed
    private void Dodge(InputAction.CallbackContext context)
    {
        dodgeAbility.TryActivate();  // Call the TryActivate method from the Dodge class

    }


    // This method is called when the throw card input (for example, a mouse button press) is triggered
    private void ThrowCard(InputAction.CallbackContext context)
    {
        // Get the closest enemy to the player using the EnemyTrackingAddOn
        //if not null
        throwCardAbility.target = enemyTrackingAbility.closestEnemy;
        throwCardAbility.TryActivate();
    }
}
    
   