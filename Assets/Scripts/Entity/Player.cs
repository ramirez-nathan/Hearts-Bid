using Scripts.Hand;
using UnityEngine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Scripts.Deck;
using System.Collections;

public class Player : Entity
{
    //playtesting for player speed 
    [SerializeField] public float moveSpeed = 5.0f;

    //physics and movements
    Rigidbody2D playerRB;
    private PlayerInput playerInput;
    public Vector2 moveInput;
    public Animator animator;


    //ability classes 
    private DodgeAbility dodgeAbility;  
    private ThrowCardAbility throwCardAbility;
    private EnemyTrackingAbility enemyTrackingAbility;

    //for throwing logic 
    [SerializeField] private PlayerHand playerHand;

    public GameOverScreen gameOverScreen;

    public struct PlayerActions
    {
        public InputAction move; // WASD
        public InputAction throwCard; // left click
        public InputAction callAllHands; // right click
        public InputAction unloadHand; // E
        public InputAction dodge; // space
        public InputAction lockOn; // shift 
        public InputAction sortByRank;
        public InputAction sortBySuit;
    }
    PlayerActions playerControls;

    public int maxHealth = 100;

    private void Start() // set up the player health bar
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !dodgeAbility.isDodging)
        {
            TakeHit(20); // should change depending on enemy's best hand
            healthBar.SetHealth(health);

            if (health <= 0) // the player died
            {
                Debug.Log("GAME OVER!");
                gameOverScreen.Setup();
            }
        }
    }

    public override void TakeHit(int damage)
    {
        base.TakeHit(damage);

        StartCoroutine(HurtRoutine(0.5f));
    }
    IEnumerator HurtRoutine(float duration)
    {
        GetComponent<Animator>().SetBool("Hurt", true);
        yield return new WaitForSeconds(duration);
        GetComponent<Animator>().SetBool("Hurt", false);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        playerInput = GetComponent<PlayerInput>();
        playerRB = GetComponent<Rigidbody2D>();

        // Get the DodgeAbility component
        dodgeAbility = GetComponent<DodgeAbility>();
        throwCardAbility = GetComponent<ThrowCardAbility>();
        enemyTrackingAbility = GetComponent<EnemyTrackingAbility>();

        //get player hand component 
        playerHand = GetComponent<PlayerHand>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        playerControls.move = playerInput.actions["Move"];
        playerControls.throwCard = playerInput.actions["ThrowCard"];
        playerControls.callAllHands = playerInput.actions["CallAllHands"];
        playerControls.unloadHand = playerInput.actions["UnloadHand"];
        playerControls.dodge = playerInput.actions["Dodge"];
        playerControls.lockOn = playerInput.actions["LockOn"];
        playerControls.sortByRank = playerInput.actions["SortByRank"];
        playerControls.sortBySuit = playerInput.actions["SortBySuit"];

        playerControls.dodge.started += Dodge;
        playerControls.throwCard.started += ThrowCard;
        playerControls.sortByRank.started += playerHand.ToggleSortByRank;
        playerControls.sortBySuit.started += playerHand.ToggleSortBySuit;

        playerControls.callAllHands.started += CallAllHands;
        //new lock on 
        playerControls.lockOn.started += LockOn; 




    }
    private void OnDisable()
    {
        playerControls.dodge.started -= Dodge;
        playerControls.throwCard.started -= ThrowCard;
        playerControls.sortByRank.started -= playerHand.ToggleSortByRank;
        playerControls.sortBySuit.started -= playerHand.ToggleSortBySuit;
        //new lock on 
        playerControls.lockOn.started -= LockOn;

        playerControls.callAllHands.started -= CallAllHands;

    }


    void Update()
    {

        //added to handle the diagnol speedup problem 
        Vector2 normalizedInput = moveInput.normalized;
        moveInput = playerControls.move.ReadValue<Vector2>();
        animator.SetFloat("x", playerRB.linearVelocityX);
        animator.SetFloat("z", playerRB.linearVelocityY);
        animator.SetBool("Dashing", dodgeAbility.isDodging);
        // --------------- DEBUGGING UTILITY INPUTS ---------------------------- //
        // PRESS "L" KEY TO CHECK CURRENT HAND
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            playerHand.LogHandContents();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Debug.Log($"Deck size is {FindAnyObjectByType<Deck>().cardsInDeck.Count}");
        }

    }
    private void LateUpdate()
    {
        enemyTrackingAbility.Activate();
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
        if (throwCardAbility.isOnCooldown) {
            Debug.Log("Can't throw card, on cooldown!");
            return; 
        }
        throwCardAbility.SetPlayer(this);
        throwCardAbility.cardToThrow = playerHand.FeedSelectedCard(); // THIS is bad, we are removing card from index here, before we even know if we are on cooldown
        if (throwCardAbility.cardToThrow == null)
        {
            Debug.LogWarning("No card to throw; hand is empty.");
            return;
        }
        throwCardAbility.target = enemyTrackingAbility.closestEnemy;
        if (throwCardAbility.target == null) { return; }
        throwCardAbility.TryActivate();
    }
    private void LockOn(InputAction.CallbackContext context)
    {
        enemyTrackingAbility.switchLock();

    }

    private void CallAllHands(InputAction.CallbackContext context)
    {
        // Trigger the ability event, notifying all subscribers.
        GlobalAbilitySystem.TriggerAbility(GlobalAbilityType.CallAllHands);
    }
}
    
    
   