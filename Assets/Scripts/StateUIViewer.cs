using TMPro;
using UnityEngine;

public class StateUIViewer : MonoBehaviour
{
    private TextMeshProUGUI TMPro;

    void Start()
    {
        TMPro = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateStateUI(string data)
    {
        TMPro.text = data;
    }
}
