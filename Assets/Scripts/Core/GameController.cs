using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class GameCamera 
    { 
        public Transform playerTrasform;
        public CameraFollow cameraFollow;
        public Vector3 offset;
    }


    [SerializeField] public bool doubleJumpEvable;
    [SerializeField] public bool stompEvable;
    [SerializeField] GameCamera gameCamera;


    // Start is called before the first frame update
    void Start()
    {
        gameCamera.cameraFollow.Setup(() => gameCamera.playerTrasform.position + gameCamera.offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
