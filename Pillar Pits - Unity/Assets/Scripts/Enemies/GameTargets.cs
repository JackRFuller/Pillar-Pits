using UnityEngine;
using System.Collections;

public class GameTargets : EnemyBaseClass
{	

    void Start()
    {
        ResetManager.ResetLevel += Reset;
    }

    public override void Hit(float _damage)
    {
        LevelManager.instance.DecrementTarget();
        base.Hit( _damage);
    }

    void Reset()
    {
        gameObject.SetActive(true);
    }
}
