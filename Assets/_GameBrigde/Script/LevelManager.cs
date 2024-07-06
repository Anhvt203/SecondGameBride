using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// using 
public enum ColorType
{
    Default,
    Black,
    Blue,
    Brown,
    Green,
    Orange,
    Red,
    Violet,
    Yellow
}
public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() {ColorType.Black, ColorType.Blue, ColorType.Brown, ColorType.Green,ColorType.Orange
    ,ColorType.Red, ColorType.Violet, ColorType.Yellow};
    //player
    public PlayerController player;
    public NavMeshData navMeshData;
    public Transform startPoint;
    public Transform finishPoint;
    public Stage stage;
    //bot
    public int botAmout;
    [SerializeField] GameObject botPrefabs;
    private List<Bot> bots = new List<Bot>();
    public Vector3 FinishPoint => finishPoint.position;
    public int CharacterAmount => botAmout + 1;
	private bool navMeshAdded = false;
	// Start is called before the first frame update
	void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        if (!navMeshAdded)
        {
			NavMesh.RemoveAllNavMeshData();
			NavMesh.AddNavMeshData(navMeshData);
			navMeshAdded = true;
		}
		//position
		List<Vector3> startPoints = new List<Vector3>();
        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(startPoint.position + Vector3.right * 2f *i);
        }
        //player
        List<ColorType> colorDatas = colorTypes;
        int rand = Random.Range(0, colorDatas.Count);
        //int rand = 0;

        player.ChangeColor(colorDatas[rand]);
        // Debug.Log(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        int randPosition = Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[randPosition];
        startPoints.RemoveAt(randPosition);

        //bot
        for (int i = 0; i < CharacterAmount -1; i++)
        {
            Bot bot = Instantiate(botPrefabs, startPoints[i], Quaternion.identity).GetComponent<Bot>();
            bot.ChangeColor(colorDatas[i]);
            bot.GetComponent<Character>().stage = stage;
            bot.ChangeState(new AttackState());
            bots.Add(bot);
        }
    }
}
