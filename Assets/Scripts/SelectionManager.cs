using System.Collections.Generic;

public class SelectionManager
{
    private static SelectionManager _instance;

    public static SelectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SelectionManager();
            }

            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    public HashSet<RSPObject> SelectedUnits = new HashSet<RSPObject>();
    public List<RSPObject> AvailableUnits = new List<RSPObject>();

    private SelectionManager() { }

    public void Select(RSPObject Unit)
    {
        SelectedUnits.Add(Unit);
        Unit.Onselected();
    }

    public void Deselect(RSPObject Unit)
    {
        Unit.OnDeselected();
        SelectedUnits.Remove(Unit);
    }

    public void DeselectAll()
    {
        foreach (RSPObject Unit in SelectedUnits)
        {
            Unit.OnDeselected();
        }

        SelectedUnits.Clear();
    }

    public bool IsSelected(RSPObject Unit)
    {
        return SelectedUnits.Contains(Unit);
    }
}
