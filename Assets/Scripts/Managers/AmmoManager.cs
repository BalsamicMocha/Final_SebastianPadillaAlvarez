using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    [Header("Ammo Settings")]
    public int maxAmmo = 10;
    public int currentAmmo;

    [Header("UI")]
    public TMP_Text ammoText;

    void Awake()
    {
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    public void UseAmmo(int amount = 1)
    {
        currentAmmo -= amount;
        if (currentAmmo < 0) currentAmmo = 0;
        UpdateUI();
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + maxAmmo;
        }
    }
}

