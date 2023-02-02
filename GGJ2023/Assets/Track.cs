using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Track : MonoBehaviour
{
    public List<Slider> Lanes;
    public Seasons CurrentSeason;
    [Range(0, 1000)]
    public int SeasonStart;
    [Range(0, 50)]
    public int SeasonEnd;
    public RectTransform SeasonHandle;

    private float TrackGraphicEndPosition = 1000f; 

    private void Start()
    {
        
    }

    private void Update()
    {
        if (SeasonStart > 0 && SeasonStart < TrackGraphicEndPosition)
            SeasonHandle.anchoredPosition = new Vector2(SeasonStart,0); 

        if(SeasonEnd > 0 && SeasonEnd < 50)
        {
            SeasonHandle.sizeDelta = new Vector2(SeasonEnd * 10, 0); 
        }
    }

    public bool isInSeasonTerritory(Slider Lane)
    {
        return Lane.value > SeasonStart && Lane.value < SeasonEnd;
    }

    public int Rank(Slider Lane)
    {
        var RankedLanes = Lanes.OrderBy(ctx => ctx.value).ToList();

        return RankedLanes.IndexOf(Lane);
    }
}
