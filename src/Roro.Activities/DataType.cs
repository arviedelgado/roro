using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;

namespace Roro.Activities
{
    [DataContract]
    public abstract class DataType
    {
        [DataMember]
        public string Id { get; protected set; }

        [DataMember]
        public string Name { get; protected set; }

        public abstract bool TrySetValue(object value);

        public static List<DataType> GetCommonTypes()
        {
            return new List<DataType>()
            {
                new Text(),
                new Number(),
                new Flag(),
                new DateTime(),
                new Password(),
                new Collection()
            };
        }

        public static DataType GetTypeById(string id)
        {
            if (AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(DataType).IsAssignableFrom(x) && !x.IsAbstract && !x.IsGenericType)
                .Select(x => Activator.CreateInstance(x) as DataType)
                .FirstOrDefault(x => x.Id == id) is DataType type)
            {
                return type;
            }
            throw new TypeLoadException();
        }
    }

    [DataContract]
    public abstract class DataType<T> : DataType
    {
        [DataMember]
        protected T Value { get; set; }

        protected DataType(T value)
        {
            this.Id = typeof(T).FullName;
            this.Name = this.GetType().Name;
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is DataType<T> other && this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public sealed class Text : DataType<String>
    {
        public Text() : this(string.Empty) { }

        public Text(string value) : base(value ?? string.Empty) { }

        public override bool TrySetValue(object value)
        {
            try
            {
                this.Value = Convert.ToString(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static implicit operator String(Text item) => item.Value;

        public static implicit operator Text(String item) => new Text(item);
    }

    public sealed class Number : DataType<Decimal>
    {
        public Number() : this(new Decimal()) { }

        public Number(Decimal value) : base(value) { }

        public override bool TrySetValue(object value)
        {
            try
            {
                this.Value = Convert.ToDecimal(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static implicit operator Decimal(Number item) => item.Value;

        public static implicit operator Number(Decimal item) => new Number(item);

        public static implicit operator Int32(Number item) => Convert.ToInt32(item.Value);
    }

    public sealed class Flag : DataType<Boolean>
    {
        public Flag() : this(new Boolean()) { }

        public Flag(Boolean value) : base(value) { }

        public override bool TrySetValue(object value)
        {
            try
            {
                this.Value = Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static implicit operator Boolean(Flag item) => item.Value;

        public static implicit operator Flag(Boolean item) => new Flag(item);
    }

    public sealed class DateTime : DataType<System.DateTime>
    {
        public DateTime() : this(new System.DateTime()) { }

        public DateTime(System.DateTime value) : base(value) { }

        public override bool TrySetValue(object value)
        {
            try
            {
                this.Value = Convert.ToDateTime(value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static implicit operator System.DateTime(DateTime item) => item.Value;

        public static implicit operator DateTime(System.DateTime item) => new DateTime(item);
    }

    public sealed class Collection : DataType<DataTable>
    {
        public Collection() : this(new DataTable()) { }

        public Collection(DataTable value) : base(value) { }

        public override bool TrySetValue(object value)
        {
            try
            {
                this.Value = (DataTable)value;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static implicit operator DataTable(Collection item) => item.Value;

        public static implicit operator Collection(DataTable item) => new Collection(item);
    }

    public sealed class Password : DataType<SecureString>
    {
        public Password() : this(new SecureString()) { }

        public Password(SecureString value) : base(value) { }

        public override bool TrySetValue(object value)
        {
            try
            {
                this.Value.Clear();
                Convert.ToString(value).ToList().ForEach(this.Value.AppendChar);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static implicit operator SecureString(Password item) => item.Value;

        public static implicit operator Password(SecureString item) => new Password(item);
    }
}
