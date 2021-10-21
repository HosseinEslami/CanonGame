using UnityEngine;

public class BasicZombie : Zombie
{
    [SerializeField] private float resistance, walkSpeed, attackRange;
    [SerializeField] private int health, damage;

    public override void Start()
    {
        Health = health;
        Speed = walkSpeed;
        Damage = damage;
        Range = attackRange;
        base.Start();
    }

    public override void ReceivedDamage(int damageAmount)
    {
        base.ReceivedDamage(damageAmount);
    }

    public override void Attack()
    {
        base.Attack();
        zAttacked = true;
    }

    public override void Hit(float force, Vector3 explosionPos, float range)
    {
        base.Hit(force, explosionPos / resistance, range);
    }
}