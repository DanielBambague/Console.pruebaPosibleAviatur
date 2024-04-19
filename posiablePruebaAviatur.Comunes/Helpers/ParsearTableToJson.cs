using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace posiablePruebaAviatur.Comunes.Helpers
{
     public class ParsearTableToJson
    {
        public static List<T> TableToList <T>(DataTable tabla) {

            string json = JsonConvert.SerializeObject(tabla);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public static T TableToObject<T>(DataTable tabla)
        {
            if (tabla == null || tabla.Rows.Count == 0)
            {
                throw new ArgumentException("La tabla está vacía o es nula");
            }

            // Convertir solo las filas de la tabla a JSON
            
            string json = JsonConvert.SerializeObject(tabla.Rows[0].Table);

            T objeto = JsonConvert.DeserializeObject<T>(json.Replace("[","").Replace("]",""));
            return objeto;
        }


    }
}
