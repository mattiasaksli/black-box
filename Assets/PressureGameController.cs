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
    public float currentPressureValue = 0;
    public float targetPressureValue;
    public float lerpTime = 1f;
    public float loseTimer = 5f;

    void Start()
    {
        currentPressureValue += thermalToggle.currentValue;
        currentPressureValue += lowerToggle.currentValue;
        currentPressureValue += emergencyToggle.currentValue;
        currentPressureValue += mainToggle.currentValue;
        targetPressureValue = currentPressureValue;
    }


    void Update()
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
            loseTimer = Mathf.Min(5, loseTimer + Time.deltaTime * 2);
        }

        timeProgressor.SetValue(5 - loseTimer);

        //Lose
        if (loseTimer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PressureFailure();
            gameObject.SetActive(false);
        }

        //Win
        if (currentPressureValue == targetPressureValue && targetPressureValue == 0.6f)
        {
            GameEventMessage.SendEvent("PressureWon");
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
        yield return new WaitForSeconds(1f);
        // Save state and play sounds
    }
}
