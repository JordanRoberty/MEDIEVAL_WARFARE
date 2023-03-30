using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : Singleton<StatsManager>
{
    public int score { get; private set; }
    public int pieces { get; private set; }
    public int kills { get; private set; }
    public int total_enemies { get; private set; }
    public int hearts { get; private set; }
    public int damage_taken { get; private set; }
    public float damage_done { get; private set; }
    public int jumps { get; private set; }

    void Start()
    {
        init();
    }

    public void init()
    {
        score = 0;
        pieces = 0;
        kills = 0;
        total_enemies = 0;
        hearts = 0;
        damage_taken = 0;
        damage_done = 0f;
        jumps = 0;
    }

    public void update_score(int point)
    {
        score += point;
    }

    public void update_pieces()
    {
        ++pieces;
    }

    public void update_kills()
    {
        ++kills;
    }

    public void update_total_enemies()
    {
        ++total_enemies;
    }

    public void update_hearts()
    {
        ++hearts;
    }

    public void update_damage_taken()
    {
        ++damage_taken;
    }

    public void update_damage_done(float damage)
    {
        damage_done += damage;
    }

    public void update_jumps()
    {
        ++jumps;
    }
}
