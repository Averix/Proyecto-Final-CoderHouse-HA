using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera virtualCamera;
    [SerializeField] public GameObject camFollow;
    [SerializeField] public GameObject camLookAt;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        virtualCamera.Follow = camFollow.transform;
        virtualCamera.LookAt = camLookAt.transform;
    }
}
