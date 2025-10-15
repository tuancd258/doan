using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStarsData", menuName = "Scriptable Objects/PlayerStarsData")]
public class PlayerStarsData : ScriptableObject
{
    public float maxHP = 100f;
    public float armor = 0f;
    public float dodgeChance = 0f;
    public float moveSpeed = 5f;
    public int leves = 1;

    public float meleeDamage = 10f;
    public float rangedDamage =8f;
    public float attackSpeed = 1f;
    public float dameMultiplier = 0f;
    public float criticalChance = 0f;
    public float Range = 1f;
    public float lifeSteal = 0;
    public float healPerHit = 0f;
    public float money =0f;
}
