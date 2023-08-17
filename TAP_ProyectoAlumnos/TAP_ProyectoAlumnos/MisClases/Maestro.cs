using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP_ProyectoAlumnos
{
    class Maestro
    {
        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private String usuario;

        public String Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        private String materia;

        public String Materia
        {
            get { return materia; }
            set { materia = value; }
        }

        public Maestro()
        {

        }

        public Maestro(String nombre, String usuario, String password, String materia)
        {
            this.nombre = nombre;
            this.usuario = usuario;
            this.password = password;
            this.materia = materia;
        }


    }
}
