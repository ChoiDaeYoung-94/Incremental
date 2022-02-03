using System;
using UnityEngine;

public class UpdateManager
{
    /// <summary>
    /// Managers - Update에 돌릴 메서드 등록(UI Contents)
    /// </summary>
    public event Action _update = null;

    /// <summary>
    /// Managers - Update에 돌릴 메서드 등록(BgScroll)
    /// </summary>
    public event Action _updateBgScroll = null;

    /// <summary>
    /// Managers - Update에 돌릴 메서드 등록(Monsters)
    /// </summary>
    public event Action _updateMonsters = null;

    /// <summary>
    /// Managers - Update()
    /// </summary>
    public void OnUpdate()
    {
        if (_update != null)
            _update.Invoke();

        if (!Managers.GameM.CheckBattle())
        {
            if (_updateMonsters != null)
                _updateMonsters.Invoke();

            if (_updateBgScroll != null)
                _updateBgScroll.Invoke();
        }
    }

    public void Clear()
    {
        _update = null;
        _updateBgScroll = null;
        _updateMonsters = null;
    }
}
