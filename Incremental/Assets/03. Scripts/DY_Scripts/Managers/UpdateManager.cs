using System;
using UnityEngine;

public class UpdateManager
{
    [Tooltip("Managers - Update에 돌릴 메서드 등록 위함")]
    public Action _update = null;

    /// <summary>
    /// Managers - Update()
    /// </summary>
    public void OnUpdate()
    {
        if (_update != null)
            _update.Invoke();
    }

    public void Clear()
    {
        _update = null;
    }
}
