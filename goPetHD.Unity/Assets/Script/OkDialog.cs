using UnityEngine;
using UnityEngine.UI;

public class OkDialog : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button okButton;
    void Start()
    {
        this.okButton = this.GetComponentInChildren<Button>();
        this.okButton.onClick.AddListener(() =>
        {
            UICamera.Instance.okDialog.enabled = false;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
