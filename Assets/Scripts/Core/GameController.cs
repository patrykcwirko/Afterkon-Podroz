using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class GameCamera 
    { 
        public CameraFollow cameraFollow;
        public Vector3 offset;
    }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] public bool doubleJumpEvable;
    [SerializeField] public bool stompEnable;
    [SerializeField] public bool dashEnable;
    [SerializeField] private GameCamera gameCamera;
    [SerializeField] private Transform currentCheckPoint;
    [SerializeField] public Weapon[] weapons;

    [HideInInspector] public int weaponIndex = 0;
    private HeartsHealthVisual healthVisual;
    private PotionSystemVisual potionSystem;

    void Awake()
    {
        GameObject player = Instantiate(playerPrefab, currentCheckPoint.position, currentCheckPoint.rotation);
        player.GetComponent<Player.PlayerCombat>().sword = weapons[0];
        healthVisual = FindObjectOfType<HeartsHealthVisual>();
        potionSystem = FindObjectOfType<PotionSystemVisual>();
        FindObjectOfType<Player.PlayerCombat>().sword = weapons[weaponIndex];
        gameCamera.cameraFollow.Setup(() => player.transform.position + gameCamera.offset);
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
