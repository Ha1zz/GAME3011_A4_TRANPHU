using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [Header("SpawnPoints")]
    [SerializeField] GameObject exampleSpawnPoint;
    [SerializeField] GameObject puzzleSpawnpoint;

    [Header("Prefab")]
    [SerializeField] Cube cubePrefab;

    private Cube[,] exampleCubeArray;
    private Cube[,] puzzleCubeArray;


    [Header("Dimesions")]
    public int row = 8;
    public int column = 8;

    [Header("Materials")]
    public Material defaultMaterial;
    public Material changedMaterial;

    [Header("PuzzleLimit")]
    public int puzzleLimit = 8;
    private int playerAnswered = 0;

    //public delegate void ClickAction();
    //public static event ClickAction OnClicked;

    private void Awake()
    {
        switch (GameManager.difficulty)
        {
            case 2:
                puzzleLimit += 2;
                break;
            case 3:
                puzzleLimit += 3;
                break;
            default:
                puzzleLimit += 1;
                break;
        }
        exampleCubeArray = new Cube[row, column];
        puzzleCubeArray = new Cube[row, column];
    }


    // Start is called before the first frame update
    void Start()
    {
        SpawnCube(exampleSpawnPoint,ref exampleCubeArray);
        SpawnCube(puzzleSpawnpoint,ref puzzleCubeArray);
        SpawnExample();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void SpawnCube(GameObject spawnPoint,ref Cube[,] cubeArray)
    {
        cubeArray = new Cube[row,column];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0 ; j < column ; j++)
            {
                cubeArray[i,j] = Instantiate(cubePrefab, new Vector3(spawnPoint.transform.position.x + i * cubePrefab.transform.localScale.x * 1.05f, spawnPoint.transform.position.y + j * cubePrefab.transform.localScale.y * 1.05f, 0),Quaternion.identity);
            }
        }
    }

    void SpawnExample()
    {
        for (int i = 0; i < puzzleLimit; i++)
        {
            bool isPressed = false;
            do
            {
                int x = Random.Range(0, row);
                int y = Random.Range(0, column);
                if (exampleCubeArray[x, y].GetComponent<Cube>().status == Status.NORMAL)
                {
                    exampleCubeArray[x, y].GetComponent<Renderer>().material = changedMaterial;
                    exampleCubeArray[x, y].GetComponent<Cube>().status = Status.PRESSED;
                    isPressed = true;
                }
            }
            while (isPressed == false);
        }
    }

    public void CompareResult()
    {
        if (playerAnswered == puzzleLimit)
        {
            int tempCheck = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (exampleCubeArray[i,j].GetComponent<Cube>().status == Status.PRESSED)
                    {
                        if (puzzleCubeArray[i,j].GetComponent<Cube>().status == Status.PRESSED)
                        {
                            tempCheck++;
                        }
                    }
                }
            }
            if (tempCheck == puzzleLimit)
            {
                Debug.Log("CHECKMATE");
                GameManager.instance.WinCondition();
            }
        }
    }

    private void IncreaseAnwser()
    {
        playerAnswered++;
    }

    private void DecreaseAnswer()
    {
        playerAnswered--;
    }

    private void OnEnable()
    {
        Cube.OnIncreased += IncreaseAnwser;
        Cube.OnIncreased += CompareResult;
        Cube.OnDecreased += DecreaseAnswer;
        Cube.OnDecreased += CompareResult;
    }

    private void OnDisable()
    {
        Cube.OnIncreased -= IncreaseAnwser;
        Cube.OnIncreased -= CompareResult;
        Cube.OnDecreased -= DecreaseAnswer;
        Cube.OnDecreased -= CompareResult;
    }
}
