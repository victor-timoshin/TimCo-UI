using System;
using System.Linq.Expressions;
using System.Web;

namespace TimCo_UI.Components.Table
{
	public class TableColumn<T, TDataType> : TableColumnBase<T> where T : class
	{
		private readonly Table<T> _component;
		private readonly Func<T, TDataType> _constraint;

		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="component"></param>
		public TableColumn(Table<T> component, Expression<Func<T, TDataType>> expression)
		{
			_component = component;

			/* если были переданы данные */
			if (expression != null)
				_constraint = expression.Compile();
		}

		#region Implementation of TableColumnBase<T>

		/// <summary>
		/// 
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public override ITableCell GetCell(object instance)
		{
			return GetValue((T)instance);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public override ITableCell GetValue(T instance)
		{
			string textValue;

			if (ValueConstraint != null)
				textValue = ValueConstraint(instance);
			else
			{
				if (_constraint == null)
					throw new InvalidOperationException("You need to specify render expression using RenderValueAs");

				TDataType value = _constraint(instance);
				textValue = string.IsNullOrEmpty(ValuePattern) ? HttpUtility.HtmlEncode(value.ToString()) : HttpUtility.HtmlDecode(string.Format(ValuePattern, value));
			}

			return new TableCell(textValue);
		}

		/// <summary>
		/// Сортировка колонки
		/// </summary>
		/// <param name="sort"></param>
		/// <returns></returns>
		public override ITableColumn<T> Sortable(bool sort)
		{
			if (sort && _constraint == null)
				return this;

			SortEnabled = sort;
			return this;
		}

		#endregion
	}
}