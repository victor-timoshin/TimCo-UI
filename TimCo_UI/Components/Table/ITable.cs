using TimCo_UI.Components.Widget;

namespace TimCo_UI.Components.Table
{
	public enum OrientationTypes
	{
		Horizontal = 0,
		Vertical = 1
	}

	public interface ITable : IHtmlAttributesContainer
	{
		ITableColumnContainer Columns { get; }
		OrientationTypes Orientation { get; set; }
		string TableName { get; set; }
		string RowId { get; set; }
		string RowClass { get; set; }
		bool IsPageable { get; set; }
	}
}