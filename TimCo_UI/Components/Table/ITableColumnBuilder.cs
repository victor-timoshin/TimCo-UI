using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TimCo_UI.Components.Table
{
	public interface ITableColumnBuilder<T>
	{
		ITableColumn<T> CreateColumn<TDataType>(Expression<Func<T, TDataType>> expression, bool hidden);

		ITableColumn<T> CreateColumn(PropertyInfo propertyInfo);
	}
}