using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public int currentAmmo;
    public List<string> defeatedEnemies = new List<string>();
}
