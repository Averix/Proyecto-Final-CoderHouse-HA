using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera[] virtualCamera;
    [SerializeField] public GameObject camFollow;
    [SerializeField] public GameObject camLookAt;
    // Gamemanager instance
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        gameManager.onBossFight += CameraChange;
    }

    // Update is called once per frame
    void Update()
    {
        virtualCamera[0].Follow = camFollow.transform;
        virtualCamera[0].LookAt = camLookAt.transform;
    }
    
    private void CameraChange(bool active)
    {
        if (active)
        {
            virtualCamera[0].gameObject.SetActive(false);
            virtualCamera[1].gameObject.SetActive(true);
        }
    }
}
