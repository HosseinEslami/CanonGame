using UnityEngine;

public interface IDamageable
{
    int Health { get; set; }
    void ReceivedDamage(int damageAmount);
    void Hit(float force, Vector3 explosionPos, float range);
}
