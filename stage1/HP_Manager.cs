using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Manager : MonoBehaviour
{
   public HP_Heart[] hearts;
    public CharacterMovement character;

    private void Start()
    {
        hearts = GetComponentsInChildren<HP_Heart>();
        character = FindObjectOfType<CharacterMovement>();
    }

    // 데미지 처리
    public void Damaged(int damage)
    {
        if (damage <= 0) return;

        character.is_Beat = true;

        // 뒤에서부터 체력 차감
        for (int i = hearts.Length - 1; i >= 0; i--)
        {
            if (damage <= 0) break;

            int current = hearts[i].currentHealth;

            if (current > 0)
            {
                if (current >= damage)
                {
                    hearts[i].SetHealth(current - damage);
                    damage = 0;
                }
                else
                {
                    damage -= current;
                    hearts[i].SetHealth(0);
                }
            }
        }

        UpdateAllUI();

        if (IsDead())
        {
            character.DIE();
            return;
        }

        StartCoroutine(character.OnBeatTime());
    }

    // 회복 처리
    public void Heal(int heal)
    {
        if (heal <= 0) return;

        // 앞에서부터 회복
        for (int i = 0; i < hearts.Length; i++)
        {
            if (heal <= 0) break;

            int current = hearts[i].currentHealth;

            if (current < hearts[i].maxHealth)
            {
                int need = hearts[i].maxHealth - current;

                if (heal >= need)
                {
                    hearts[i].SetHealth(hearts[i].maxHealth);
                    heal -= need;
                }
                else
                {
                    hearts[i].SetHealth(current + heal);
                    heal = 0;
                }
            }
        }

        UpdateAllUI();
    }

    // 사망 체크
    private bool IsDead()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i].currentHealth > 0)
                return false;
        }
        return true;
    }

    // UI 전체 갱신
    private void UpdateAllUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetHealth(hearts[i].currentHealth);
        }
    }
}
