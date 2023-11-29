using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    /// <summary>
    /// Can be detect by ai
    /// </summary>
    public interface IDetectable
    {
        public Transform GetTransform();
    }
}
