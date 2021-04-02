using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public bool isActivable;
    public Activator activator;
    public GameObject arrowPref;
    public int shotRate;

    private bool isActive;
    private Transform shotPoint;
    private int currentTime;

    void Start()
    {
        activator.onTriger += Activator_onTriger;
        TimeTickSystem.onTick += TimeTickSystem_onTick;
        shotPoint = transform.Find("shotPoint");
    }

    private void TimeTickSystem_onTick(object sender, TimeTickSystem.onTickEventArgs e)
    {
        currentTime++;
    }

    private void Activator_onTriger(object sender, System.EventArgs e)
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivable)
        {
            if (!isActive) return;
            Shoot();
        }
        else
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (currentTime < shotRate) return;
        currentTime = 0;
        GameObject projectile = Instantiate(arrowPref, shotPoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().direction = Vector2.right;

    }
}
