using System;
using UnityEngine;

public class CannonController : MonoBehaviour, IDamageable
{
    public Transform shootPoint;
    public float force;

    [SerializeField] private float rotXSpeed, rotYSpeed, fireRate;
    [SerializeField] private int health;
    [SerializeField] private GameObject bullet, barrelParticle;
    [SerializeField] private Transform barrel;

    private float _horizontalRot, _verticalRot, _timer;
    private GameObject _currentBullet;

    public int Health { get; set; }

    private void Start()
    {
        Health = health;
    }

    private void Update()
    {
        if(GameManager.Instance.gameOver) return;
        
        _horizontalRot += Input.GetAxis("Mouse X") * rotXSpeed * -Time.deltaTime;
        _horizontalRot = Mathf.Clamp(_horizontalRot, -75, 75);
        
        transform.localRotation = Quaternion.AngleAxis(_horizontalRot, Vector3.forward);
        
        _verticalRot += Input.GetAxis("Mouse Y") * rotYSpeed * -Time.deltaTime;
        _verticalRot = Mathf.Clamp(_verticalRot, -3, 25);
        barrel.transform.localRotation = Quaternion.AngleAxis(_verticalRot, Vector3.up);

        _timer -= Time.deltaTime;
        
        if (Input.GetButtonUp("Fire1") && _timer < 0)
        {
            _currentBullet = GameManager.Instance.poolManager.CheckPool(bullet);
            _currentBullet.transform.position = shootPoint.position;
            _currentBullet.transform.rotation = shootPoint.rotation;
            _currentBullet.SetActive(true);

            _currentBullet.GetComponent<Rigidbody>().velocity = shootPoint.up * force;
            
            barrelParticle.SetActive(true);
            _timer = fireRate;
        }
    }

    public void Hit(float force, Vector3 explosionPos, float range) { }

    public void ReceivedDamage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health <= 0) GameOver();
    }

    private void GameOver()
    {
        GameManager.GameOver?.Invoke(false);
    }
}