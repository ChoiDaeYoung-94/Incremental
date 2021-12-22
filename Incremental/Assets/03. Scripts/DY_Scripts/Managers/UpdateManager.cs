using System;
using UnityEngine;

public class UpdateManager
{
    [Tooltip("Managers - Update에 돌릴 메서드 등록 위함")]
    public Action _update = null;

    [Tooltip("Managers - Update에 돌릴 메서드 등록(BgScroll)")]
    public Action _updateBgScroll = null;

    /// <summary>
    /// Managers - Update()
    /// </summary>
    public void OnUpdate()
    {
        if (_update != null)
            _update.Invoke();

        if (_updateBgScroll != null)
            _updateBgScroll.Invoke();
    }

    public void Clear()
    {
        _update = null;
        _updateBgScroll = null;
    }
}
