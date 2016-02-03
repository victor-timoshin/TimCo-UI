using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace TimCo_UI.Components.Table
{
	public interface ITableColumnContainer : IEnumerable<ITableColumn>
	{
		ITableColumn GetByName(string name);
	}

	public interface ITableColumnContainer<T> : ITableColumnContainer
	{
		ITableColumn<T> Bound(ITableColumn<T> column);

		ITableColumn<T> Bound();

		ITableColumn<T> Bound(bool hidden);

		ITableColumn<T> Bound<TKey>(Expression<Func<T, TKey>> constraint);

		ITableColumn<T> Bound<TKey>(Expression<Func<T, TKey>> constraint, string columnName);

		ITableColumn<T> Bound<TKey>(Expression<Func<T, TKey>> constraint, bool hidden);

		ITableColumn<T> Bound(PropertyInfo propertyInfo);
	}
}