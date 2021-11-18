using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] [Range (1,4)]private int numPlayers;
    [SerializeField] private GameObject[] players;
    //[SerializeField] private int[] playerTurn;
    [SerializeField] private int currentTurn;
    [SerializeField] private float nextTunrTimer;
    [SerializeField] private int currentPlayer;
    [SerializeField] public static int score;
    [SerializeField] private float turnTimer = 0.00f;
    [SerializeField] private int loopPosition;
    // Sound components
    [SerializeField] private GameObject[] audioTrigger;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource mainSound;
    // Raycast triggers
    [SerializeField] private int rayDistance = 1;
    [SerializeField] public RaycastHit hit;
    //
    private PlayerMovement playerMovement;
    public bool currentPlayerRun;
    public static bool movementPhaseStart;
    public static bool movementPhaseDone;
    public static bool actionPhase;
    public static bool actionDone;
    public static bool nextPlayerTurn;
    public static bool turnEnd;
     
    

    void Awake()
    {
        // Singleton method
        if (instance == null)
        {
            instance = this;
            // creates and fills players array  
            players = new GameObject[4];
            players[0] = GameObject.Find("Player 1");
            players[1] = GameObject.Find("Player 2");
            players[2] = GameObject.Find("Player 3");
            players[3] = GameObject.Find("Player 4");
            // Deactivates players that wont plat
            switch (numPlayers)
            {
                case 1:
                    players[1].SetActive(false);
                    players[2].SetActive(false);
                    players[3].SetActive(false);
                    break;
                case 2:
                    players[2].SetActive(false);
                    players[3].SetActive(false);
                    break;
                case 3:
                    players[3].SetActive(false);
                    break;
                default:
                    break;
            }
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the turn, player order and loop position
        currentTurn = 1;
        currentPlayer = 1;
        // Loop position refers to the Waypoint on the stage to return to once the player has made a full lap around the board
        loopPosition = 4;
        movementPhaseStart = false;
        movementPhaseDone = false;
        actionPhase = false;
        actionDone = false;
        turnEnd = false;
        currentPlayer = 0;
        // Initialize timers
        nextTunrTimer = 0.00f;
        turnTimer = 0.00f;
    }

    // Update is called once per frame
    void Update()
    {
        // starts at 0, if numplayers = 1 then it will only execute once
        if (currentPlayer != numPlayers)
        {
            // gets current player scripts
            playerMovement = players[currentPlayer].GetComponent<PlayerMovement>();

            // if player hasnt moved and is nost moving then Start Movement Phase
            if (!movementPhaseDone && !playerMovement.isRun && !playerMovement.movementDone)
            {
                movementPhaseStart = true;
            }
            
            // Once movement has started the flags are set to avoid setting start again
            if (movementPhaseStart)
            {
                if (playerMovement.isRun)
                {
                    movementPhaseStart = false;
                }
                else
                {
                    TurnMovement(currentPlayer);
                }      
            }

            if (playerMovement.movementDone)
            {
                movementPhaseDone = true;
            }

            // Once the movement hase is complete and player stopped we begin action phase
            if (movementPhaseDone && !playerMovement.isRun && !actionDone)
            {
                actionPhase = true;
            }

            // in action phase we call for action scripts and await player input
            if(actionPhase && !actionDone)
            {
                TurnAction(currentPlayer);
                if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.R))
                {
                    actionPhase = false;
                    actionDone = true;
                }
            }

            // the IF is missing parameters from the action scripts
            // Once actions have been taken and confirmed a timner is started and player count is increased
            if(movementPhaseDone && actionDone && !nextPlayerTurn)
            {
                playerMovement.movementDone = false;
                currentPlayer++;
                nextPlayerTurn = true;
            }

            // Timer with message for playeRS
            if (nextPlayerTurn)
            {
                nextTunrTimer += Time.deltaTime;
                if (numPlayers != 1)
                    Debug.Log("Preparate siguiente Jugador");
            }
            
            // once timer is ready actions are reset and another turn can be taken
            // if more than 1 playuer is playing then the next player is anounced
            if (nextTunrTimer >= 2.00f)
            {
                nextPlayerTurn = false;
                movementPhaseDone = false;
                actionDone = false;
                if (numPlayers!= 1)
                    Debug.Log("Tu turno Jugador" + currentPlayer);
                nextTunrTimer = 0.00f;
            }
        }

        // if all players have played their turns the global turn counter increases
        if (currentPlayer == numPlayers)
        {
            currentPlayer = 0;
            currentTurn++;
            Debug.Log("Empieza tu nuevo turno");
        }

        // Audio Control By Raycast
        if (Physics.Raycast(audioTrigger[0].transform.position, audioTrigger[0].transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            mainSound.clip = audioClips[1];
            mainSound.Play();
        }
        if (Physics.Raycast(audioTrigger[1].transform.position, audioTrigger[1].transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            mainSound.clip = audioClips[2];
            mainSound.Play();
        }
    }

    // Method to generate the movement and activate current player movement script
    // movement is chosen as a random 1-6 as in a dice roll
    // player input SPACEBAR is expected
    private void TurnMovement(int playerIndex)
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerMovement.movementPoints == 0)
        {
            int movement = Random.Range(1, 7);
            playerMovement.isRun = true;
            playerMovement.loopPosition = loopPosition;
            playerMovement.movementPoints = movement;
            playerMovement.playerAnimator.SetTrigger("Run");
            // TEST if changing followingWaypoint with lastWaypoint yields same result
            playerMovement.newWP = playerMovement.followingWaypoint + movement;
            Debug.Log("Haz sacado " + movement);
        }
    }

    // Method to take one of three actions
    // 1 fight, 2 quest, 3 rest
    // player input F, Q or R is expected
    private void TurnAction(int playerIndex)
    {
        // Call for figth Script to instantiate enemy based on tile difficulty and resolve battle at random
        if (Input.GetKeyDown(KeyCode.F))
        {
            // put fight script here
            Debug.Log("Fight!");
        }

        // Call for quest Script to resolve a quest at random based on tile difficulty
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // put quest script here
            Debug.Log("Questing...");
        }

        // Call for recovery Script to regain a fixed amount of HP
        if (Input.GetKeyDown(KeyCode.R))
        {
            // put recovery script here
            Debug.Log("You decide to take a break and indulge in the scenery");
        }
    }
}
