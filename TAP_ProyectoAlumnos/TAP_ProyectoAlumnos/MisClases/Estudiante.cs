using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP_ProyectoAlumnos.MisClases
{
    class Estudiante
    {
        private String nombre;
        
        public String Nombre
        {
            get { return nombre;}
            set { nombre = value; }
        }

        private String noControl;

        public String NoControl
        {
            get { return noControl; }
            set { noControl = value; }
        }

        private String carrera;

        public String Carrera
        {
            get { return carrera; }
            set { carrera = value; }
        }

        private int semestre;

        public int Semestre
        {
            get { return semestre; }
            set { semestre = value; }
        }

        private String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        private bool evaluacionDoc;

        public bool EvaluacionDoc
        {
            get { return evaluacionDoc; }
            set { evaluacionDoc = value; }
        }

        public Estudiante()
        {

        }

        public Estudiante(String nombre, String noControl, String carrera, int semestre, String password, bool evaluacionDoc)
        {
            this.nombre = nombre;
            this.noControl = noControl;
            this.carrera = carrera;
            this.semestre = semestre;
            this.password = password;
            this.evaluacionDoc = evaluacionDoc;
        }
    }
}
