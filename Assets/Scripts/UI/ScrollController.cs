using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    #region Serialized Field
    [Header("Content")]
    [SerializeField, Range(20, 100)]
    private int contentNumber = 20;
    [SerializeField]
    private float scrollSpeed = 0.03f;
    [SerializeField]
    private ScrollRect scrollRect = null;
    [SerializeField]
    private GameObject contentPrefab = null;
    #endregion

    #region Private Field
    private List<GameObject> contentList = null;
    private bool isPressed = false;
    #endregion

    private void Start()
    {
        StartCoroutine(CreateContents());
    }

    private void OnEnable()
    {
        UIButton.OnButtonDown += Scrolling;
        UIButton.OnButtonUp += NotScrolling;
    }

    #region Subscribed Functions
    private void Scrolling(int sign, bool state)
    {
        isPressed = state;
        float ratio = scrollRect.horizontalScrollbar.size;
        StartCoroutine(Scroll(sign, ratio));
    }

    private void NotScrolling(bool state)
    {
        isPressed = state;
    }
    #endregion

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(CreateContents());
        }
    }

    #region Content Creation
    private IEnumerator CreateContents()
    {
        if (contentList == null)
            contentList = new List<GameObject>();
        while (contentList.Count != contentNumber)
        {
            if (contentList.Count > contentNumber)
            {
                Destroy(contentList.Last());
                contentList.RemoveAt(contentList.Count - 1);
                yield return null;
            }
            else
            {
                var button = Instantiate(contentPrefab, scrollRect.content);
                var img = button.GetComponent<Image>();
                img.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                contentList.Add(button);
                yield return null;
            }
        }
    }
    #endregion

    #region Scrolling
    private IEnumerator Scroll(int sign, float ratio)
    {
        var speed = sign * scrollSpeed * ratio;
        while (isPressed)
        {
            scrollRect.horizontalNormalizedPosition += speed;
            yield return null;
        }
    }
    #endregion

    private void OnDisable()
    {
        UIButton.OnButtonDown -= Scrolling;
        UIButton.OnButtonUp -= NotScrolling;
    }
}
