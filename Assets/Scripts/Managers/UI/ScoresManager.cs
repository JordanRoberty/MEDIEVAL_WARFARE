using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.GameFoundation;
using static GameFoundationUtils;

public class ScoresManager : Singleton<ScoresManager>
{
    [SerializeField] private GameObject _level_score_viewer_prefab;
    [SerializeField] private Transform _levels_scores_container;

    private void Start()
    {
        List<InventoryItem> levels_scores = get_inventory_items_from_tag("SCORE");
        Assert.IsNotNull(levels_scores);

        _levels_scores_container.destroy_children();

        for (int i = 0; i < levels_scores.Count; ++i)
        {
            LevelScoresViewer level_scores_viewer = Instantiate(
                _level_score_viewer_prefab,
                Vector3.zero,
                Quaternion.identity,
                _levels_scores_container
            ).GetComponent<LevelScoresViewer>();

            List<Score> scores = new List<Score>();
            for (int s = 0; s < 3; ++s)
            {
                scores.Add(new Score(
                    levels_scores[i].GetMutableProperty("name_" + s),
                    levels_scores[i].GetMutableProperty("score_" + s)
                ));
            }

            level_scores_viewer.init(i, scores);
        }
    }
}
