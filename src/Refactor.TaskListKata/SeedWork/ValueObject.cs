using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Refactor.TaskListKata.SeedWork;

/// <summary>
/// The value object class
/// </summary>
public abstract class ValueObject<T> where T : ValueObject<T>
{
    /// <summary>
    /// The members
    /// </summary>
    private static readonly Member[] Members = GetMembers().ToArray();

    /// <summary>
    ///     Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///     <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object other)
    {
        if (ReferenceEquals(objA: null, objB: other))
        {
            return false;
        }

        if (ReferenceEquals(this, objB: other))
        {
            return true;
        }

        var members = Members;

        return other.GetType() == typeof(T) &&
               Members.All(m =>
               {
                   var otherValue = m.GetValue(other);
                   var thisValue = m.GetValue(this);
                   return m.IsNonStringEnumerable
                       ? GetEnumerableValues(otherValue).SequenceEqual(GetEnumerableValues(thisValue))
                       : otherValue?.Equals(thisValue) ?? thisValue == null;
               });
    }

    /// <summary>
    /// Nots the equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns></returns>
    public bool NotEquals(object other)
    {
        return Equals(other).Equals(false);
    }

    /// <summary>
    ///     Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
        return CombineHashCodes(
            Members.Select(m => m.IsNonStringEnumerable
                ? CombineHashCodes(GetEnumerableValues(m.GetValue(this)))
                : m.GetValue(this)));
    }

    /// <summary>
    ///     Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    ///     The result of the operator.
    /// </returns>
    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
    {
        return Equals(objA: left, objB: right);
    }

    /// <summary>
    ///     Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    ///     The result of the operator.
    /// </returns>
    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !Equals(objA: left, objB: right);
    }

    /// <summary>
    ///     Converts to string.
    /// </summary>
    /// <returns>
    ///     A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        if (Members.Length == 1)
        {
            var m = Members[0];
            var value = m.GetValue(this);
            return m.IsNonStringEnumerable
                ? $"{string.Join(separator: "|", GetEnumerableValues(value))}"
                : value.ToString();
        }

        var values = Members.Select(m =>
        {
            var value = m.GetValue(this);
            return m.IsNonStringEnumerable
                ? $"{m.Name}:{string.Join(separator: "|", GetEnumerableValues(value))}"
                : m.Type != typeof(string)
                    ? $"{m.Name}:{value}"
                    : value == null
                        ? $"{m.Name}:null"
                        : $"{m.Name}:\"{value}\"";
        });
        return $"{typeof(T).Name}[{string.Join(separator: "|", values: values)}]";
    }

    /// <summary>
    ///     Gets the members.
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<Member> GetMembers()
    {
        var t = typeof(T);
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;
        while (t != typeof(object))
        {
            if (t == null)
            {
                continue;
            }

            foreach (var p in t.GetProperties(flags))
            {
                yield return new Member(p);
            }

            foreach (var f in t.GetFields(flags))
            {
                yield return new Member(f);
            }

            t = t.BaseType;
        }
    }

    /// <summary>
    ///     Gets the enumerable values.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns></returns>
    private static IEnumerable<object> GetEnumerableValues(object obj)
    {
        var enumerator = ((IEnumerable)obj).GetEnumerator();
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
    }

    /// <summary>
    ///     Combines the hash codes.
    /// </summary>
    /// <param name="objs">The objs.</param>
    /// <returns></returns>
    private static int CombineHashCodes(IEnumerable<object> objs)
    {
        unchecked
        {
            return objs.Aggregate(seed: 17, func: (current, obj) => current * 59 + (obj?.GetHashCode() ?? 0));
        }
    }

    /// <summary>
    /// </summary>
    private readonly struct Member
    {
        /// <summary>
        ///     The name
        /// </summary>
        public readonly string Name;

        /// <summary>
        ///     The get value
        /// </summary>
        public readonly Func<object, object> GetValue;

        /// <summary>
        ///     The is non string enumerable
        /// </summary>
        public readonly bool IsNonStringEnumerable;

        /// <summary>
        ///     The type
        /// </summary>
        public readonly Type Type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Member" /> struct.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <exception cref="ArgumentException"></exception>
        public Member(MemberInfo info)
        {
            switch (info)
            {
                case FieldInfo field:
                    Name = field.Name;
                    GetValue = obj => field.GetValue(obj);
                    IsNonStringEnumerable = typeof(IEnumerable).IsAssignableFrom(field.FieldType) &&
                                            field.FieldType != typeof(string);
                    Type = field.FieldType;
                    break;
                case PropertyInfo prop:
                    Name = prop.Name;
                    GetValue = obj => prop.GetValue(obj);
                    IsNonStringEnumerable = typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) &&
                                            prop.PropertyType != typeof(string);
                    Type = prop.PropertyType;
                    break;
                default:
                    throw new ArgumentException(message: "Member is not a field or property?!?!",
                        paramName: info.Name);
            }
        }
    }
}