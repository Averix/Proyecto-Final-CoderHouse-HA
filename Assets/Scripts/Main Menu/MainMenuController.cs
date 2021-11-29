using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Menu sections to be navigated
    [SerializeField] private GameObject[] menuSection;
    // Previous and current menu container for menu navigation
    [SerializeField] private GameObject previousMenu;
    [SerializeField] private GameObject currentMenu;
    // Instance of MenuDataTransfer to send data through to the next scene
    [SerializeField] private MenuDataTransfer menuDataInstance;

    private void Awake()
    {
        // Disables all menu sections
        for (int i = 0; i < menuSection.Length; i++)
        {
            menuSection[i].SetActive(false);
        }
        // Enables the Main menu first section
        menuSection[0].SetActive(true);
        // Gets the instance for MenuDataTransfer
        menuDataInstance = GameObject.Find("Menu Data Transfer").GetComponent<MenuDataTransfer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Access New Game section
    public void NewGame()
    {
        menuSection[0].SetActive(false);
        menuSection[1].SetActive(true);
        previousMenu = menuSection[0];
        currentMenu = menuSection[1];
    }
    // Starts a new game and transfer the selected amount of players
    public void StartNewGame(int numPlayers)
    {
        menuDataInstance.PlayerAssign(numPlayers);
        SceneManager.LoadScene(1);
    }
    // Access the Minigames Section
    public void Minigames()
    {
        menuSection[0].SetActive(false);
        menuSection[2].SetActive(true);
        previousMenu = menuSection[0];
        currentMenu = menuSection[2];
    }
    // Access the in-game Store to unlock game content
    public void Store()
    {
        menuSection[0].SetActive(false);
        menuSection[3].SetActive(true);
        previousMenu = menuSection[0];
        currentMenu = menuSection[3];
    }
    // Access settings section
    public void Settings()
    {
        menuSection[0].SetActive(false);
        menuSection[4].SetActive(true);
        previousMenu = menuSection[0];
        currentMenu = menuSection[4];
    }
    // Return to previous section
    public void Return()
    {
        currentMenu.SetActive(false);
        previousMenu.SetActive(true);
        currentMenu = previousMenu;
    }
    // Closes the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
