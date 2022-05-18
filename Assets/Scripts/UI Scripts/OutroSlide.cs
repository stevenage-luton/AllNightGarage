using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/End Slide")]
public class OutroSlide : ScriptableObject
{
    [TextArea(10, 100)]
    public string slideText;
}
