using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string SelectedCharacter;
    public Texture2D SelectedCharacterTexture;
    public static List<GameObject> Enemy=new List<GameObject>();
    public static List<GameObject> Coin = new List<GameObject>();
    public WeaponData SelectedWeapon;
    public static void addEnemy(GameObject enemy)
    {
        Enemy.Add(enemy);
    }
    public static void delEnemy(GameObject enemy)
    {
        Enemy.Remove(enemy);
    }
    
    public static void addCoin(GameObject enemy)
    {
        Coin.Add(enemy);
    }
    public static void delCoin(GameObject enemy)
    {
        Coin.Remove(enemy);
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // không bị phá khi load scene mới
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
