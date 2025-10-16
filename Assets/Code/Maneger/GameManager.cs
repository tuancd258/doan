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
    public static void addToList<T>(List<T> list,T obj)
    {
        list.Add(obj);
    }
    public static void removeFormList<T>(List<T> list,T obj)
    {
        list.Remove(obj);
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
