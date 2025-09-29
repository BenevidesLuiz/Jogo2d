using UnityEngine;

public class BossHP
{
    private int hp;

    public BossHP(int hp) {
        this.hp = hp;
    }
    public void Damage(int damage)
    {
        hp -= damage;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }
    public int getHp() {
        return hp;
    }
}
