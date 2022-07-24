using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float life = 1.5f;
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(ResetParent());
    }

    private IEnumerator ResetParent()
    {
        yield return new WaitForSeconds(life);
        ps.transform.SetParent(null);
    }
}
