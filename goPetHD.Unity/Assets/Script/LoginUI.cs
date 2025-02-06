using goPetHD;
using goPetHD.Helper;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button loginButton;
    private TMPro.TMP_InputField username, password;
    void Start()
    {
        this.loginButton = this.GetComponentInChildren<Button>();
        TMPro.TMP_InputField[] inputFields = this.GetComponentsInChildren<TMPro.TMP_InputField>();
        this.username = inputFields[0];
        this.password = inputFields[1];
        this.loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLoginButtonClicked()
    {
        if (StringHelper.IsUsernamePassValid(this.username.text) && StringHelper.IsUsernamePassValid(this.password.text))
        {
            UICamera.ShowWaitDialog();
            GameClient.client.doConnect();
        }
        else UICamera.ShowOkDialog("Tài khoản và mật khẩu không được để trống từ 6-20 kí tự và không chứa kí tự đặc biệt.");
    }
}
