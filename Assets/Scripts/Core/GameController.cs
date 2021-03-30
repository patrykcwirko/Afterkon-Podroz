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
    [SerializeField] public Weapon[] weapons;

    [HideInInspector] public int weaponIndex = 0;
    private HeartsHealthVisual healthVisual;
    private PotionSystemVisual potionSystem;

    void Awake()
    {
        healthVisual = FindObjectOfType<HeartsHealthVisual>();
        potionSystem = FindObjectOfType<PotionSystemVisual>();
        FindObjectOfType<Player.PlayerCombat>().sword = weapons[weaponIndex];
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
    public void SwitchWeapon()
    {
        var player = FindObjectOfType<Player.PlayerCombat>();
        weaponIndex++;
        if (weaponIndex < weapons.Length)
        {
            player.sword = weapons[weaponIndex];
            player.sword.Setup(player);
        }
        else
        {
            weaponIndex = 0;
            player.sword = weapons[weaponIndex];
            player.sword.Setup(player);
        }
    }
}
