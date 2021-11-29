using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    // item RigidBody
    [SerializeField] private Rigidbody itemRB;
    // Inventory Manager
    [SerializeField] private GameObject inventoryManager;
    [SerializeField] private InventoryManager instance;
    // Game Manager
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameManager gmInstance;

    // Start is called before the first frame update
    void Start()
    {
        itemRB = gameObject.GetComponent<Rigidbody>();
        inventoryManager = GameObject.Find("InventoryManager");
        instance = inventoryManager.GetComponent<InventoryManager>();
        gameManager = GameObject.Find("GameManager");
        gmInstance = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        itemRB.AddTorque(Vector3.up);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            instance.item = gameObject;
            instance.AddItem();
            gmInstance.CoinSound();
            Destroy(gameObject);
        }
    }
}
