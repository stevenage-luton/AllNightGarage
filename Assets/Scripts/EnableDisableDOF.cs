using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnableDisableDOF : MonoBehaviour
{
    public VolumeProfile _Profile;
    UnityEngine.Rendering.Universal.DepthOfField depthOfField;

    ClampedFloatParameter maxBlur;
    int maxFocal = 25;
    int minFocal = 1;

    void Awake()
    {
        _Profile.TryGet(out depthOfField);
        depthOfField.active = true;
        maxBlur = depthOfField.focalLength;
        depthOfField.focalLength.value = minFocal;
    }

    public void EnableDisableDepthOfField(bool value)
    {
        StopAllCoroutines();
        if (value)
        {
            StartCoroutine(EnableDepthOfField());
        }
        else
        {
            StartCoroutine(DisableDepthOfField());
        }
      
    }

    public IEnumerator EnableDepthOfField()
    {
        
        depthOfField.focalLength.value = minFocal;

        for (int i = minFocal; i < maxFocal; i++)
        {
            depthOfField.focalLength.value ++;
            yield return new WaitForSeconds(.01f);
        }

    }

    public IEnumerator DisableDepthOfField()
    {

        depthOfField.focalLength.value = maxFocal;

        for (int i = minFocal; i < maxFocal; i++)
        {
            depthOfField.focalLength.value--;
            yield return new WaitForSeconds(.01f);
        }

    }
}
