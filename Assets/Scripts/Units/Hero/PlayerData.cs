using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [HideInInspector]public Rigidbody2D rb;

    [Header("Health & score")]
    public int health = 100;
    public int score;
    public int nb_coins;
    //RUNES ??

    //[Header("Weapon")]
    //WEAPON???

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
