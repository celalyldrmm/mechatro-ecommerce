using Microsoft.Data.SqlClient;

namespace MechatroEcommerce.Models
{
    public class Connection
    {
        public static SqlConnection ServerConnect
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection("Server=.;Database=MechatroProject;Trusted_Connection=True;TrustServerCertificate=True;");
                return sqlConnection;
            }
        }
    }
}
