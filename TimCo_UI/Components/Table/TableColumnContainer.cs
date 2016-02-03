using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TimCo_UI.Components.Table
{
	public class TableColumnContainer<TModel> : KeyedCollection<string, ITableColumn>, ITableColumnContainer<TModel>
	{
		private readonly ITableColumnBuilder<TModel> _columnBuilder;

		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="columnBuilder"></param>
		public TableColumnContainer(ITableColumnBuilder<TModel> columnBuilder)
		{
			_columnBuilder = columnBuilder;
		}

		#region Implementation of ITableColumnContainer<T>

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		public ITableColumn<TModel> Bound(ITableColumn<TModel> column)
		{
			try
			{
				base.Add(column);
			}
			catch (Exception)
			{
				throw new ArgumentException(string.Format("Column '{0}' already exist in the table", column.Name));
			}

			return column;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ITableColumn<TModel> Bound()
		{
			return Bound(false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hidden"></param>
		/// <returns></returns>
		public ITableColumn<TModel> Bound(bool hidden)
		{
			return Bound((Expression<Func<TModel, string>>)null, hidden);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="constraint"></param>
		/// <returns></returns>
		public ITableColumn<TModel> Bound<TKey>(Expression<Func<TModel, TKey>> constraint)
		{
			return Bound(constraint, false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="constraint"></param>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public ITableColumn<TModel> Bound<TKey>(Expression<Func<TModel, TKey>> constraint, string columnName)
		{
			ITableColumn<TModel> newColumn = CreateColumn(constraint, false, columnName);
			return Bound(newColumn);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="constraint"></param>
		/// <param name="hidden"></param>
		/// <returns></returns>
		public ITableColumn<TModel> Bound<TKey>(Expression<Func<TModel, TKey>> constraint, bool hidden)
		{
			ITableColumn<TModel> newColumn = CreateColumn(constraint, hidden, string.Empty);
			return Bound(newColumn);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="propertyInfo"></param>
		/// <returns></returns>
		public ITableColumn<TModel> Bound(PropertyInfo propertyInfo)
		{
			ITableColumn<TModel> newColumn = _columnBuilder.CreateColumn(propertyInfo);
			if (newColumn == null)
				return null;

			return Bound(newColumn);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public new IEnumerator<ITableColumn> GetEnumerator()
		{
			return base.GetEnumerator();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public ITableColumn GetByName(string name)
		{
			return this.FirstOrDefault(i => i.Name.ToUpper() == name.ToUpper());
		}

		#endregion

		#region Implementation of KeyedCollection<string, ITableColumn>

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected override string GetKeyForItem(ITableColumn item)
		{
			return item.Name;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="constraint"></param>
		/// <param name="hidden"></param>
		/// <param name="columnName"></param>
		/// <returns></returns>
		private ITableColumn<TModel> CreateColumn<TKey>(Expression<Func<TModel, TKey>> constraint, bool hidden, string columnName)
		{
			ITableColumn<TModel> newColumn = _columnBuilder.CreateColumn(constraint, hidden);
			if (!string.IsNullOrEmpty(columnName))
				newColumn.Name = columnName;

			return newColumn;
		}

		#endregion
	}
}