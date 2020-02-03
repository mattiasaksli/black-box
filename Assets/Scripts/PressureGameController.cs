using Doozy.Engine;
using Doozy.Engine.Progress;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressureGameController : MonoBehaviour
{
    public PressureToggle thermalToggle;
    public PressureToggle lowerToggle;
    public PressureToggle emergencyToggle;
    public PressureToggle mainToggle;
    public Slider indicator;
    public Progressor timeProgressor;

    public GameObject player;
    public AudioSource src;
    public SaveState save;

    public ChooseAudio chooser;
    public float currentPressureValue = 0;
    public float targetPressureValue;
    public float lerpTime = 1f;
    public float loseTimer;
    public float maxLoseTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        save = GameObject.FindGameObjectWithTag("SaveState").GetComponent<SaveState>();
        src = GetComponent<AudioSource>();
        src.Play();
        timeProgressor.SetMax(maxLoseTime);
        currentPressureValue += thermalToggle.currentValue;
        currentPressureValue += lowerToggle.currentValue;
        currentPressureValue += emergencyToggle.currentValue;
        currentPressureValue += mainToggle.currentValue;
        targetPressureValue = currentPressureValue;
    }


    void LateUpdate()
    {
        if (currentPressureValue != targetPressureValue)
        {
            currentPressureValue = Mathf.Lerp(currentPressureValue, targetPressureValue, (1 / lerpTime) * Time.deltaTime);
            if (Mathf.Abs(currentPressureValue - targetPressureValue) < 0.001f) { currentPressureValue = targetPressureValue; }
        }

        indicator.value = currentPressureValue;

        if (currentPressureValue >= 0.65f)
        {
            loseTimer = Mathf.Max(0, loseTimer - Time.deltaTime);
        }
        else
        {
            loseTimer = Mathf.Min(maxLoseTime, loseTimer + Time.deltaTime * 2);
        }

        timeProgressor.SetValue(maxLoseTime - loseTimer);
        src.pitch = 1 + timeProgressor.Value / maxLoseTime;

        //Lose
        if (loseTimer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PressureFailure();
            chooser.Stop();
            src.Stop();
            this.enabled = false;
        }

        //Win
        if (currentPressureValue == targetPressureValue && targetPressureValue == 0.6f)
        {
            GameEventMessage.SendEvent("GameWon");
            StartCoroutine(GameWon());
        }
    }

    public void ChangeTargetPressureValue()
    {
        targetPressureValue = 0;
        targetPressureValue += thermalToggle.currentValue;
        targetPressureValue += lowerToggle.currentValue;
        targetPressureValue += emergencyToggle.currentValue;
        targetPressureValue += mainToggle.currentValue;
    }

    private IEnumerator GameWon()
    {
        save.changeFlag("valve");
        chooser.Choose();
        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().isInputAvailable = true;
    }
}
