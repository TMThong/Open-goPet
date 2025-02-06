using UnityEngine;

public class UICamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UICamera Instance;
    public Canvas loginUI, okDialog, answerDialog, waitDialog;
    void Start()
    {
        Instance = this;
        
        loginUI = transform.Find("loginUI").GetComponent<Canvas>();
        okDialog = transform.Find("okDialog").GetComponent<Canvas>();
        answerDialog = transform.Find("answerDialog").GetComponent<Canvas>();
        waitDialog = transform.Find("waitDialog").GetComponent<Canvas>();
        this.clearAllScreen();
        loginUI.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void clearAllScreen(Canvas nonClear = null)
    {
        loginUI.enabled = false;
        okDialog.enabled = false;
        answerDialog.enabled = false;
        waitDialog.enabled = false;
        if (nonClear != null)
        {
            nonClear.enabled = true;
        }
    }

    public void showOkDialog(string message)
    {
        clearAllScreen();
        okDialog.enabled = true;
        okDialog.GetComponentInChildren<UnityEngine.UI.Text>().text = message;
    }

    public void showWaitDialog(Canvas nonClear = null)
    {
        clearAllScreen(nonClear);
        waitDialog.enabled = true;
    }
}
