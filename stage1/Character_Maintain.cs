using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Maintain : MonoBehaviour
{
    public static Character_Maintain Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
