using AutoMapper;
using System.Reflection;
namespace ECommerce.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            RegisterMappingsFromConfiguration(MappingConfiguration.ModelViewModelMappings);
        }
        private void RegisterMappingsFromConfiguration(Dictionary<Type, Type[]> mappings)
        {
            foreach (var mapping in mappings)
            {
                var modelType = mapping.Key;
                var viewModelTypes = mapping.Value;

                foreach (var viewModelType in viewModelTypes)
                {
                    CreateMapForTypes(modelType, viewModelType);
                }
            }
        }

        private void CreateMapForTypes(Type model, Type viewModel)
        {
            var createMapMethod = typeof(Profile).GetMethod("CreateMap", new Type[] { }).MakeGenericMethod(model, viewModel);
            var mapExpression = createMapMethod.Invoke(this, null);

            var reverseMapMethod = mapExpression.GetType().GetMethod("ReverseMap");
            reverseMapMethod.Invoke(mapExpression, null);
        }
    }
}
