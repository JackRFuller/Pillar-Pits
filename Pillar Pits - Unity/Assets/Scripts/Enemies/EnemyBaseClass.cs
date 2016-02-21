using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyBaseClass : MonoBehaviour {

    [Header("Health Attributes")]
    public float health;

    public virtual void Hit(float _damage)
    {
        //Inflict Damage
        health -= _damage;

        if (health <= 0)
            Death();
    }

    void Death()
    {
        gameObject.SetActive(false);
    }
}
