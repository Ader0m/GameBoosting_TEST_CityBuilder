using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private RectTransform _menu;
    [SerializeField] private RectTransform _gameInterface;
    [SerializeField] private RectTransform _buildInteface;
    [SerializeField] private RectTransform _terraInteface;
    [SerializeField] private RectTransform _infoInterface;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _buildButton;
    [SerializeField] private Button _terraButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _infoText;


    void Awake()
    {
        _menu.gameObject.SetActive(true);
        _gameInterface.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_playButton.gameObject.activeSelf)
        {
            SwitchInterface();
            PlayerCamera.Instance.SwitchCamera();
        }
    }

    private void SwitchInterface()
    {
        _menu.gameObject.SetActive(!_menu.gameObject.activeSelf);
        _gameInterface.gameObject.SetActive(!_gameInterface.gameObject.activeSelf);
        _buildInteface.gameObject.SetActive(false);
        _buildInteface.gameObject.GetComponent<BuildInterface>().OffButton();
        _terraInteface.gameObject.SetActive(false);

        Destroy(_buildInteface.gameObject.GetComponent<BuildInterface>().CurrentTemplate);
    }

    public void ShowBuildInteface()
    {
        _buildInteface.gameObject.SetActive(!_buildInteface.gameObject.activeSelf);
        _buildInteface.gameObject.GetComponent<BuildInterface>().OffButton();
        _buildButton.GetComponent<Outline>().enabled = !_buildButton.GetComponent<Outline>().enabled;
        _terraButton.GetComponent<Outline>().enabled = false;
        _terraInteface.gameObject.SetActive(false);

        Destroy(_buildInteface.gameObject.GetComponent<BuildInterface>().CurrentTemplate);
    }

    public void ShowTerraInteface()
    {
        _buildInteface.gameObject.SetActive(false);
        _buildInteface.gameObject.GetComponent<BuildInterface>().OffButton();
        _buildButton.GetComponent<Outline>().enabled = false;
        _terraButton.GetComponent<Outline>().enabled = !_terraButton.GetComponent<Outline>().enabled;
        _terraInteface.gameObject.SetActive(!_terraInteface.gameObject.activeSelf);

        Destroy(_buildInteface.gameObject.GetComponent<BuildInterface>().CurrentTemplate);
    }

    public void SpawnPlayer()
    {
        Game.Instance.SpawnPlayer();
        SwitchInterface();
        _infoInterface.gameObject.SetActive(true);
        _playButton.gameObject.SetActive(false);

    }

    public void SaveWorld()
    {
        Game.Instance.SaveWorldQuery();
    }

    public void LoadWorld()
    {
        Game.LoadWorldFlag = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void OnText(string str)
    {
        _infoText.text = str;
        _infoText.gameObject.SetActive(true);

        StartCoroutine(OffText(_infoText));
    }

    IEnumerator OffText(Text text)
    {
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
    }
}
