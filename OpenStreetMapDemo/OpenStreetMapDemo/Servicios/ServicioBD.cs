using OpenStreetMapDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStreetMapDemo.Servicios
{
    public static class ServicioBD
    {
        public static List<Lugar> ObtenerLugares()
        {
            return new List<Lugar>()
            {
                new Lugar()
                {
                    Nombre = "Guadalajara",
                    Latitud = 20.6736,
                    Longitud = -103.344
                },
                new Lugar()
                {
                    Nombre = "Cortazar",
                    Latitud = 20.4800800,
                    Longitud = -100.9606700
                },
                new Lugar()
                {
                    Nombre = "Villagran",
                    Latitud = 20.55423,
                    Longitud = -101.03719
                },
                new Lugar()
                {
                    Nombre = "Valle de Santiago",
                    Latitud = 20.3941,
                    Longitud = -101.193
                },
                new Lugar()
                {
                    Nombre = "Estados Unidos 1",
                    Latitud = 48.087931,
                    Longitud = -123.416372
                },
                new Lugar()
                {
                    Nombre = "Estados Unidos 2",
                    Latitud = 32.849559,
                    Longitud = -117.119802
                },
                new Lugar()
                {
                    Nombre = "Estados Unidos 3",
                    Latitud = 30.404131,
                    Longitud = -81.965535
                },
                new Lugar()
                {
                    Nombre = "Estados Unidos 4",
                    Latitud = 44.592935,
                    Longitud = -73.116843
                },

            };
        }
    }
}
