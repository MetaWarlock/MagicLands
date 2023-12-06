using TMPro;
using UnityEngine;

public class StateUIViewer : MonoBehaviour
{
    private TextMeshProUGUI TMPro;

    private void Awake()
    {
        TMPro = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateStateUI(string data)
    {
        TMPro.text = data;
    }
}
