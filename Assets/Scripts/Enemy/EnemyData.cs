using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int startHealth;
    public int attackDamage;
    public int goldValue;
    public int expValue;
    public float disappearSpeed;

    public string ExpPath;
}
