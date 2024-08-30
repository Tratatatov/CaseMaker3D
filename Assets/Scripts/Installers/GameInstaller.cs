using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Factories")]
    [SerializeField] private PaintMaterialFactory _paintFactory;
    [SerializeField] private SimpleMaterialFactory _simpleMaterialFactory;
    [Header("Strategies")]
    [SerializeField] private PaintConfig _paintConfigData;
    private PaintStrategy _paintStrategy;
    public override void InstallBindings()
    {
        MaterialFactoriesBindings();
        StrategiesBindings();
    }
    private void MaterialFactoriesBindings()
    {
        Container.Bind<PaintMaterialFactory>().FromInstance(_paintFactory).AsSingle();
        Container.Bind<SimpleMaterialFactory>().FromInstance(_simpleMaterialFactory).AsSingle();
    }
    private void StrategiesBindings()
    {
        Container.Bind<PaintConfig>().FromInstance(_paintConfigData).AsSingle().NonLazy();
        Container.Bind<PaintStrategy>().FromNew().AsSingle().NonLazy();

    }
}