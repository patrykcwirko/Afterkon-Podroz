using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public bool doubleJumpEvable;
    [SerializeField] public bool stompEvable;
    [SerializeField] Transform playerTrasform;
    public CameraFollow cameraFollow;


    // Start is called before the first frame update
    void Start()
    {
        cameraFollow.Setup(() => playerTrasform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
