using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [SerializeField] private Renderer rendere;
    public ColorType colorType;
    // private int totalBrick = 0;
    // private List<GameObject> playerBricks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        rendere.material = ColorController.Ins.getColorMaterial(colorType);
    }
}
