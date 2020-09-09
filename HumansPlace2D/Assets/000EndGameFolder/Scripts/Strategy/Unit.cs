using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Unit 
{
    private IClosestUnitFinder enemyFinder;
    private Func<Unit, bool> enemyChecker;

    public Unit(IClosestUnitFinder EnemyFinder, Func<Unit, blood> EnemyChecker)
    {
        enemyFinder = EnemyFinder;
        enemyChecker = EnemyChecker;
        
    }

    void Update()
    {
        var enemy = enemyFinder.Find();
        if (enemy != null && enemyChecker(enemy))
            Attempt(enemy);
    }
    protected abstract void Attempt(Unit unit);
}

interface IClosestUnitFinder
{
    Unit Find();
}
interface Func<T> : IDamage
{
    
}

public interface IKillable
{
    void Kill();
}

public interface IDamageable<T>
{
    void Damage(T damageTakin);
}