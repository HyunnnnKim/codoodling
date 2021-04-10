using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    #region Serialized Field
    [Header("Content")]
    [SerializeField, Range(20, 100)]
    private int contentNumber = 20;
    [SerializeField]
    private ScrollRect scrollRect = null;
    [SerializeField]
    private GameObject contentPrefab = null;

    [Header("Buttons")]
    [SerializeField]
    private EventTrigger leftButton = null;
    [SerializeField]
    private EventTrigger rightButton = null;
    [SerializeField]
    private float scrollSpeed = 0.03f;
    #endregion

    #region Private Field
    private List<GameObject> contentList = null;
    private bool isPressed = false;
    #endregion

    private void Start()
    {
        Setup();
    }

    #region Initialize
    private void Setup()
    {
        StartCoroutine(CreateContents());

        var leftDownEntry = new EventTrigger.Entry();
        leftDownEntry.eventID = EventTriggerType.PointerDown;
        leftDownEntry.callback.AddListener((data) => OnLeftPointerDown((PointerEventData)data));

        var rightDownEntry = new EventTrigger.Entry();
        rightDownEntry.eventID = EventTriggerType.PointerDown;
        rightDownEntry.callback.AddListener((data) => OnRightPointerDown((PointerEventData)data));

        var upEntry = new EventTrigger.Entry();
        upEntry.eventID = EventTriggerType.PointerUp;
        upEntry.callback.AddListener((data) => OnPointerUp((PointerEventData)data));

        leftButton.triggers.Add(leftDownEntry);
        leftButton.triggers.Add(upEntry);

        rightButton.triggers.Add(rightDownEntry);
        rightButton.triggers.Add(upEntry);
    }
    #endregion

    #region Subscribed Functions
    /// <summary>
    /// Called on Left Button PointerDown Event.
    /// </summary>
    /// <param name="data"></param>
    private void OnLeftPointerDown(PointerEventData data)
    {
        isPressed = true;
        float ratio = scrollRect.horizontalScrollbar.size;
        StartCoroutine(Scroll(-1, ratio));
    }

    /// <summary>
    /// Called on Right Button PointerDown Event.
    /// </summary>
    /// <param name="data"></param>
    private void OnRightPointerDown(PointerEventData data)
    {
        isPressed = true;
        float ratio = scrollRect.horizontalScrollbar.size;
        StartCoroutine(Scroll(+1, ratio));
    }

    /// <summary>
    /// Called on any Button PointerUp Event.
    /// </summary>
    /// <param name="data"></param>
    private void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
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
}
