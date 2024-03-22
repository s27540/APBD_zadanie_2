using System.Text;

namespace zadanie_2;

public class ContainerShip
{
    private List<Container> _loadedContainers = new List<Container>();
    private double _maxSpeedKT;
    private int _maxAmountOfContainers;
    private double _maxMassOfContainersT;

    public ContainerShip(double maxSpeedKt, int maxAmountOfContainers, double maxMassOfContainersT)
    {
        _maxSpeedKT = maxSpeedKt;
        _maxAmountOfContainers = maxAmountOfContainers;
        _maxMassOfContainersT = maxMassOfContainersT;
    }

    public void LoadListOfContainers(List<Container> containersToLoad)
    {
        
        if (_loadedContainers.Count + containersToLoad.Count > _maxAmountOfContainers)
        {
            throw new InvalidOperationException("Adding these containers would exceed the maximum amount of containers allowed on the ship.");
        }

        double totalMassOfNewContainers = 0;
        foreach (var container in containersToLoad)
        {
            totalMassOfNewContainers += container.getLoadMass();
        }
        
        double totalMassCurrentContainers = 0;
        foreach (var container in containersToLoad)
        {
            totalMassCurrentContainers += container.getLoadMass();
        }

        if (totalMassOfNewContainers + totalMassCurrentContainers > _maxMassOfContainersT * 1000)
        {
            throw new InvalidOperationException("Adding these containers would exceed the maximum mass of containers allowed on the ship.");
        }

        _loadedContainers.AddRange(containersToLoad);
    }

    public void LoadContainer(Container containerToLoad)
    {
        List<Container> containersToAdd = new List<Container> { containerToLoad };
        LoadListOfContainers(containersToAdd);
    }

    public void RemoveContainerFromShip(Container containerToRemove)
    {
        _loadedContainers.Remove(containerToRemove);
    }

    public void SwitchContainers(string serialNumberToRemove, Container containerToAdd)
    {
        
        for (int i = 0; i < _loadedContainers.Count; i++)
        {
            if (_loadedContainers[i]._serialNumber.Equals(serialNumberToRemove))
            {
                _loadedContainers.RemoveAt(i);
                break;
            }
        }

        _loadedContainers.Add(containerToAdd);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Loaded Containers: [");
        foreach (var container in _loadedContainers)
        {
            if (container != null)
            {
                sb.AppendLine(container.ToString());
            }
        }
        sb.AppendLine($"], Max speed: {_maxSpeedKT}, Max amount of containers: {_maxAmountOfContainers}, Max mass of containers: {_maxMassOfContainersT}");

        return sb.ToString();
    }

}