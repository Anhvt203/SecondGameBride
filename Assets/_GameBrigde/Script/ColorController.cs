using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorController : Singleton<ColorController>
{
    [SerializeField] Material[] colorMaths;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Material getColorMaterial(ColorType colorType)
    {
        return colorMaths[(int)colorType];
    }
}
