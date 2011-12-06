using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
#if RESHARPER_5
using JetBrains.ReSharper.UnitTestFramework;
#else
using JetBrains.ReSharper.UnitTestExplorer;
#endif

using Simple.Testing.ReSharperRunner.Presentation;

namespace Simple.Testing.ReSharperRunner.Factories
{
	using Presentation;

	internal class ContextFactory
  {
    readonly string _assemblyPath;

    readonly IProjectModelElement _project;
    readonly IUnitTestProvider _provider;
    readonly ContextCache _cache;

    public ContextFactory(IUnitTestProvider provider, IProjectModelElement project, string assemblyPath, ContextCache cache)
    {
      _provider = provider;
      _cache = cache;
       _project = project;
      _assemblyPath = assemblyPath;
    }

    public ContextElement CreateContext(ITypeElement type)
    {
      if (_cache.Classes.ContainsKey(type))
      {
        return _cache.Classes[type];
      }

      ContextElement context = new ContextElement(_provider,
                                                  _project,
                                                  type.CLRName,
                                                  _assemblyPath);
      _cache.Classes.Add(type, context);
      return context;
    }

    public ContextElement CreateContext(IMetadataTypeInfo type)
    {
      return new ContextElement(_provider,
                                _project,
                                type.FullyQualifiedName,
                                _assemblyPath);
    }
  }
}
