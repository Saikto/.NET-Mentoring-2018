using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyIoC.Attributes;

namespace MyIoC
{
	public class Container
	{
	    private readonly IDictionary<Type, Type> _registeredTypesDict;

	    public Container()
	    {
	        _registeredTypesDict = new Dictionary<Type, Type>();
	    }

	    public void AddAssembly(Assembly assembly)
	    {
	        var assemblyTypes = assembly.ExportedTypes;

	        foreach (var type in assemblyTypes)
	        {
	            var importConstructorAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
	            bool hasImportProperties = GetImportProperties(type).Any();

	            if (importConstructorAttribute != null || hasImportProperties)
	            {
                    _registeredTypesDict.Add(type,type);
	            }

	            var exportAttributes = type.GetCustomAttributes<ExportAttribute>();
	            foreach (var exportAttribute in exportAttributes)
	            {
	                _registeredTypesDict.Add(exportAttribute.Contract ?? type, type);
	            }
	        }
	    }

	    public void AddType(Type type)
	    {
            _registeredTypesDict.Add(type, type);
	    }

	    public void AddType(Type type, Type baseType)
	    {
            _registeredTypesDict.Add(baseType, type);
	    }

		public object CreateInstance(Type type)
		{
		    var instance = GetInstance(type);
		    return instance;
		}

		public T CreateInstance<T>()
		{
		    var type = typeof(T);
		    var instance = (T)GetInstance(type);
		    return instance;

		}

	    private object GetInstance(Type type)
	    {

	        Type dependentType;
	        try
	        {
	            dependentType = _registeredTypesDict[type];
	        }
	        catch(KeyNotFoundException)
	        {
	            throw new KeyNotFoundException(
	                $"Can't create instance of {type.FullName} because dependency is not provided.");
	        }

            ConstructorInfo constructorInfo;
	        try
	        {
	            constructorInfo = dependentType.GetConstructors().First();
	        }
	        catch (ArgumentNullException)
	        {
	            throw new ArgumentNullException(
	                $"Can't create instance of {dependentType.FullName} because there are no public constructors for this type.");
            }

	        object instance = ConstructInstance(dependentType, constructorInfo);

	        if (dependentType.GetCustomAttribute<ImportConstructorAttribute>() != null)
	        {
	            return instance;
	        }

	        var propertiesInfo = GetImportProperties(dependentType);
	        foreach (var propertyInfo in propertiesInfo)
	        {
	            propertyInfo.SetValue(instance, GetInstance(propertyInfo.PropertyType));
	        }

	        return instance;
	    }

	    private object ConstructInstance(Type type, ConstructorInfo constructorInfo)
	    {
	        ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
            List<object> instancesOfParameters = new List<object>();

	        foreach (var parameterInfo in parameterInfos)
	        {
	            instancesOfParameters.Add(GetInstance(parameterInfo.ParameterType));
	        }

	        object instance = Activator.CreateInstance(type, instancesOfParameters.ToArray());
	        return instance;
	    }

	    private IEnumerable<PropertyInfo> GetImportProperties(Type type)
	    {
	        return type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null);
	    }
	}
}
