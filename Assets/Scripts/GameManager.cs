using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MLAPI;
using MLAPI.SceneManagement;

public enum Scenes
{
    START_SCENE = 0,
    MAIN_MENU,
    GAME_SCENE
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _connectionError;
    [SerializeField] private StartSceneController _startScreen;
    private Slider _progressBar;
    private Text _progressText;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
        _progressBar = _loadingScreen.GetComponentInChildren<Slider>();
        _progressText = _loadingScreen.GetComponentInChildren<Text>();
        StartGame();
    }

    private void StartGame()
    {
        _loadingScreen.SetActive(true);
        DataManager.Instance.LoadUserData();
        StartCoroutine(StartClient());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene((int)Scenes.START_SCENE);
        _loadingScreen.SetActive(true);
    }

    public void Play(int charId)
    {
        NetworkingManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes($"{Client.Instance._netID} {charId}");
        NetworkingManager.Singleton.StartClient();
        _loadingScreen.SetActive(true);
        LoadScene((int)Scenes.GAME_SCENE);
    }

    public void LoadScene(int sceneIdToLoad)
    {
        StartCoroutine(Load(sceneIdToLoad));
    }

    private IEnumerator StartClient()
    {
        Client.Instance.ConnectToServer();
        Debug.Log("Connecting to server");
        for(int counter = 0; counter < 10; counter++)
        {
            // Progress text: Connecting
            _progressText.text = "Connecting";
            if(Client.Instance.tcp.SocketConnected())
            {
                if(DataManager.Instance._dataExists)
                {
                    // Progress text: Getting data
                    _progressText.text = "Getting data";
                    ClientSend.AccountDataRequest(Account.Current.ID, Account.Current.Username);
                }
                else
                {
                    _progressText.text = "Connected";
                    _startScreen.StartScene();
                }
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
        _connectionError.SetActive(true);
    }

    private IEnumerator Load(int sceneIdToLoad)
    {
        // Progress text: Loading
        _progressText.text = "Loading";
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdToLoad, LoadSceneMode.Additive);
        while(!operation.isDone)
        {
            _progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
        SceneManager.UnloadSceneAsync(sceneIdToLoad - 1);
        _loadingScreen.SetActive(false);
    }
}
