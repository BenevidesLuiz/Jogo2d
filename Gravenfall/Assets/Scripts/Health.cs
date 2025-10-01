using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour {

    
    public float damageCooldown = 2f;
    private bool canTakeDamage = true;
    [SerializeField]
    private int _maxhp = 100;
    [SerializeField]
    private int _hp;

    public int MaxHp => _maxhp;

    public int Hp {
        get => _hp;

        private set {
            var isDamage = value < _hp;
            _hp = Mathf.Clamp(value, min: 0, _maxhp);
            if (isDamage) {
                Damaged?.Invoke(_hp);
            }
            else {
                Healed?.Invoke(_hp);
            }

            if (_hp <= 0) {
                Died?.Invoke();
            }

        }

    }

    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;

    private void Awake() {
        _hp = _maxhp;
    }

    public void Damage(int amount)
    {
        if (canTakeDamage)
        {
            Hp -= amount;
            StartCoroutine(DamageCooldownRoutine());
        }
    }
    private IEnumerator DamageCooldownRoutine()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    public void Heal(int amount) => Hp += amount;

    public void HealFull() => Hp = _hp;

    public void kill() => Hp = 0;

    public void Adjust(int value){
        Hp = value;
    }





}
