using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IAttacker
{
    public int Damage { get; set; }
    public float Range { get; set; }
    
    [SerializeField] private float explosionRange, explosionForce;
    [SerializeField] private int bulletDamage;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject projectileParticle;
    
    private GameObject _currentExplosionEffect;

    private void Start()
    {
        Damage = bulletDamage;
        Range = explosionRange;
        
        projectileParticle = GameManager.Instance.poolManager.CheckPool(projectileParticle);
        projectileParticle.transform.position = transform.position;
        projectileParticle.transform.rotation = transform.rotation;
        projectileParticle.transform.parent = transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Exploder"))
        {
            // Debug.Log("Explode");
            Attack();
            gameObject.SetActive(false);
            _currentExplosionEffect = GameManager.Instance.poolManager.CheckPool(explosionEffect);
            _currentExplosionEffect.transform.position = transform.position;
            //_currentExplosionEffect.transform.rotation = transform.rotation;
            _currentExplosionEffect.SetActive(true);
        }
    }

    public void Attack()
    {
        Collider[] objInHitArea = Physics.OverlapSphere(transform.position, Range);

        if (objInHitArea.Length > 0)
        {
            foreach (var item in objInHitArea)
            {
                var damageableObj = item?.GetComponent<IDamageable>();
                if (damageableObj == null || item.transform == transform || item.gameObject.name == "Cannon") continue;

                damageableObj.ReceivedDamage(Damage);
                damageableObj.Hit(explosionForce, transform.position, Range);
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, Range);
    }*/
}