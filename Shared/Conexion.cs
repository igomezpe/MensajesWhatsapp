using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Conexion
    {
        public List<Bloqueos> ListarObjetosBloqueados()
        {
            List<Bloqueos> list = new List<Bloqueos>();
            try
            {
                using (OracleConnection cn = new OracleConnection("DATA SOURCE=IBSBLD;PASSWORD=externo;USER ID=externo;"))
                {
                  
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cn.Open();
                        cmd.Connection = cn;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "PKG_EX_AUTOM.SEL_OBJETOSBLOQUEADOS";
                        cmd.Parameters.Add(new OracleParameter("sFECHAPROCESO", OracleDbType.Varchar2).CollectionType = OracleCollectionType.None);
                        cmd.Parameters.Add(new OracleParameter("pCursor", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output));
                       var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Bloqueos bl = new Bloqueos()
                            {
                                Id = string.Empty,
                                Nombre = reader["objeto"].ToString()
                            };

                            list.Add(bl);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}
