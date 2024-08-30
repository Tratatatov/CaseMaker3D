using UnityEngine;
using Zenject;

public class TestInstaller : Installer<TestInstaller>
{
    [SerializeField] private GameObject meh;
    public override void InstallBindings()
    {
        GameObject newGameObject = Container.InstantiatePrefab(meh);
    }
}
