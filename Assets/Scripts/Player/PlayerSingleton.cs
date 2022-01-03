using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    // PlayerSingleton instance
    public static PlayerSingleton instance;
    // Gamemanager instance
    private GameManager gameManager;
    // Players Array
    [SerializeField] private GameObject[] players;
    // Amount of players
    [SerializeField] [Range(1, 4)] public int numPlayers;
    // Player position flag
    [SerializeField] private bool playerRearrange = false;
    // Menu data Transfer
    private MenuDataTransfer dataInstance;

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
            dataInstance = GameObject.Find("Menu Data Transfer").GetComponent<MenuDataTransfer>();
            numPlayers = dataInstance.numPlayers;
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        gameManager.onBossFight += OnBossFightHandler;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (playerRearrange == true)
        {
            gameManager.onBossFight -= OnBossFightHandler;
        }
    }

    public void OnBossFightHandler(bool active)
    {
        if (active && playerRearrange == false)
        {
            switch (numPlayers)
            {
                case 1:
                    players[0].transform.position = new Vector3(-3.84f, 0, -5) + new Vector3 (88, 2.7f, 69);
                    break;
                case 2:
                    players[0].transform.position = new Vector3(-3.84f, 0, -5) + new Vector3(88, 2.7f, 69);
                    players[1].transform.position = new Vector3(-2.34f, 0, -3) + new Vector3(88, 2.7f, 69);
                    break;
                case 3:
                    players[0].transform.position = new Vector3(-3.84f, 0, -5) + new Vector3(88, 2.7f, 69);
                    players[1].transform.position = new Vector3(-2.34f, 0, -3) + new Vector3(88, 2.7f, 69);
                    players[2].transform.position = new Vector3(-2.34f, 0, 0) + new Vector3(88, 2.7f, 69);
                    break;
                case 4:
                    players[0].transform.position = new Vector3(-3.84f, 0, -5) + new Vector3(88, 2.7f, 69);
                    players[1].transform.position = new Vector3(-2.34f, 0, -3) + new Vector3(88, 2.7f, 69);
                    players[2].transform.position = new Vector3(-2.34f, 0, 0) + new Vector3(88, 2.7f, 69);
                    players[3].transform.position = new Vector3(-3.84F, 0, 3) + new Vector3(88, 2.7f, 69);
                    break;
                default:
                    break;
            }
            playerRearrange = true;
        }
    }
}
