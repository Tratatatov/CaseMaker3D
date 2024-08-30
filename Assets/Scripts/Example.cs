using UnityEngine;
using Zenject;

public class Example : MonoBehaviour
{

    [SerializeField] private Renderer _renderer;

    // Factories 
    private PaintMaterialFactory _paintFactory;
    private SimpleMaterialFactory _simpleFactory;

    // Strategies
    [SerializeField]private PaintStrategy _paintStrategy;
    private DefaultStrategy _defaultStrategy;
    private DecorationStrategy _currentStrategy;

    [Inject]
    private void Construct(PaintStrategy paintStrategy, PaintMaterialFactory paintMaterialFactory, SimpleMaterialFactory simpleMaterialFactory)
    {
        _paintStrategy = paintStrategy;
        _simpleFactory = simpleMaterialFactory;
        _paintFactory = paintMaterialFactory;
    }

    private void SetStrategy(DecorationStrategy strategy)
    {
        if (_currentStrategy != null)
            _currentStrategy.Exit();
        _currentStrategy = strategy;
        _currentStrategy.Enter();
    }

    private void Update()
    {
        _currentStrategy.Update();
    }
    public void SetGlitterPaint()
    {
        _renderer.material = _paintFactory.GetMaterial(MaterialType.Glitter);
        SetStrategy(_defaultStrategy);
    }
    public void SetAcrylicPaint()
    {
        _renderer.material = _paintFactory.GetMaterial(MaterialType.Acrylic);
        SetStrategy(_paintStrategy);
    }
    public void SetGradientPaint()
    {
        _renderer.material = _paintFactory.GetMaterial(MaterialType.Gradient);
        SetStrategy(_paintStrategy);
    }

    public void SetGlitter()
    {
        _renderer.material = _simpleFactory.GetMaterial(MaterialType.Glitter);
        SetStrategy(_defaultStrategy);
    }

    public void SetAcrylic()
    {
        _renderer.material = _simpleFactory.GetMaterial(MaterialType.Acrylic);
        SetStrategy(_defaultStrategy);
    }

    public void SetGradient()
    {
        _renderer.material = _simpleFactory.GetMaterial(MaterialType.Gradient);
        SetStrategy(_defaultStrategy);
    }

    

    private void Awake()
    {
        _paintStrategy.Init(_renderer);
        _defaultStrategy = new DefaultStrategy();
        SetStrategy(_defaultStrategy);
    }




}
