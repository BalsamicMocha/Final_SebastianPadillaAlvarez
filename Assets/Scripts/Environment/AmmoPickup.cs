using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 5;    

    private void OnTriggerEnter(Collider other)
    {
        AmmoManager ammoManager = other.GetComponentInChildren<AmmoManager>();
        if (ammoManager != null)
        {
            ammoManager.AddAmmo(ammoAmount);
            AudioManager.Instance.Play("Pickup");
            Destroy(gameObject);
        }
    }
}
