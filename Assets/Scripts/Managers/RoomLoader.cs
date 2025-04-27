using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoader : MonoBehaviour
{
    public static RoomLoader Instance { get; private set; }

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private float fadeDuration = 1f;

    [Header("Room Flow Settings")]
    [SerializeField] private int maxRooms = 5;

    private int currentRoomIndex = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // hide at first
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);

        // kick off the first room load
        StartCoroutine(LoadSceneAsync($"Room {currentRoomIndex}"));
    }

    /// <summary>
    /// Call this to advance to the next room (or victory when done).
    /// </summary>
    public void LoadNextRoom()
    {
        currentRoomIndex++;
        var next = currentRoomIndex > maxRooms;

        if (next)
        {
            PongManager.Instance.SetGameState(GameState.Victory);
        }
        else
        {
            // load the next room
            StartCoroutine(LoadSceneAsync($"Room {currentRoomIndex}"));
        }


    }

    /// <summary>
    /// Fades to black, loads the scene, fades back, then invokes onComplete.
    /// </summary>
    public IEnumerator FadeOutThen(Action onComplete)
    {
        // 1) show & fade to black
        loadingText.gameObject.SetActive(false);
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        yield return null; // wait one frame for activation

        // 2) fade back to transparent
        yield return Fader.Fade(1f, 0f, fadeDuration, canvasGroup);
        canvasGroup.gameObject.SetActive(false);

        // 3) invoke callback (e.g. start RoomConfig logic)
        onComplete?.Invoke();
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // 1) show & fade to black
        canvasGroup.gameObject.SetActive(true);
        yield return Fader.Fade(0f, 1f, fadeDuration, canvasGroup);

        // 2) start loading the next scene, but hold off activation
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        // 3) wait until Unity has it ready (90%)
        while (op.progress < 0.9f)
            yield return null;

        // 4) now let it swap in
        op.allowSceneActivation = true;
        // wait until it's fully done
        while (!op.isDone)
            yield return null;

        // 5) fade back to transparent (reveal the new scene)
        yield return Fader.Fade(1f, 0f, fadeDuration, canvasGroup);
        canvasGroup.gameObject.SetActive(false);
    }

    /// <summary>
    /// Immediately reloads the first room.
    /// </summary>
    public void RestartFirstRoom()
    {
        currentRoomIndex = 1;
        StartCoroutine(LoadSceneAsync($"Room {currentRoomIndex}"));
    }
}

