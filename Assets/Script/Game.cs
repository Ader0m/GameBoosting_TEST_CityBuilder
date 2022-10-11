using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Game : MonoBehaviour
{
    #region Singltone
    public static Game Instance
    {
        get
        {
            return _instance;
        }
    }
    private static Game _instance;
    #endregion

    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private GameObject _playerPrefab;
    /// <summary>
    /// Сube edge
    /// </summary>
    [SerializeField] private int _gameFieldSize = 100;
    /// <summary>
    /// false - desert, true - flat
    /// </summary>
    [SerializeField] private bool _typeGeneration;
    public Sprite[] CellSpriteMass;
    public static bool LoadWorldFlag;


    void Awake()
    {
        _instance = this;   
        LoadResources();      
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (LoadWorldFlag)
        {
            if (!LoadWorld())
            {
                _canvasManager.OnText("Файл сохранения не найден или поврежден");
                GenerateWorld();             
            }
        }
        else
        {
            GenerateWorld();

        }
        GameFieldDraw.Instance.DrawGameField(LoadWorldFlag);
        LoadWorldFlag = false;
    }

    public void GenerateWorld()
    {
        IBuilder builder;
        GameFieldLogick gameField = new GameFieldLogick(_gameFieldSize);
        _typeGeneration = UnityEngine.Random.Range(0, 2) == 0 ? false : true;


        if (_typeGeneration)
        {
            builder = new FlatBuilder();
        }
        else
        {
            builder = new DesertBuilder();
        }       
        Director director = new Director(builder);
        director.Construct();      
    }

    private void LoadResources()
    {
        CellSpriteMass = Resources.LoadAll<Sprite>(@"Cell\");
        Debug.Log("LoadResourcesFinish");
    }

    public void SpawnPlayer() 
    {
        GameObject obj = Instantiate(_playerPrefab);
        obj.transform.position = new Vector3(_gameFieldSize / 2, 5, _gameFieldSize / 2);
    }

    public void SaveWorldQuery()
    {
        if (SaveWorld())
        {
            _canvasManager.OnText("Игра сохранена");
        }
        else
        {
            _canvasManager.OnText("Ошибка при сохранении");
        }
    }

    private bool SaveWorld()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
                + "/World.dat");

            GameFieldLogick.GameFieldSerial serial = new GameFieldLogick.GameFieldSerial();
            bf.Serialize(file, serial);
            
            file.Close();       
            Debug.Log("SaveWorldFinish");


            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }


        return false;
    }

    private bool LoadWorld()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath
                    + "/World.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath
                    + "/World.dat", FileMode.Open);              

                GameFieldLogick.GameFieldSerial data = (GameFieldLogick.GameFieldSerial)bf.Deserialize(file);
                GameFieldLogick gameField = new GameFieldLogick(data.SizeGameFieldSerial);
                gameField.TypeGameFieldMass = data.GameFieldMassSerial;                
                foreach (IBuilding building in data.BuildingListSerial)
                {
                    GameFieldDraw.Instance.AddBuilding(building.GetPoint(), building.GetSizeBuild());
                }

                file.Close();
                Debug.Log("World data loaded!");
                _canvasManager.OnText("Игра загружена");                

                return true;
            }
            else
            {
                Debug.LogError("No have save data!");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }


        return false;
    }
}
