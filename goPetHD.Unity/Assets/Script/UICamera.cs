using TMPro;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UICamera Instance;
    public Canvas loginUI, okDialog, answerDialog, waitDialog;
    private Canvas[] dialogUIs;
    void Start()
    {
        Instance = this;

        loginUI = transform.Find("loginUI").GetComponent<Canvas>();
        okDialog = transform.Find("okDialog").GetComponent<Canvas>();
        answerDialog = transform.Find("answerDialog").GetComponent<Canvas>();
        waitDialog = transform.Find("waitDialog").GetComponent<Canvas>();
        this.dialogUIs = new Canvas[] { okDialog, answerDialog, waitDialog };
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

    private void clearAllScreen(Canvas[] canvas)
    {
        if (canvas != null)
        {
            foreach (var item in canvas)
            {
                item.enabled = false;
            }
        }
    }

    public void showWaitDialog(Canvas nonClear = null)
    {
        clearAllScreen(nonClear);
        waitDialog.enabled = true;
    }

    public void showOkDialog(string message, Canvas nonClear = null)
    {
        clearAllScreen(nonClear);
        okDialog.enabled = true;
        okDialog.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }

    public void OkDialogImp(string message)
    {
        this.clearAllScreen(this.dialogUIs);
        okDialog.enabled = true;
        okDialog.GetComponentInChildren<TextMeshProUGUI>().text = message;
    }

    public void WaitDialog()
    {
        this.clearAllScreen(this.dialogUIs);
        waitDialog.enabled = true;
    }

    public static void ShowWaitDialog()
    {
        Instance.WaitDialog();
    }

    public static void ShowOkDialog(string message)
    {
        Instance.OkDialogImp(message);
    }

    public static void ShowOkDialog(string message, Canvas nonClear)
    {
        Instance.showOkDialog(message, nonClear);
    }

    public static void ShowWaitDialog(Canvas nonClear)
    {
        Instance.showWaitDialog(nonClear);
    }
}
