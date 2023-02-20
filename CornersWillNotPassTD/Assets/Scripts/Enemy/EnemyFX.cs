using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpownPosition;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void EnemyHit(Enemy enemy, float damage)
    {
        if (_enemy == enemy)
        {
            GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanseFromPool();
            TextMeshProUGUI damageText = newInstance.GetComponent<DamageText>().DmgText;
            damageText.text = damage.ToString();

            newInstance.transform.SetParent(textDamageSpownPosition);
            newInstance.transform.position = textDamageSpownPosition.position;
            newInstance.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Projectile.OnEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        Projectile.OnEnemyHit -= EnemyHit;
    }
}
