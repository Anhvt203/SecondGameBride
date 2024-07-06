using System.Collections;
using System.Collections.Generic;
using MarchingBytes;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    private List<Vector3> emptyPoints = new List<Vector3>();
    private List<Brick> bricks = new List<Brick>();
    [SerializeField] Brick brickPrefeb;
    void Start()
    {
    }
    internal void OnInit(ColorType colorType)
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            emptyPoints.Add(brickPoints[i].position);
        }
        for(int i = 0; i< 5; i++)
        {
            NewBrick(colorType);
        }
    }
    public void InitColor(ColorType colorType)
    {
    }
    //them gach
    public void NewBrick(ColorType colorType)
    {
        if(emptyPoints.Count > 0)
        {
            int randomNumber = Random.Range(0, emptyPoints.Count);
            Brick brick = Instantiate(brickPrefeb, emptyPoints[randomNumber], Quaternion.identity);
            // Brick brick =  EasyObjectPool.instance.GetObjectFromPool("Brick",emptyPoints[randomNumber], Quaternion.identity).GetComponent<Brick>();
            brick.ChangeColor(colorType);
            emptyPoints.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }
    public void RemoveBrick(Brick brick)
    {
        StartCoroutine(respawnBrick(brick.colorType));
        emptyPoints.Add(brick.transform.position);
        bricks.Remove(brick);
        Destroy(brick.gameObject);
        // EasyObjectPool.instance.ReturnObjectToPool(brick.gameObject);
        // brick.gameObject.SetActive(false);
        //return mot ham tao ra
        // if (Random.Range(0, 2) == 0)
        // {
        //     StartCoroutine(respawnBrick(ColorType.Blue));
        // }
        // else
        // {
        //     StartCoroutine(respawnBrick(ColorType.Green));
        // }
    }
    IEnumerator respawnBrick(ColorType colorType)
    {
        yield return new WaitForSeconds(3f);
        NewBrick(colorType);
    }
    public void MoreBrick()
    {
        for(int i = 0; i < 5; i ++)
            {
                NewBrick(ColorType.Blue);
                NewBrick(ColorType.Red);
                NewBrick(ColorType.Green);
                NewBrick(ColorType.Yellow);
            }
    }
    internal Brick SeekBrickPoint(ColorType colorType)
    {
        Brick brick = null;
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick = bricks[i];
                break;
            }
        }
        return brick;
    }
}

