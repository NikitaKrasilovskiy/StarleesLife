using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampUI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private new Camera camera;
    private RectTransform _transform;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        var pos = camera.WorldToScreenPoint(target.position);
        _transform.anchoredPosition = pos;
    }
}
