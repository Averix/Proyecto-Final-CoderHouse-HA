using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    [SerializeField] private Rigidbody markerRB;
    [SerializeField] public Renderer markerMat;
    [SerializeField] public GameObject[] markerGO;
    [SerializeField] public int markerIndex;
    [SerializeField] public bool markerEN = true;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        markerGO = GameObject.FindGameObjectsWithTag("Markers");
        markerRB = markerGO[markerIndex].GetComponent<Rigidbody>();
        markerMat = markerGO[markerIndex].GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (markerEN)
        {
            markerGO[markerIndex].SetActive(true);
            markerRB.AddTorque(Vector3.up);
        }
        
        else
        {
            markerGO[markerIndex].SetActive(false);
        } 
    }
}
