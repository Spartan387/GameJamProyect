using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float time = 0.4f;
    private void Awake()
    {
        Destroy(this.gameObject, time);
    }
}
