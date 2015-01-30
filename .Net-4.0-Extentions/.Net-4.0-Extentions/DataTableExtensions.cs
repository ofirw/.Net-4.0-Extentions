namespace Net_4._0_Extentions
{
    using System.Data;

    public static class DataTableExtensions
    {
        public static void AddOrMerge(this DataTableCollection tables, DataTable dt)
        {
            if (tables.Contains(dt.TableName))
            {
                tables[dt.TableName].Merge(dt);
            }
            else
            {
                tables.Add(dt);
            }
        }
    }
}