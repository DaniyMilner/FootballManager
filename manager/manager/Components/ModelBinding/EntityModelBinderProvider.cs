using System;
using System.Web.Mvc;
using DomainModel.Entities;

namespace manager.Components.ModelBinding
{
    public class EntityModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (!typeof(Entity).IsAssignableFrom(modelType))
                return null;

            Type modelBinderType = typeof(IEntityModelBinder<>).MakeGenericType(modelType);
            return (IModelBinder)DependencyResolver.Current.GetService(modelBinderType);
        }
    }
}