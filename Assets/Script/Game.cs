using System;
using System.Collections;
using System.Collections.Generic;
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
            LoadWorld();
        }
        else
        {
            GenerateWorld();

        }
        GameFieldMV.Instance.DrawGameField(LoadWorldFlag);
        LoadWorldFlag = false;
    }

    public void GenerateWorld()
    {
        IBuilder builder;
        GameFieldModel gameField = new GameFieldModel(_gameFieldSize);
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

    public void SaveWorld()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
            + "/World.dat");

        GameFieldModel.Serial serial = new GameFieldModel.Serial();
        bf.Serialize(file, serial);
        file.Close();

        Debug.Log("SaveWorldFinish");
    }

    public bool LoadWorld()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath
                    + "/World.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath
                    + "/World.dat", FileMode.Open);              

                GameFieldModel.Serial data = (GameFieldModel.Serial)bf.Deserialize(file);
                GameFieldModel gameField = new GameFieldModel(data.SizeGameFieldSerial);
                gameField.TypeGameFieldMass = data.GameFieldMassSerial;                
                foreach (IBuilding building in data.BuildingListSerial)
                {
                    GameFieldMV.Instance.AddBuilding(building.GetPoint(), building.GetSizeBuild());
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
