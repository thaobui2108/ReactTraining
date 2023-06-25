using WebReactApi.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebReactApi.Core.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }

    public abstract class EntityBase : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
        public void MarkAsDeleted()
        {
            ObjectState = ObjectState.Deleted;
        }

        public object this[string propertyName]
        {
            get
            {
                var propertyInfo = GetType().GetProperty(propertyName);
                if (propertyInfo == null)
                    return null;

                var allowNull = propertyInfo.CustomAttributes.Any(x => x.AttributeType.Name == typeof(AllowNullAttribute).Name);
                var value = propertyInfo.GetValue(this, null);
                if (!allowNull)
                    return value ?? string.Empty;
                return value;
            }
            set
            {
                var propertyInfo = GetType().GetProperty(propertyName);
                if (propertyInfo == null) return;

                var isNullAbled = Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;
                var type = isNullAbled ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;
                if (type != null)
                {
                    var isEnum = type.IsEnum;
                    if (!isEnum)
                    {
                        var val = value == null ? null : Convert.ChangeType(value, type);
                        propertyInfo.SetValue(this, val, null);
                    }
                    else
                    {
                        var vlEnum = Enum.Parse(type, value.ToString(), true);
                        propertyInfo.SetValue(this, vlEnum, null);
                    }
                }
            }
        }
    }

    public abstract class EntityVersionable : EntityBase
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public abstract class Entity<T> : EntityBase, IEntity<T>, ICloneable
    {
        public virtual T Id { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public abstract class EntityVersionable<T> : EntityVersionable, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
