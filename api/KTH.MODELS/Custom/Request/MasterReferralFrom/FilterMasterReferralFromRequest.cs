using static KTH.MODELS.Constants.ConstantsMassage;
using System.Linq.Expressions;
using System.Reflection;
using KTH.MODELS.Custom.Response.MasterReferralFrom;

namespace KTH.MODELS.Custom.Request.MasterReferralFrom
{
    public class FilterMasterReferralFromRequest : FilterModel
    {
        public FilterMasterReferralFromRequest()
        {
            InitializeStringProperties();
        }

        public string? Name { get; set; }

        public bool? IsActive { get; set; }

        public static List<MasterReferralFromListResponseData> ApplySorting(List<MasterReferralFromListResponseData> queryable, string sortName, string sortType)
        {
            if (!string.IsNullOrEmpty(sortName))
            {
                PropertyInfo propertyInfo = typeof(MasterReferralFromListResponseData).GetProperty(sortName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(MasterReferralFromListResponseData), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var lambda = Expression.Lambda<Func<MasterReferralFromListResponseData, object>>(
                        Expression.Convert(property, typeof(object)), parameter);

                    if (!string.IsNullOrEmpty(sortType))
                    {
                        if (sortType.ToLower() == OrderByType.ASC.Value.ToLower())
                        {
                            return queryable.OrderBy(lambda.Compile()).ToList();
                        }
                        else if (sortType.ToLower() == OrderByType.DESC.Value.ToLower())
                        {
                            return queryable.OrderByDescending(lambda.Compile()).ToList();
                        }
                    }
                }
            }

            return queryable;
        }

        private void InitializeStringProperties()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string) && prop.CanWrite && prop.CanRead)
                {
                    prop.SetValue(this, string.Empty);
                }
            }
        }

        public void TrimAllProperties()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(string) && prop.CanWrite && prop.CanRead)
                {
                    var currentValue = prop.GetValue(this) as string;
                    if (currentValue != null)
                    {
                        prop.SetValue(this, currentValue.Trim());
                    }
                }
            }
        }
    }
}

