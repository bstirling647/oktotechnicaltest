using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ContentSlider : MonoBehaviour
{
    public Transform _offScreenBottomMount;
    public Transform _offScreenTopMount;
    public Transform _onScreenMount;
    public List<PageContent> _pages;
    public int _currentVisible;
    public int _nextVisible;
    public float _slideTime = 1;
    public Ease _leavingEaseType;
    public Ease _arrivingEaseType;
    public AudioSource _audioSource;
    public AudioClip _swooshClip;
    private bool _midTransition;
    void Awake()
    {
        foreach (PageContent p in transform.GetComponentsInChildren<PageContent>())
        {
            _pages.Add(p);
            p.gameObject.transform.position = _offScreenBottomMount.position;
            p.gameObject.SetActive(false);
        }
        _pages[0].transform.position = _onScreenMount.position;
        _pages[0].gameObject.SetActive(true);
    }

    public void CycleContent(bool pUp)
    {
        Vector3 fromPosition;
        Vector3 toPosition;
        DOTween.Kill("SlideTween");


        if (_midTransition)
        {
            //here's we'll snap things into place in case of a swipe while still in the old swipe
            _pages[_currentVisible].transform.position = _offScreenBottomMount.position;
            _pages[_nextVisible].transform.position = _onScreenMount.position;
            TweenComplete();
        }

        if (pUp)
        {
            if (_currentVisible == _pages.Count - 1)
            {
                _nextVisible = 0;
            }
            else
            {
                _nextVisible = _currentVisible + 1;
            }
            fromPosition = _offScreenBottomMount.position;
            toPosition = _offScreenTopMount.position;
            //pitching the audio to differentiate up and down swipes
            _audioSource.pitch = 1.1f;
        }
        else
        {

            if (_currentVisible == 0)
            {
                _nextVisible = _pages.Count - 1;
            }
            else
            {
                _nextVisible = _currentVisible - 1;
            }
            fromPosition = _offScreenTopMount.position;
            toPosition = _offScreenBottomMount.position;
            _audioSource.pitch = 0.9f;
        }

        _pages[_nextVisible].gameObject.SetActive(true);
        _pages[_nextVisible].gameObject.transform.position = fromPosition;
        _midTransition = true;

        _audioSource.PlayOneShot(_swooshClip);

        DOTween.Sequence()
        .Append(_pages[_currentVisible].transform.DOMove(toPosition, _slideTime)
        .SetEase(_leavingEaseType))
        .Insert(0, _pages[_nextVisible].transform.DOMove(_onScreenMount.position, _slideTime)
        .SetEase(_arrivingEaseType))
        .AppendCallback(TweenComplete)
        .SetId("SlideTween");

    }
    private void TweenComplete()
    {
        _pages[_currentVisible].gameObject.SetActive(false);
        _midTransition = false;
        _currentVisible = _nextVisible;
    }
}
