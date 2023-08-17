using UnityEngine;

public class VoronoiColoring : MonoBehaviour
{
    public static VoronoiColoring instance = null;

    public MapGeneratorPreview mp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public static VoronoiColoring Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }
}
