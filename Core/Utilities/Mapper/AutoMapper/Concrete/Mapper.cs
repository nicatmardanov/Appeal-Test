using AutoMapper;
using System.Reflection;
using CustomIMapper = Core.Utilities.Mapper.IMapper;

namespace Auth.Core.Utilities.Mapper.AutoMapper.Concrete
{
    public class Mapper : CustomIMapper
    {
        private List<Type>? _mappedTypes = null;
        public Destination Map<Source, Destination>(Source source, bool reverseMap = false)
        {
            IMapper mapper = Config<Source, Destination>(reverseMap);
            return mapper.Map<Source, Destination>(source);
        }

        private IMapper Config<Source, Destination>(bool reverseMap)
        {
            ConfigMaps<Source, Destination>(out MapperConfiguration config, reverseMap);
            return config.CreateMapper();
        }

        private void ConfigMaps<Source, Destination>(out MapperConfiguration config, bool reverseMap)
        {
            Type sourceType = GetType(typeof(Source));
            Type destinationType = GetType(typeof(Destination));

            _mappedTypes = new() { sourceType };

            config = new MapperConfiguration(x =>
            {
                if (reverseMap)
                    x.CreateMap(sourceType, destinationType).ReverseMap();
                else
                    x.CreateMap(sourceType, destinationType);

                IEnumerable<PropertyInfo>? sourceTypeProperties = sourceType.GetProperties();
                IEnumerable<PropertyInfo>? destinationTypeProperties = destinationType.GetProperties();

                ConfigureProperties(sourceTypeProperties, destinationTypeProperties, ref x, reverseMap);
            });
        }

        private void ConfigureProperties(IEnumerable<PropertyInfo>? sourceTypeProperties, IEnumerable<PropertyInfo>? destinationTypeProperties, ref IMapperConfigurationExpression expression, bool reverseMap)
        {
            if (sourceTypeProperties is not null)
            {
                foreach (PropertyInfo? property in sourceTypeProperties)
                {
                    Type sourcePropertyType = GetType(property.PropertyType);
                    if (!sourcePropertyType.IsValueType && sourcePropertyType != typeof(string) && (sourcePropertyType.IsGenericType || sourcePropertyType.IsClass))
                    {
                        Type? destinationPropertyBaseType = destinationTypeProperties?.FirstOrDefault(x => x.Name == property.Name)?.PropertyType;
                        Type? destinationEquivalentPropertyType = destinationPropertyBaseType != null ? GetType(destinationPropertyBaseType) : null;

                        if (sourcePropertyType != null && destinationEquivalentPropertyType != null && !_mappedTypes!.Contains(sourcePropertyType))
                        {
                            if (reverseMap)
                                expression.CreateMap(sourcePropertyType, destinationEquivalentPropertyType).ReverseMap();
                            else
                                expression.CreateMap(sourcePropertyType, destinationEquivalentPropertyType);

                            _mappedTypes.Add(sourcePropertyType!);
                            ConfigureProperties(sourcePropertyType!.GetProperties(), destinationEquivalentPropertyType!.GetProperties(), ref expression, reverseMap);
                        }
                    }
                }
            }
        }

        private Type GetType(Type type)
        {
            if (type.IsGenericType)
                return type.GetGenericArguments()[0];

            return type;
        }
    }
}
