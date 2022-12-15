using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{

    [SerializeField] private Transform projectileSpownPosition;

    private ObgectPooler _pooler;
    private Turret _turret;
    private Projectile _currentProjectileLoaded;

    private void Start()
    {
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObgectPooler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadProjectile();
        }

        if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
        {
            _currentProjectileLoaded.transform.parent = null;
            _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
        }
    }

    private void LoadProjectile()
    {
        GameObject newInstance = _pooler.GetInstanseFromPool();
        newInstance.transform.localPosition = projectileSpownPosition.position;
        newInstance.transform.SetParent(projectileSpownPosition);
        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        newInstance.SetActive(true);
    }
}
