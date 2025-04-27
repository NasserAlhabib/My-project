using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    [SerializeField] private UpgradeDataSO[] availableUpgrades;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Up to maxChoices, one pick per UpgradeType category.
    /// </summary>
    public UpgradeDataSO[] GetUniqueUpgradesByCategory(int maxChoices)
    {
        // group by type, randomize group order
        var groups = availableUpgrades
            .GroupBy(u => u.type)
            .OrderBy(_ => Random.value)
            .ToList();

        var picks = new List<UpgradeDataSO>();
        foreach (var g in groups)
        {
            if (picks.Count >= maxChoices) break;
            var list = g.ToList();
            picks.Add(list[Random.Range(0, list.Count)]);
        }
        return picks.ToArray();
    }

    /// <summary>Queue into PlayerStats</summary>
    public void ApplyUpgrade(UpgradeDataSO u)
    {
        switch (u.type)
        {
            case UpgradeType.ExtraLife:
                // use your PongManager’s new method
                PongManager.Instance.GainExtraLife((int)u.value);
                break;

            case UpgradeType.PaddleExtension:
            case UpgradeType.PaddleSpeed:
                // your existing paddle upgrades
                PlayerStats.Instance.AddUpgrade(u);
                break;

                // handle other types here…
        }
    }
}
