using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int _swipeLength = 15;
    public ContentSlider _contentSlider;
    [SerializeField] private bool _touchDown;
    private Vector2 _touchStartPos;

    public void OnBeginDrag(PointerEventData data)
    {
        _touchDown = true;
    }
    public void OnEndDrag(PointerEventData data)
    {
        _touchDown = false;
    }
    public void OnDrag(PointerEventData data)
    {
        if (data.dragging)
        {
            _touchStartPos = data.pressPosition;
        }
        if (_touchDown)
        {
            if (data.position.y >= _touchStartPos.y + _swipeLength)
            {
                _touchDown = false;
                _contentSlider.CycleContent(true);
            }
            if (data.position.y <= _touchStartPos.y - _swipeLength)
            {
                _touchDown = false;
                _contentSlider.CycleContent(false);
            }
        }
    }
}
