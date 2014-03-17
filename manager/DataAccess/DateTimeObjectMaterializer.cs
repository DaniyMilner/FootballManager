using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DateTimeObjectMaterializer
    {
        private static DateTime SpecifyUtcKind(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }

        public static void Materialize(object entity)
        {
            var properties = entity.GetType().GetProperties().ToList();

            properties.Where(property => property.PropertyType == typeof(DateTime))
                .ToList()
                .ForEach(delegate(PropertyInfo info)
                {
                    var datetime = (DateTime)info.GetValue(entity, null);
                    if (info.CanWrite)
                        info.SetValue(entity, SpecifyUtcKind(datetime), null);
                });

            properties.Where(property => property.PropertyType == typeof(DateTime?))
                .ToList()
                .ForEach(delegate(PropertyInfo info)
                {
                    var datetime = (DateTime?)info.GetValue(entity, null);
                    if (datetime.HasValue)
                    {
                        info.SetValue(entity, SpecifyUtcKind(datetime.Value), null);
                    }
                });
        }
    }
}
