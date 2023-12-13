using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
    /// <summary>
    /// sealed no permite heredar
    /// </summary>
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {


        }

        public void Inicializar()
        {
            Escuela = new Escuela(
               "Codemotion",
               2013,
               TiposEscuela.Primaria,
               ciudad: "Quintilla",
               pais: "COLOMBIA"
           );

            CargarCursos();
            CargarAsinaturas();
            CargarEvaluaciones();
        }

        public void ImprimirDiccionario(
            Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> dic,
            bool ImprimirEval = false
        )
        {
            foreach (var objDic in dic)
            {
                Printer.WriteTitle(objDic.Key.ToString());

                foreach (var val in objDic.Value)
                {
                    switch (objDic.Key)
                    {
                        case LlavesDiccionario.Evaluacion:
                            if (ImprimirEval)
                                Console.WriteLine(val);
                            break;
                        case LlavesDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val);
                            break;
                        case LlavesDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                            break;
                        case LlavesDiccionario.Curso:
                            var cursoTmp = val as Curso;

                            if (cursoTmp != null)
                            {
                                int count = ((Curso)val).Alumnos.Count;
                                Console.WriteLine("Curso" + val.Nombre + "Alumnos: " + count);
                            }
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }

                }
            }

        }

        public Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlavesDiccionario.Escuela, new[] { Escuela });
            diccionario.Add(LlavesDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var listaTmpEvaluacion = new List<Evaluacion>();
            var listaTmpAsignaturas = new List<Asignatura>();
            var listaTmpAlumnos = new List<Alumno>();
            foreach (var cur in Escuela.Cursos)
            {
                listaTmpAlumnos.AddRange(cur.Alumnos);
                listaTmpAsignaturas.AddRange(cur.Asignaturas);

                foreach (var alumno in cur.Alumnos)
                {
                    listaTmpEvaluacion.AddRange(alumno.Evaluaciones);
                }
            }

            diccionario.Add(LlavesDiccionario.Alumno, listaTmpAlumnos.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlavesDiccionario.Asignatura, listaTmpAsignaturas.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlavesDiccionario.Evaluacion, listaTmpEvaluacion.Cast<ObjetoEscuelaBase>());

            return diccionario;
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            out int conteoAlumnos,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
        )
        {
            conteoEvaluaciones = conteoAsignaturas = conteoAlumnos = 0;

            var listObj = new List<ObjetoEscuelaBase>();
            listObj.Add(Escuela);

            if (traerCursos)
                listObj.AddRange(Escuela.Cursos);

            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (traerAsignaturas)
                    listObj.AddRange(curso.Asignaturas);

                if (traerAlumnos)
                    listObj.AddRange(curso.Alumnos);

                if (traerEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listObj.AsReadOnly();
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
        )
        {
            return GetObjetoEscuelas(out int dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluaciones,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
        )
        {
            return GetObjetoEscuelas(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluaciones,
            out int conteoCursos,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
        )
        {
            return GetObjetoEscuelas(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy);
        }
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetoEscuelas(
            out int conteoEvaluaciones,
            out int conteoCursos,
            out int conteoAsignaturas,
            bool traerEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
        )
        {
            return GetObjetoEscuelas(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy);
        }
        private List<Alumno> GeneralAlumnosRandom(int cantidad)
        {
            string[] nombres = { "Paul", "Roger", "Marisabel", "Martin", "Benjamin" };
            string[] apellidos = { "Zambrano", "Piovi", "Guerrero", "Auqui", "Saquinga" };
            string[] nombres2 = { "Alejandro", "Augusto", "Maria", "Ezequiel", "Paolo" };

            var listaAlumnos = from n1 in nombres
                               from n2 in nombres2
                               from a1 in apellidos
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };



            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }

        #region Metodos de carga
        private void CargarEvaluaciones()
        {
            var rnd = new Random(System.Environment.TickCount);
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Evaluacion ev = new Evaluacion
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = MathF.Round((float)(5 * rnd.NextDouble()), 2),
                                Alumno = alumno
                            };

                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }

            // throw new NotImplementedException();
        }

        private void CargarAsinaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>()
                {
                    new Asignatura{Nombre="Matematicas"},
                    new Asignatura{Nombre="Educacion Fisica"},
                    new Asignatura{Nombre="CCNN"},
                    new Asignatura{Nombre="Castellanos"}
                };

                curso.Asignaturas = listaAsignaturas;

                // CargarCursos
            }
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                new Curso() { Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "201",Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "301",Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "401",Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "501",Jornada = TiposJornada.Mañana }
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GeneralAlumnosRandom(cantRandom);
            }
        }
        #endregion
    }

}