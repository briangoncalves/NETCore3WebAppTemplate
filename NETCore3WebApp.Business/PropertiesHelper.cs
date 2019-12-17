using System.Linq;

namespace NETCore3WebApp.Business
{
    public static class PropertiesHelper
    {
        public static void CopyProperties<T>(T fromEntity, T toEntity)
        {
            var properties = fromEntity.GetType().GetProperties().Where(x => x.Name.ToUpper() != "ID").ToList();
            properties.ForEach(item =>
            {
                var value = item.GetValue(fromEntity);
                item.SetValue(toEntity, value);
            });
        }
    }
}
