using System;
using UnityEngine;

public class UpdateManager
{
    /// <summary>
    /// Managers - Update에 돌릴 메서드 등록 위함
    /// </summary>
    public event Action _update = null;

    /// <summary>
    /// Managers - Update에 돌릴 메서드 등록(BgScroll)
    /// </summary>
    public event Action _updateBgScroll = null;

    /// <summary>
    /// Managers - Update()
    /// </summary>
    public void OnUpdate()
    {
        if (!Managers.GameM.isBattle)
        {
            if (_update != null)
                _update.Invoke();

            if (_updateBgScroll != null)
                _updateBgScroll.Invoke();
        }
    }

    public void Clear()
    {
        _update = null;
        _updateBgScroll = null;
    }
}
