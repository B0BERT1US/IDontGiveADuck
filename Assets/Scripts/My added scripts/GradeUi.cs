using UnityEngine;
using TMPro;

public class GradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gradeLabel;
    [SerializeField] private string prefix = " ";

    void Awake()
    {
        if (!gradeLabel)
            gradeLabel = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGradeChanged += HandleGradeChanged;

            // Initialise text with current grade if game already started
            HandleGradeChanged(GameManager.Instance.CurrentGrade);
        }
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGradeChanged -= HandleGradeChanged;
        }
    }

    private void HandleGradeChanged(string grade)
    {
        if (string.IsNullOrEmpty(grade))
            grade = "D";

        if (!gradeLabel) return;
        gradeLabel.text = prefix + grade;
    }

}
