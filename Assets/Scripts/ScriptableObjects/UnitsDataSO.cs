using UnityEngine;

[CreateAssetMenu(fileName = "UnitsData", menuName = "ScriptableObjects/UnitsDataSO", order = 1)]
public class UnitsDataSO : ScriptableObject
{
    public string unitsName;
    public int cost;

    public float movementSpeed;

    public float maxHealth;

    public float damage;
    public float attackSpeed;
    public float range;

    public AttackType attackType;

    public GameObject ghostPrefab;
    public GameObject unitBluePrefab;
    public GameObject unitRedPrefab;
    
    public AudioClip invocationSound;
    public AudioClip attackSound;
    public AudioClip deathSound;

    //Se puede agregar particulas de Ataque/Invocacion/Muerte

   public static float spawnCooldown = 3;
}
