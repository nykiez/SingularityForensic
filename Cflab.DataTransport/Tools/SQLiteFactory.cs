using System.Data;
using System.Data.SQLite;
using Chloe.Entity;
using Chloe.Infrastructure;
using SQLiteContext = Chloe.SQLite.SQLiteContext;

namespace Cflab.DataTransport.Tools
{
    public class SQLiteFactory : IDbConnectionFactory
    {
        private readonly string connection;

        private SQLiteFactory(string connection)
        {
            this.connection = connection;
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connection);
        }

        /// <summary>
        /// 新建数据库链接
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static SQLiteContext NewContext(string path)
        {
            var conn = $"data source = {path}";
            var factory = new SQLiteFactory(conn);
            var context = new SQLiteContext(factory);
            return context;
        }
    }

    [Table("sqlite_master")]
    public class SQLiteMaster
    {
        [Column("type")]
        public string Type { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("tbl_name")]
        public string TblName { get; set; }

        [Column("rootpage")]
        public long RootPage { get; set; }

        [Column("sql")]
        public string Sql { get; set; }
    }
}