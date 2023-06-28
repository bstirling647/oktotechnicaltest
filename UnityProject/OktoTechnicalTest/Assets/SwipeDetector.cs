using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public int _swipeLength = 15;
    public ContentSlider _contentSlider;
    private bool _touchDown;
    private Vector2 _touchStartPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchStartPos = Input.mousePosition;
            _touchDown = true;
        }
        if (_touchDown)
        {
            if (Input.mousePosition.y >= _touchStartPos.y + _swipeLength)
            {
                _touchDown = false;
                _contentSlider.CycleContent(true);
            }
            if (Input.mousePosition.y <= _touchStartPos.y - _swipeLength)
            {
                _touchDown = false;
                _contentSlider.CycleContent(false);
            }

        }
    }
}
