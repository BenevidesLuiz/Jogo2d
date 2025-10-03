using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButtonInOptions : MonoBehaviour
{
    public GameObject backToMenuButton;
    public GameObject goToOptions;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenOptions() {
        EventSystem.current.SetSelectedGameObject(goToOptions);

    }
    public void CloseOptions()
    {
        EventSystem.current.SetSelectedGameObject(backToMenuButton);

    }
}
