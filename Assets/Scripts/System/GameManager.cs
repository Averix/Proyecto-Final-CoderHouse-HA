using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range (1,4)]private int numPlayers;
    [SerializeField] private GameObject[] players;
    //[SerializeField] private int[] playerTurn;
    [SerializeField] private int currentTurn;
    [SerializeField] private int currentPlayer;
    [SerializeField] private int score;
    [SerializeField] private float turnTimer = 0.00f;
    [SerializeField] private int loopPosition;
    private PlayerMovement playerMovement;
    public bool currentPlayerRun;
    

    void Awake()
    {
        players = new GameObject[4];
        players[0] = GameObject.Find("Player 1");
        players[1] = GameObject.Find("Player 2");
        players[2] = GameObject.Find("Player 3");
        players[3] = GameObject.Find("Player 4");
        
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
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = 1;
        currentPlayer = 1;
        loopPosition = 4;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numPlayers!= 1)
        {
            //currently working on it
        }
        else
        {
            playerMovement = players[0].GetComponent<PlayerMovement>();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int movement = Random.Range(1, 7);
                playerMovement.isRun = true;
                playerMovement.loopPosition = loopPosition;
                playerMovement.movementPoints = movement;
                playerMovement.playerAnimator.SetTrigger("Run");
                playerMovement.newWP = playerMovement.nextWaypoint + movement;
            }
        }

    }
}
