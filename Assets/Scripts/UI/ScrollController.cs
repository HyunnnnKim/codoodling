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
    private int _contentNumber = 20;
    [SerializeField]
    private Transform _contentRoot = null;
    [SerializeField]
    private GameObject _content = null;

    [Header("Buttons")]
    [SerializeField]
    private EventTrigger _leftButton = null;
    [SerializeField]
    private EventTrigger _rightButton = null;
    [SerializeField]
    private float _scrollSpeed = 1f;
    #endregion

    #region Private Field
    private List<GameObject> _contentList = null;
    private ScrollRect _scrollRect = null;

    private bool _pointerPressed = false;
    private UIButton _selectedButton = UIButton.Left;
    #endregion

    private void Start()
    {
        Setup();
    }

    #region Initialize
    private void Setup()
    {
        StartCoroutine(CreateContents());
        _scrollRect = GetComponentInChildren<ScrollRect>();

        var pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => OnPointerDown((PointerEventData)data));

        var pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => OnPointerUp((PointerEventData)data));

        var pointerHoverEntry = new EventTrigger.Entry();
        pointerHoverEntry.eventID = EventTriggerType.PointerEnter;
        pointerHoverEntry.callback.AddListener((data) => OnPointerSelect((PointerEventData)data));

        _leftButton.triggers.Add(pointerDownEntry);
        _leftButton.triggers.Add(pointerUpEntry);
        _leftButton.triggers.Add(pointerHoverEntry);

        _rightButton.triggers.Add(pointerDownEntry);
        _rightButton.triggers.Add(pointerUpEntry);
        _rightButton.triggers.Add(pointerHoverEntry);
    }
    #endregion

    #region Buttons
    private void OnPointerDown(PointerEventData data)
    {
        _pointerPressed = true;
    }

    private void OnPointerUp(PointerEventData data)
    {
        _pointerPressed = false;
    }

    private void OnPointerSelect(PointerEventData data)
    {
        var button = data.hovered.FirstOrDefault();
        if (button.transform.parent.gameObject == _leftButton.gameObject)
        {
            if (_selectedButton != UIButton.Left) _selectedButton = UIButton.Left;
        }
        else if (button.transform.parent.gameObject == _rightButton.gameObject)
        {
            if (_selectedButton != UIButton.Right) _selectedButton = UIButton.Right;
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(CreateContents());
        }

        if (_pointerPressed)
        {
            if (_selectedButton.Equals(UIButton.Left))
            {
                ScrollLeft();
            }
            else if (_selectedButton.Equals(UIButton.Right))
            {
                ScrollRight();
            }
        }
    }

    #region Content Creation
    private IEnumerator CreateContents()
    {
        if (_contentList == null)
            _contentList = new List<GameObject>();
        while (_contentList.Count != _contentNumber)
        {
            if (_contentList.Count > _contentNumber)
            {
                Destroy(_contentList.Last());
                _contentList.RemoveAt(_contentList.Count - 1);
                yield return null;
            }
            else
            {
                var button = Instantiate(_content, _contentRoot);
                var img = button.GetComponent<Image>();
                img.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                _contentList.Add(button);
                yield return null;
            }
        }
    }
    #endregion

    #region Scrolling
    private void ScrollLeft()
    {
        Scroll(-1);
    }

    private void ScrollRight()
    {
        Scroll(+1);
    }

    private void Scroll(int sign)
    {
        var bar = GetComponentInChildren<Scrollbar>();
        var speed = sign * _scrollSpeed * bar.size;
        _scrollRect.horizontalNormalizedPosition += speed;
    }
    #endregion
}

public enum UIButton { Left, Right }