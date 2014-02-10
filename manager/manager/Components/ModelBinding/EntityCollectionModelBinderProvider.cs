using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Entities;

namespace manager.Components.ModelBinding
{
    public class EntityCollectionModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (!modelType.IsGenericType || !typeof(Entity).IsAssignableFrom(modelType.GetGenericArguments()[0]))
                return null;

            Type modelBinderType = typeof(IEntityCollectionModelBinder<>).MakeGenericType(modelType.GetGenericArguments()[0]);
            return (IModelBinder)DependencyResolver.Current.GetService(modelBinderType);
        }
    }
}