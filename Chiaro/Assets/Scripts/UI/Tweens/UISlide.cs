using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using Pixelplacement.TweenSystem;

public class UISlide : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    [Tooltip("Sets endTransform to transform at start")]
    public bool setEndToTransform = true;
    [Tooltip("Disable GO on start")]
    public bool willDisableOnStart;

    public float tweenTime = 1f;
    public AnimationCurve showEase, hideEase;

    TweenBase currTween;

    void Awake() {
        if (setEndToTransform) {
            endPosition = transform.localPosition;
        }
        transform.localPosition = startPosition;
        if (willDisableOnStart) {
            gameObject.SetActive(false);
        }
    }

    public void ShowElement() {
        gameObject.SetActive(true);
        currTween = Tween.LocalPosition(transform, endPosition, tweenTime, 0, showEase, Tween.LoopType.None, null, null, false);
    }

    public void HideElement() {
        currTween = Tween.LocalPosition(transform, startPosition, tweenTime, 0, hideEase, Tween.LoopType.None, null, OnCompleteHide, false);
    }

    void OnCompleteHide() {
        gameObject.SetActive(false);
    }
}
