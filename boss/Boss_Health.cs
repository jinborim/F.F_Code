using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    public float Full_Health = 150;
    public float Health = 150;

    public GameObject boss_health_parent;
    public Image healthBar;

    public GameObject boss;

    public Boss_Movement boss_;


    // Start is called before the first frame update
    void Start()
    {
        boss_health_parent = this.transform.gameObject;
        healthBar = boss_health_parent.transform.GetChild(0).transform.Find("BossHP").GetComponentInChildren<Image>();
        boss = GameObject.Find("Boss");
        boss_ = GameObject.FindObjectOfType<Boss_Movement>();
        boss_health_parent.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void bossDamaged(int _damage)
    {
        boss_.Boss_is_Beat = true;
        if (Health - _damage <= 0)
        {
            Health = 0;
            boss_.BOSSDIE();
            boss_health_parent.gameObject.SetActive(false);
        }
        else if (Health - _damage > 0)
        {
            Health -= _damage;
            if (boss != null)
            {
                StartCoroutine(boss_.BossOnBeatTime());
            }
        }
        healthBar.fillAmount = (Health / Full_Health);
        boss_.RestHealth = Health;
    }
}
