using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{

    [SerializeField] private Transform projectileSpownPosition;
    [SerializeField] private float delayBtwAttacks = 2f;

    private float _nextAttackTime;
    private ObgectPooler _pooler;
    private Turret _turret;
    private Projectile _currentProjectileLoaded;

    private void Start()
    {
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObgectPooler>();
        LoadProjectile();
    }

    private void Update()
    {

        if (IsTurretEmpty())
        {
            LoadProjectile();
        }

        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + delayBtwAttacks;
        }


    }

    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanseFromPool();
        newInstance.transform.localPosition = projectileSpownPosition.position;
        newInstance.transform.SetParent(projectileSpownPosition);
        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        _currentProjectileLoaded.TurretOwnner = this;
        _currentProjectileLoaded.ResetProjectile();
        newInstance.SetActive(true);
    }

    private bool IsTurretEmpty()
    {
        return _currentProjectileLoaded == null;
    }

    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }

}
