using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Manager : MonoBehaviour
{
    public static int hp;
    public float Full_Health = 100;
    public float Health = 100;
    public float Rest = 0;


    public GameObject Hp_parent;

    public HP_Heart[] life;

    public CharacterMovement character;


    void Start()
    {
        Hp_parent = this.transform.gameObject;
        life = Hp_parent.GetComponentsInChildren<HP_Heart>();
        character = GameObject.FindObjectOfType<CharacterMovement>();
    }

    public void Damaged(int _damage)
    {
        character.is_Beat = true;
        for (int i = life.Length - 1; i >= 0; i--)
        {
            if (life[i].Heart_Health > 0)
            {
                if (life[i].Heart_Health - _damage <= 0)
                {
                    if (i != 0)
                    {
                        int Rest = _damage - life[i].Heart_Health;
                        life[i].Heart_Health = 0;
                        life[i - 1].Heart_Health -= Rest;
                        break;
                    }
                    else if (i == 0)
                    {
                        life[i].Heart_Health = 0;
                        //Debug.Log("사망");
                        character.DIE();
                    }


                }
                else if (life[i].Heart_Health - _damage > 0)
                {
                    life[i].Heart_Health -= _damage;
                    break;
                }
            }
            else if (life[i].Heart_Health == 0)
            {
                continue;
            }

        }

        for (int i = 0; i < life.Length; i++)
        {
            Health_Status(i, life[i].Heart_Health, "D");
        }


        StartCoroutine(character.OnBeatTime());



    }

    public void Heal(int _heal)
    {
        for (int i = 0; i < life.Length; i++)
        {
            if (life[i].Heart_Health < 100)
            {
                if (life[i].Heart_Health + _heal >= 100)
                {
                    if (i != life.Length - 1)
                    {
                        int Rest = (life[i].Heart_Health + _heal) - 100;
                        life[i].Heart_Health = 100;
                        life[i + 1].Heart_Health += Rest;
                        break;
                    }
                    else if (i == life.Length - 1)
                    {
                        life[i].Heart_Health = 100;
                        break;
                    }
                }
                else if (life[i].Heart_Health + _heal < 100)
                {
                    life[i].Heart_Health += _heal;
                    break;
                }
            }
            else if (life[i].Heart_Health == 100)
            {
                continue;
            }
        }

        for (int i = 0; i < life.Length; i++)
        {
            Health_Status(i, life[i].Heart_Health, "H");
        }




    }


    public void Health_Status(int index, float health, string _what) //what은 D랑 H으로 데미지인지 회복인지 구분할거임
    {
        life[index].hp_Heart.fillAmount = (health / Full_Health);
    }

}
