using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedZone : MonoBehaviour
{
    private Material thisMaterial;
    private void Awake()
    {
        thisMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void Fading()
    {
        StartCoroutine(BurningOut());
    }

    private IEnumerator BurningOut()
    {
        float step = 0f;
        while (step < 1.1f)
        {
            thisMaterial.SetFloat("_Threshold", step);
            step += 0.01f;
            yield return new WaitForFixedUpdate();
        }
    }
}
