using UnityEngine;

public class UIManager : MonoBehaviour{
    [SerializeField] GameObject deathPanel;

    public void ToogleDeathPanel(){
        deathPanel.SetActive(!deathPanel.activeSelf); 
    }
}
