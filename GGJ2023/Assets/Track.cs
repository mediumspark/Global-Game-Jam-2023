using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum Seasons
{
    Winter,
    FoolsSpring,
    SpringOfDeception,
    ThirdWinter,
    ThePollening,
    Spring,
    Summer,
    Hell,
    FalseFall,
    SecondSummer,
    Fall
}
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
        int season = Random.Range(0, 11);
        CurrentSeason = (Seasons)season;
        SeasonStart = Random.Range(0, 600);
        SeasonEnd = Random.Range(0, 50); 

        switch (CurrentSeason)
        {
            case Seasons.Winter:
                SeasonHandle.GetComponent<Image>().color = Color.white;
                break;

            case Seasons.FoolsSpring:
                SeasonHandle.GetComponent<Image>().color = new Color(.1f, 0.5f, 0, 1);
                break;

            case Seasons.SpringOfDeception:
                SeasonHandle.GetComponent<Image>().color = new Color(.3f, 0.1f, 0, 1);
                break;

            case Seasons.ThirdWinter:
                SeasonHandle.GetComponent<Image>().color = Color.gray;
                break;

            case Seasons.ThePollening:
                SeasonHandle.GetComponent<Image>().color = Color.magenta;
                break; 

            case Seasons.Spring:
                SeasonHandle.GetComponent<Image>().color = Color.green;
                break;

            case Seasons.Summer:
                SeasonHandle.GetComponent<Image>().color = Color.yellow;
                break;

            case Seasons.Hell:
                SeasonHandle.GetComponent<Image>().color = Color.red;
                break;

            case Seasons.FalseFall:
                SeasonHandle.GetComponent<Image>().color = Color.cyan;
                break;

            case Seasons.SecondSummer:
                SeasonHandle.GetComponent<Image>().color = new Color(1, 1, 0, .5f);
                break;

            case Seasons.Fall:
                SeasonHandle.GetComponent<Image>().color = new Color(1, 0.64f, 0); 
                break;
        }
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
