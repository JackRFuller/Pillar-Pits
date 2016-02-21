using UnityEngine;
using System.Collections;

public class GameTargets : EnemyBaseClass {	

    public override void Hit(float _damage)
    {
        LevelManager.instance.DecrementTarget();
        base.Hit( _damage);
    }
}
