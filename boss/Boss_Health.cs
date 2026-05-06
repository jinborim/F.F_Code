using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    public float Full_Health = 150f;
    public float Health = 150f;

    [SerializeField] private GameObject boss_health_parent;
    [SerializeField] private Image healthBar;

    [SerializeField] private GameObject boss;
    [SerializeField] private Boss_Movement boss_;

    void Start()
    {
      if (boss_health_parent == null)
            boss_health_parent = gameObject;

        if (healthBar == null && boss_health_parent.transform.childCount > 0)
        {
            Transform uiRoot = boss_health_parent.transform.GetChild(0);
            Transform hp = uiRoot.Find("BossHP");

            if (hp != null)
                healthBar = hp.GetComponentInChildren<Image>();
        }

        if (boss == null)
            boss = GameObject.Find("Boss");

        if (boss_ == null)
            boss_ = FindObjectOfType<Boss_Movement>();

        if (boss_health_parent.transform.childCount > 0)
            boss_health_parent.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void bossDamaged(int _damage)
    {
   if (boss_ == null || healthBar == null) return;

        boss_.Boss_is_Beat = true;

        if (Health <= _damage)
        {
            Health = 0f;
            boss_.BOSSDIE();
            boss_health_parent.SetActive(false);
        }
        else
        {
            Health -= _damage;

            if (boss != null)
                StartCoroutine(boss_.BossOnBeatTime());
        }

        healthBar.fillAmount = Health / Full_Health;
        boss_.RestHealth = Health;
    }
}
