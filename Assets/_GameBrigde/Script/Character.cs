using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    public float speed = 5f;
    public Animator animator;
    private string crrAnim;
    public Transform body;
    public LayerMask groundLayer;
    public LayerMask stairLayer;
    [HideInInspector] public Stage stage;
    [SerializeField] PlayerBrick playerBrickPrefabs;
    [SerializeField] Transform brickHolder;
    public List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    public int BrickCount => playerBricks.Count;
    // Start is called before the first frame update
    public bool CanMove(Vector3 point)
    {
        bool canMove = false;
        if (Physics.Raycast(point + Vector3.up, Vector3.down, 3f, groundLayer))
        {
            RaycastHit hit;
            if (Physics.Raycast(point + Vector3.up, Vector3.down, out hit,3f, stairLayer))
            {
                // Debug.Log("stair color");
                if (hit.collider.gameObject.GetComponent<ColorObject>().colorType == colorType)
                {
                    // Debug.Log("same color");
                    canMove = true;
                }
                else
                {
                    // Debug.Log("different color");
                    if (playerBricks.Count > 0)
                    {
                        hit.collider.GetComponent<Stair>().ChangeColor(colorType);
                        RemoveBrick();
                    }
                    canMove = false;
                }
            }
            else
            {
                // Debug.Log("ground");
                canMove = true;
            }
        }
        // if (Physics.Raycast(point, Vector3.down, 2f, groundLayer))
        // {
        //     canMove = true;
        // }
        return canMove;
    }
    public Vector3 checkGround(Vector3 point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer))
        {
            // Debug.Log(hit.point);
            return hit.point + Vector3.up * 0.1f;
        }
        return point;
    }
    public void changeAnim(string animName)
    {
        if (crrAnim != animName)
        {
            if (!string.IsNullOrEmpty(crrAnim))
            {
                animator.ResetTrigger(crrAnim);
            }
            crrAnim = animName;
            animator.SetTrigger(crrAnim);
        }
    }
    public void AddBrick()
    {
        // Debug.Log("add brick");
        PlayerBrick playerBrick = Instantiate(playerBrickPrefabs, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition =  new Vector3(0f, 0.25f * (playerBricks.Count -1), -0.6f);
        playerBricks.Add(playerBrick);
    }
    public void RemoveBrick()
    {
        if (playerBricks.Count > 0)
        {
            // Debug.Log("remove brick");
            PlayerBrick playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Brick")
        {
            if (other.gameObject.GetComponent<ColorObject>().colorType == colorType)
            {
                stage.RemoveBrick(other.GetComponent<Brick>());
                AddBrick();
            }
        }
    }
}
