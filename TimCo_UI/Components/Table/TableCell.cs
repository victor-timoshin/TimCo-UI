using System.Web;

namespace TimCo_UI.Components.Table
{
	public class TableCell : ITableCell
	{
		private readonly string _value;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		public TableCell(string value)
		{
			_value = value;
		}

		#region Implementation of ITableCell

		/// <summary>
		/// 
		/// </summary>
		public string Value
		{
			get { return _value; }
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Value;
		}
	}
}