using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager instance
    public static GameManager instance;
    
    // Amount of players
    [SerializeField] [Range(1, 4)] public int numPlayers;
    // Players Array
    [SerializeField] private GameObject[] players;
    // Players Inventory
    [SerializeField] private GameObject inventoryManagerInstance;
    //[SerializeField] private int[] playerTurn;
    [SerializeField] private int currentTurn;
    [SerializeField] private float nextTunrTimer;
    [SerializeField] public int currentPlayer;
    [SerializeField] public static int score;
    [SerializeField] private float turnTimer = 0.00f;
    [SerializeField] private int loopPosition;
    // Sound components
    [SerializeField] private GameObject[] audioTrigger;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource mainSound;
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioClip coinClip;
    // Raycast triggers
    [SerializeField] private int rayDistance = 1;
    [SerializeField] public RaycastHit hit;
    // Menu data Transfer
    private MenuDataTransfer dataInstance;
    // HUD Control
    private GUIManager hudInstance;
    // Player movement Script
    private PlayerMovement playerMovement;
    // Player marker control
    private MarkerController markerController;
    // Player inventory control
    private InventoryManager inventoryManager;
    // Player camera control
    [SerializeField] private CameraManager cameramanager;
    // Player turn status 
    [SerializeField] public static bool currentPlayerRun;
    [SerializeField] public static bool movementPhaseStart;
    [SerializeField] public static bool movementPhaseDone;
    [SerializeField] public static bool actionPhase;
    [SerializeField] public static bool actionDone;
    [SerializeField] public static bool nextPlayerTurn;
    [SerializeField] public static bool turnEnd;
    // player override status
    [SerializeField] public static bool movementOverride = false;
    [SerializeField] public static bool actionOverride = false;


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
            // Gets numPlayers from Menu Data
            dataInstance = GameObject.Find("Menu Data Transfer").GetComponent<MenuDataTransfer>();
            numPlayers = dataInstance.numPlayers;
            // 
            hudInstance = GameObject.Find("HUD").GetComponent<GUIManager>();
            for (int i = 0; i < numPlayers; i++)
            {
                hudInstance.ActivePlayerHUD(i);
            }
            // Deactivates players that wont plaY and disable markers other than P1
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
            
            // gets virtual camera
            GameObject vCam = GameObject.Find("Vcam1");
            cameramanager = vCam.GetComponent<CameraManager>();
            CameraChange(currentPlayer);

            markerController = players[0].GetComponent<MarkerController>();
            markerController.markerIndex = 0;
            markerController = players[1].GetComponent<MarkerController>();
            markerController.markerIndex = 1;
            markerController.markerEN = false;
            markerController = players[2].GetComponent<MarkerController>();
            markerController.markerIndex = 2;
            markerController.markerEN = false;
            markerController = players[3].GetComponent<MarkerController>();
            markerController.markerIndex = 3;
            markerController.markerEN = false;

            inventoryManagerInstance = GameObject.Find("InventoryManager");
            inventoryManager = inventoryManagerInstance.GetComponent<InventoryManager>();

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
        inventoryManager.currentPlayer = currentPlayer;
        // Initialize timers
        nextTunrTimer = 0.00f;
        turnTimer = 120.00f;

    }

    // Update is called once per frame
    void Update()
    {
        // Turn timer control
        if (turnTimer >= 0.0f)
        {
            turnTimer -= Time.deltaTime;
        }
        else
        {
            // Override movemente phase
            if (movementPhaseStart)
            {
                if (!playerMovement.isRun)
                {
                    movementOverride = true;
                }
                else
                {
                    movementOverride = false;
                }    
            }

            // Override action phase
            if (actionPhase && !actionDone)
            {
                actionOverride = true;
            }
            else
            {
                actionOverride = false;
            }
        }

        // starts at 0, if numplayers = 1 then it will only execute once
        if (currentPlayer != numPlayers)
        {
            // gets current player scripts
            playerMovement = players[currentPlayer].GetComponent<PlayerMovement>();
            // Gets current player marker script
            markerController = players[currentPlayer].GetComponentInChildren<MarkerController>();
            // Enables markes and asigns color according to current player
            MarkerEnabler(currentPlayer);
            // sets canera on current player
            CameraChange(currentPlayer);

            // if player hasnt moved and is nost moving then Start Movement Phase
            if (!movementPhaseDone && !playerMovement.isRun && !playerMovement.movementDone && !nextPlayerTurn)
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
                    TurnMovement(movementOverride);
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
            if (actionPhase && !actionDone)
            {
                TurnAction(actionOverride);
                if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.R) || actionOverride)
                {
                    actionPhase = false;
                    actionDone = true;
                }
            }

            // the IF is missing parameters from the action scripts
            // Once actions have been taken and confirmed a timner is started and player count is increased
            if (movementPhaseDone && actionDone && !nextPlayerTurn)
            {
                playerMovement.movementDone = false;
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
                currentPlayer++;
                inventoryManager.currentPlayer = currentPlayer;
                nextPlayerTurn = false;
                movementPhaseDone = false;
                actionDone = false;
                if (numPlayers != 1)
                    Debug.Log("Tu turno Jugador" + currentPlayer);
                nextTunrTimer = 0.00f;
                turnTimer = 120.00f;
                MarkerDisable();
            }
        }

        // if all players have played their turns the global turn counter increases
        if (currentPlayer == numPlayers)
        {
            currentPlayer = 0;
            currentTurn++;
            Debug.Log("Empieza tu nuevo turno");
        }

    }

    // Method to generate the movement and activate current player movement script
    // movement is chosen as a random 1-6 as in a dice roll
    // player input SPACEBAR is expected
    private void TurnMovement(bool movementOverride)
    {
        if ((Input.GetKeyDown(KeyCode.Space) && playerMovement.movementPoints == 0)|| movementOverride)
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

    // Enables current player marker and assigns color
    private void MarkerEnabler(int player)
    {
        markerController.markerEN = true;
        switch (player)
        {
            case 0:
                markerController.markerMat.material.color = Color.red;
                break;
            case 1:
                markerController.markerMat.material.color = Color.blue;
                break;
            case 2:
                markerController.markerMat.material.color = Color.yellow;
                break;
            case 3:
                markerController.markerMat.material.color = Color.green;
                break;
            default:
                break;
        }

    }

    // Disables current player marker
    private void MarkerDisable()
    {
        markerController.markerEN = false;
    }

    // Assigns virtual camera to current player
    private void CameraChange(int player)
    {
        cameramanager.camFollow = players[player];
        cameramanager.camLookAt = players[player];
    }

    // Method to take one of three actions
    // 1 fight, 2 quest, 3 rest
    // player input F, Q or R is expected
    private void TurnAction(bool actionOverride)
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
        if (Input.GetKeyDown(KeyCode.R ) || actionOverride)
        {
            // put recovery script here
            Debug.Log("You decide to take a break and indulge in the scenery");
        }
    }

    private void RaycastSoundChange()
    {
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

    // Plays coin sound
    public void CoinSound()
    {
        coinSound.PlayOneShot(coinClip);
    }
}