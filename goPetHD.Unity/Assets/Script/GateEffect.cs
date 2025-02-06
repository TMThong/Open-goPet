using UnityEngine;
using UnityEngine.UI;

public class GateEffect : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Image left, right;
    private Image left2, right2;
    private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        canvas = this.GetComponent<Canvas>();
        left = this.transform.Find("left").GetComponent<Image>();
        right = this.transform.Find("right").GetComponent<Image>();
        left2 = this.transform.Find("left2").GetComponent<Image>();
        right2 = this.transform.Find("right2").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        left.transform.position -= new Vector3(speed, 0, 0);
        left2.transform.position -= new Vector3(speed, 0, 0);
        right.transform.position += new Vector3(speed, 0, 0);
        right2.transform.position += new Vector3(speed, 0, 0);
        if (left.transform.position.x <= -left.rectTransform.rect.width && right.transform.position.x >= right.rectTransform.rect.width + this.canvas.pixelRect.width)
        {
            Destroy(canvas.gameObject, 0f);
        }
    }
}
