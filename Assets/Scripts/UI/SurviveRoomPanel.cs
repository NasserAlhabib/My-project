using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurviveRoomPanel : RoomPanelBase
{
    [Header("---------------------- Survive Room Panel Settings ----------------------")]
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text surviveText;
    [SerializeField] private AudioClip swooshInClip;
    [SerializeField] private AudioClip swooshOutClip;

    public override void InitializePanle(LobbyUIManager lobbyUIManager)
    {
        base.InitializePanle(lobbyUIManager);
        PongManager.Instance.OnHealthChanged += UpdateLivesText;
        audioSource = GetComponent<AudioSource>();
        timerText.gameObject.SetActive(false);
        surviveText.gameObject.SetActive(false);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        StartCoroutine(RoomFlowCoroutine());
    }

    private void OnDisable()
    {
        PongManager.Instance.OnHealthChanged -= UpdateLivesText;
    }

    private void OnDestroy()
    {
        PongManager.Instance.OnHealthChanged -= UpdateLivesText;

    }


    private void UpdateLivesText(int currentHealth, int maxHealth)
    {
        livesText.text = $"Lives: {currentHealth}";
    }


    private IEnumerator RoomFlowCoroutine()
    {
        float duration = PongManager.Instance.GetCurrentRoomData().roomDuration;

        // Prepare the surviveText for tweening
        surviveText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        surviveText.text = $"Survive For {duration} seconds";

        // start invisible and tiny
        Color color = surviveText.color;
        surviveText.color = new Color(color.r, color.g, color.b, 0f);
        surviveText.transform.localScale = Vector3.zero;

        Sequence splash = DOTween.Sequence()
      // 1) swoosh-in at the very start
      .OnStart(() => audioSource.PlayOneShot(swooshInClip))

      // 2) fade in + pop scale
      .Append(surviveText.DOFade(1f, 0.5f))
      .Join(surviveText.transform
          .DOScale(1f, 0.5f)
          .SetEase(Ease.OutBack))

      // 3) hold on-screen
      .AppendInterval(1.5f)

      // 4) play swoosh-out _just before_ fading away
      .AppendCallback(() => audioSource.PlayOneShot(swooshOutClip))

      // 5) fade out + shrink
      .Append(surviveText.DOFade(0f, 0.5f))
      .Join(surviveText.transform
          .DOScale(0f, 0.5f))

      // 6) hide when done
      .OnComplete(() => surviveText.gameObject.SetActive(false));

        // 3) Wait for that splash to finish
        yield return splash.WaitForCompletion();

        // 4) Now do your countdown
        timerText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        float timeLeft = duration;
        while (timeLeft > 0f)
        {
            timerText.text = $"Time Left: {Mathf.CeilToInt(timeLeft)}";
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        timerText.text = "0";

        // 5) End the room
        timerText.gameObject.SetActive(false);
        PongManager.Instance.EndRoom();
        ClosePanel();
    }


}
