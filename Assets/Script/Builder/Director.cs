internal class Director
{
    private IBuilder _builder;    


    public Director(IBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Не менять порядок. Проверки оптимизированы под очередь.
    /// </summary>
    public void Construct()
    {
        _builder.BuildWaterCell();
        _builder.BuildSwampCell();       
        _builder.BuildSendCell();
        _builder.BuildGrassCell();
        _builder.BuildTownCell();
    }
}

