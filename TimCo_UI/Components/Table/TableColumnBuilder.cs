using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TimCo_UI.Components.Table
{
	internal class TableColumnBuilder<T> : ITableColumnBuilder<T> where T : class
	{
		private readonly Table<T> _component;

		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="component"></param>
		public TableColumnBuilder(Table<T> component)
		{
			_component = component;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TDataType"></typeparam>
		/// <param name="expression"></param>
		/// <param name="hidden"></param>
		/// <returns></returns>
		public ITableColumn<T> CreateColumn<TDataType>(Expression<Func<T, TDataType>> expression, bool hidden)
		{
			bool isExpression = expression == null || expression.Body as MemberExpression != null;
			if (isExpression)
				return new TableColumn<T, TDataType>(_component, expression);

			throw new NotSupportedException(string.Format("Expression '{0}' not supported by table", expression));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="propertyInfo"></param>
		/// <returns></returns>
		public ITableColumn<T> CreateColumn(PropertyInfo propertyInfo)
		{
			ITableColumn<T> column;
			column = CreateColumn(propertyInfo, true);

			return column;
		}

		#region Private methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="propertyInfo"></param>
		/// <param name="hidden"></param>
		/// <returns></returns>
		private ITableColumn<T> CreateColumn(PropertyInfo propertyInfo, bool hidden)
		{
			Type entityType = typeof(T);
			Type columnType = typeof(TableColumn<,>).MakeGenericType(entityType, propertyInfo.PropertyType);

			ParameterExpression parameter = Expression.Parameter(entityType, "e");
			MemberExpression expressionProperty = Expression.Property(parameter, propertyInfo);

			Type funcType = typeof(Func<,>).MakeGenericType(entityType, propertyInfo.PropertyType);
			LambdaExpression lambda = Expression.Lambda(funcType, expressionProperty, parameter);

			var column = Activator.CreateInstance(columnType, lambda, _component) as ITableColumn<T>;
			return column;
		}

		#endregion
	}
}