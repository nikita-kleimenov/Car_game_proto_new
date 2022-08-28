using System;
using System.Collections;
using System.Collections.Generic;
using BG.UI.Camera;
using DG.Tweening;
using UnityEngine;

public class LevelTransitionEffect : MonoBehaviour
{
    #region Singleton

    private static LevelTransitionEffect _default;
    public static LevelTransitionEffect Default => _default;
    
    #endregion
    
    [SerializeField] private SpriteRenderer _back;
    [SerializeField] private SpriteMask _hole;


    private void Awake()
    {
        _default = this;
    }

    private void Start()
    {
        transform.SetParent(Camera.main.transform);
        transform.localPosition = Vector3.forward * (Camera.main.nearClipPlane + 0.01f);
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        float size = (Mathf.Tan(0.5f * Camera.main.fieldOfView) * 0.01f * 4f) * 2f;
        transform.localScale = Vector3.one * size;
    }

    public void DoTransition(Action onComplete)
    {
        _hole.transform.DOScale(0f, .5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                _hole.transform.DOScale(1f, .5f)
                    .SetEase(Ease.InQuad);
            });
    }
}
