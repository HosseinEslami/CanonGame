using System.Collections;
using UnityEngine;

public abstract class Zombie : MonoBehaviour, IDamageable, IAttacker
{
    public int Damage { get; set; }
    public int Health { get; set; }
    public float Range { get; set; }
    public bool zAttacked;

    protected float Speed;

    [HideInInspector] public Color originalColor;

    private ZombieNavMesh _navMesh;
    private RagDollDeath _ragDollDeath;
    private Material _material;
    private Animator _animator;
    private Collider _collider;

    private void OnEnable()
    {
        if (_material)//Not First Time
        {
            _material.color = originalColor;
            _collider.enabled = true;
        }
        GameManager.GameOver.AddListener(GameOver);
        zAttacked = false;
        
    }

    private void OnDisable()
    {
        GameManager.GameOver.RemoveListener(GameOver);
    }

    private void GameOver(bool isWinner)
    {
        if(isWinner) return;
        _navMesh.StopMoving();
        _animator.SetTrigger("Scream");
    }

    public virtual void Start()
    {
        _navMesh = GetComponent<ZombieNavMesh>();
        _ragDollDeath = GetComponent<RagDollDeath>();
        _material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        originalColor = _material.color;
        _navMesh.speed = Speed;
    }

    public virtual void ReceivedDamage(int damageAmount)
    {
        Health -= damageAmount;
    }

    public virtual void Hit(float force, Vector3 explosionPos, float range)
    {
        if (Health <= 0)
        {
            _navMesh.StopMoving();
            _ragDollDeath.Die(force, explosionPos, range);
            _collider.enabled = false;
            StartCoroutine(DisappearAndDisabler(4f, 0.005f));
        }
    }

    public virtual void Attack()
    {
        Collider[] objInHitArea = Physics.OverlapSphere(transform.position, Range + 10f);

        if (objInHitArea.Length > 0)
        {
            foreach (var item in objInHitArea)
            {
                // Debug.Log(item.name);
                var damageableObj = item?.GetComponent<IDamageable>();
                if (damageableObj == null || item.transform == transform ||
                    item.gameObject.CompareTag("Exploder")) continue;

                damageableObj.ReceivedDamage(Damage);
                damageableObj.Hit(0, Vector3.zero, 0);
            }
        }

        _navMesh.StopMoving();
        _animator.SetTrigger("Attack");
        _collider.enabled = false;
        StartCoroutine(DisappearAndDisabler(6.5f, 0.001f));
    }

    private IEnumerator DisappearAndDisabler(float waitTime, float disappearSpeed)
    {
        for (float t = 0f; t < waitTime; t += Time.deltaTime)
        {
            if(t > waitTime * 0.5f) _ragDollDeath.ToggleRagDoll(false, false);
            Color tmpColor = _material.color;
            tmpColor.a = tmpColor.a - disappearSpeed;
            _material.color = tmpColor;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}