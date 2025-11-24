using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderUI : MonoBehaviour
{
    [SerializeField] private Slider slider;


    [SerializeField] private Gradient fillColor;

    private Image _fillImage;
    private float _totalTime;
    private LevelData _cachedLevel;

    private void Awake()
    {
        if (!slider) slider = GetComponent<Slider>();
        if (slider.fillRect)
            _fillImage = slider.fillRect.GetComponent<Image>();

        var fillArea = slider.transform.Find("Fill Area") as RectTransform;
        if (fillArea)
        {
            fillArea.offsetMin = new Vector2(0, fillArea.offsetMin.y);
            fillArea.offsetMax = new Vector2(0, fillArea.offsetMax.y);
        }

        if (fillColor == null)
        {
            var g = new Gradient();
            g.SetKeys(
                new[] {
                    new GradientColorKey(Color.green, 0f),
                    new GradientColorKey(Color.yellow, 0.5f),
                    new GradientColorKey(Color.red, 1f)
                },
                new[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f)
                }
            );
            fillColor = g;
        }

        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.interactable = false;
    }

    private void OnEnable()
    {
        RefreshTotalTime(force: true);
        UpdateUI();
    }

    private void Update()
    {
        RefreshTotalTime(force: false);
        UpdateUI();
    }

    private void RefreshTotalTime(bool force)
    {
        var gm = GameManager.Instance;
        if (!gm) return;

        if (force || gm.CurrentLevel != _cachedLevel)
        {
            _cachedLevel = gm.CurrentLevel;
            _totalTime = (_cachedLevel != null && _cachedLevel.timeLimit > 0f)
                ? _cachedLevel.timeLimit
                : Mathf.Max(1f, gm.TimeLeft);
        }
    }

    private void UpdateUI()
    {
        var gm = GameManager.Instance;
        if (!gm || _totalTime <= 0f) return;

        if (gm.TimeLeft <= 0.001f)
        {
            slider.value = 0f;
            if (_fillImage) _fillImage.color = fillColor.Evaluate(1f);
     
        }

        float ratio = Mathf.Clamp01(gm.TimeLeft / _totalTime);

        slider.value = ratio;

        if (_fillImage)
            _fillImage.color = fillColor.Evaluate(1f - ratio);

       
    }


}