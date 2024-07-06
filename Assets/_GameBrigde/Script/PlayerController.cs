using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
public class PlayerController : Character
{
    // Start is called before the first frame update
    void Start()
    {
        changeAnim("idle");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // transform.position += JoystickControl.direct * speed * Time.deltaTime;
            Vector3 nextPoints = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextPoints))
            {
                transform.position = checkGround(nextPoints);
            }
            changeAnim("run");
            if (JoystickControl.direct != Vector3.zero)
            {
                body.forward = JoystickControl.direct;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
    }
}
