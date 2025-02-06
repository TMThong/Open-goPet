using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button loginButton;
    private InputField username, password;
    void Start()
    {
        this.loginButton = this.GetComponentInChildren<Button>();
        this.username = this.transform.Find("username").GetComponent<InputField>();
        this.password = this.transform.Find("password").GetComponent<InputField>();
        this.loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLoginButtonClicked()
    {
        UICamera.Instance.showWaitDialog(UICamera.Instance.loginUI);
         
    }
}
