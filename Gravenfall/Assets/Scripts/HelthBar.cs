using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private RectTransform _barRect;
    [SerializeField] private RectMask2D _mask;
    [SerializeField] private TMP_Text _hpIndicator;

    private float _maxRightMask;
    private float _initialRightMask;

    private void Start(){
        // calcula o tamanho máximo da máscara
        _maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z;
        _initialRightMask = _mask.padding.z;

        // atualiza a barra no início
        UpdateBar(health.Hp);

        // se inscreve nos eventos
        health.Damaged.AddListener(UpdateBar);
        health.Healed.AddListener(UpdateBar);
    }

    private void UpdateBar(int currentHp){
        // atualiza o texto
        _hpIndicator.SetText($"{currentHp}/{health.MaxHp}");

        // atualiza a barra (reduzindo máscara de acordo com a vida)
        float ratio = (float)currentHp / health.MaxHp;
        _mask.padding = new Vector4(
            _mask.padding.x,
            _mask.padding.y,
            _initialRightMask + _maxRightMask * (1 - ratio),
            _mask.padding.w
        );
    }

}
