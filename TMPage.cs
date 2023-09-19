using System;

namespace TM.Desktop
{
    public class Page
    {
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public int RowIndex { get; set; }
        public int TotalRow { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<dynamic> query { get; set; }
        public Page() { }
        public Page(IEnumerable<dynamic> query, int PageNumber = 1, int PageSize = 15)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.TotalRow = query.Count();
            this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
            this.query = query.ToList().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }
        public List<dynamic> ToList()
        {
            return query.ToList();
        }

        public IEnumerable<T> PagesAnonymous<T>(IEnumerable<T> query, int PageNumber = 1, int PageSize = 15)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
            this.TotalRow = query.Count();
            this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
            return query.ToList().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }

        public List<T> ToList<T>(IEnumerable<T> query)
        {
            return PagesAnonymous(query).ToList();
        }

        //public IEnumerable<dynamic> DapperPage(IEnumerable<dynamic> query, int PageNumber, int PageSize)
        //{
        //    this.PageNumber = PageNumber;
        //    this.PageSize = PageSize;
        //    this.TotalRow = query.Count();
        //    this.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(this.TotalRow) / Convert.ToDecimal(PageSize)));
        //    return query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize);
        //}
        public string getRowIndexStr(Int32 index)
        {
            index = (index + (this.PageNumber - 1) * this.PageSize);
            if (index < 10)
                return "0" + index;
            else return index + "";
        }
        public int getRowIndex(int index) { return Convert.ToInt32(getRowIndexStr(index)); }
        private string ReplacePage(string href, int page)
        {
            return href.Replace("page=0", "page=" + page.ToString());
        }
    }
    public static class Dapper
    {
        public static int PageNumber { get; set; }
        public static int TotalPage { get; set; }
        public static int RowIndex { get; set; }
        public static int TotalRow { get; set; }
        public static int PageSize = 15;

        public static IEnumerable<dynamic> DapperPage(this IEnumerable<dynamic> query, int PageNumber, int PageSize)
        {
            Dapper.PageNumber = PageNumber;
            Dapper.PageSize = PageSize;
            Dapper.TotalRow = query.Count();
            Dapper.TotalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Dapper.TotalRow) / Convert.ToDecimal(PageSize)));
            return query.AsQueryable().Skip((PageNumber - 1) * PageSize).Take(PageSize); ;
        }
        public static string getRowIndexStr(Int32 index)
        {
            index = (index + (Dapper.PageNumber - 1) * Dapper.PageSize);
            if (index < 10)
                return "0" + index;
            else return index + "";
        }
        public static Int32 getRowIndex(Int32 index) { return Convert.ToInt32(getRowIndexStr(index)); }
        private static string ReplacePage(string href, int page)
        {
            return href.Replace("page=0", "page=" + page.ToString());
        }
    }
}