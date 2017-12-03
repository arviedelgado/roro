using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Roro.Activities
{
    [DataContract]
    public abstract class DataType
    {
        [DataMember]
        public string Id { get; protected set; }

        [DataMember]
        public string Name { get; protected set; }

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

        public static implicit operator String(Text item) => item.Value;

        public static implicit operator Text(String item) => new Text(item);
    }

    public sealed class Number : DataType<Decimal>
    {
        public Number() : this(0) { }

        public Number(Decimal value) : base(value) { }

        public static implicit operator Decimal(Number item) => item.Value;

        public static implicit operator Number(Decimal item) => new Number(item);

        public static implicit operator Int32(Number item) => Convert.ToInt32(item.Value);
    }

    public sealed class Flag : DataType<Boolean>
    {
        public Flag() : this(false) { }

        public Flag(Boolean value) : base(value) { }

        public static implicit operator Boolean(Flag item) => item.Value;

        public static implicit operator Flag(Boolean item) => new Flag(item);
    }

    public sealed class DateAndTime : DataType<DateTime>
    {
        public DateAndTime() : this(DateTime.MinValue) { }

        public DateAndTime(DateTime value) : base(value) { }

        public static implicit operator DateTime(DateAndTime item) => item.Value;

        public static implicit operator DateAndTime(DateTime item) => new DateAndTime(item);
    }

    public sealed class DataTypeColumn : DataGridViewComboBoxColumn
    {
        public DataTypeColumn()
        {
            this.CellTemplate = new DataTypeCell();
            this.ValueType = typeof(DataType);
            this.DataSource = new List<DataType>() { new Text(), new Number() };
            this.ValueMember = "Id";
            this.DisplayMember = "Name";
            this.DisplayStyleForCurrentCellOnly = true;
        }
    }

    public sealed class DataTypeCell : DataGridViewComboBoxCell
    {
        public void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.DataSource = new List<DataType>(this.DataSource as List<DataType>)
            {
                DataType.GetTypeById(this.Value.ToString())
            };
            e.ThrowException = false;
        }
    }
 }
