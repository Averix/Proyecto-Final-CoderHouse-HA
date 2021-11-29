using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // GameManager instance
    public static InventoryManager instance;

    // Indicidual Lists for object types
    [SerializeField] public List<GameObject> itemList;
    //[SerializeField] public List<GameObject> silverCoinList;
    // item to be added or substracted from inventory
    [SerializeField] public GameObject item;
    // Current player ID
    [SerializeField] public int currentPlayer;
    // Current player currency
    [SerializeField] public int playerCurrency;

    // Dictionary acts as the separator for players inventories. The key corresponds to the current player
    // Similar to a bank account where the key represents the clients account
    [SerializeField] public Dictionary<int, List<GameObject>> playerItems;

    void Awake()
    {
        // Singleton method
        if (instance == null)
        {
            instance = this;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem()
    {
        itemList.Add(item);
    }
    //public void AddSilverCoins()
    //{
    //    silverCoinList.Add(item);
    //}
    public void GetItem()
    {
        itemList.Remove(item);
    }
    //public void GetSilverCoins()
    //{
    //    silverCoinList.Remove(item);
    //}
}
