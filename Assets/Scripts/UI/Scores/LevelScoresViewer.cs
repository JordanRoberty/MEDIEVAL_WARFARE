using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class LevelScoresViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI level_text;
    [SerializeField] private Transform _scores_viewers_container;
    [SerializeField] private GameObject _score_viewer_prefab;

    public void init(int level_number, List<Score> level_scores)
    {
        Assert.IsTrue(level_scores.Count == 3);

        level_text.text = "Level " + (level_number + 1);

        _scores_viewers_container.destroy_children();
        for(int i = 0; i < level_scores.Count; ++i)
        {
            ScoreViewer score_viewer = Instantiate(
                _score_viewer_prefab,
                Vector3.zero,
                Quaternion.identity,
                _scores_viewers_container
            ).GetComponent<ScoreViewer>();

            score_viewer.init(i, level_scores[i]);
        }
    }
}
