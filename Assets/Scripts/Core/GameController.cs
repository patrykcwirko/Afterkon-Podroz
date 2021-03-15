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
    [SerializeField] public bool stompEnable;
    [SerializeField] public bool dashEnable;
    [SerializeField] GameCamera gameCamera;
    [SerializeField] Transform currentCheckPoint;

    private HeartsHealthVisual healthVisual;
    private PotionSystemVisual potionSystem;

    void Start()
    {
        healthVisual = FindObjectOfType<HeartsHealthVisual>();
        potionSystem = FindObjectOfType<PotionSystemVisual>();
        gameCamera.cameraFollow.Setup(() => gameCamera.playerTrasform.position + gameCamera.offset);
        healthVisual.onDeath += Game_OnDeath;
    }

    private void Game_OnDeath(object sender, EventArgs e)
    {
        var player = FindObjectOfType<Player.PlayerMovement>().gameObject;
        player.gameObject.transform.SetPositionAndRotation(currentCheckPoint.position, currentCheckPoint.rotation);
        healthVisual.GetHeartSystem().Heal(100);
        potionSystem.ClearPotionList();
    }

    public void SetCheckPoint(Transform checkPoint)
    {
        currentCheckPoint = checkPoint;
    }
}
