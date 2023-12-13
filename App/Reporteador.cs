using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null)
            {
                throw new ArgumentNullException(nameof(dicObsEsc));
            }

            _diccionario = dicObsEsc;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            if (_diccionario.TryGetValue(
                LlavesDiccionario.Evaluacion,
                out IEnumerable<ObjetoEscuelaBase> lista
            ))
            {
                return lista.Cast<Evaluacion>();
            }
            {
                return new List<Evaluacion>();
                /// Escribir en el log auditoria que usuario no retorno datos
            }
        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();
            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDiccionarioEvaluacionXAsignatura()
        {
            var dictaRta = new Dictionary<string, IEnumerable<Evaluacion>>();
            var listaAsignaturas = GetListaAsignaturas(out var listaEvaluaciones);


            foreach (var asig in listaAsignaturas)
            {
                var evalAsignaturas = from eval in listaEvaluaciones
                                      where eval.Asignatura.Nombre == asig
                                      select eval;

                dictaRta.Add(asig, evalAsignaturas);
            }

            return dictaRta;
        }

        public Dictionary<string, IEnumerable<object>> GetPromedioAlumnosXAsignatura()
        {
            var rta = new Dictionary<string, IEnumerable<object>>();
            var dicAsigXAsig = GetDiccionarioEvaluacionXAsignatura();

            foreach (var asigConEval in dicAsigXAsig)
            {
                var promediosAlumnos =
                from eval in asigConEval.Value
                group eval by new
                {
                    eval.Alumno.UniqueId,
                    eval.Alumno.Nombre,
                }
                into grupoEvalAlumno
                select new AlumnoPromedio
                {
                    alumnoId = grupoEvalAlumno.Key.UniqueId,
                    alumnoNombre = grupoEvalAlumno.Key.Nombre,
                    promedio = grupoEvalAlumno.Average(evaluacion => evaluacion.Nota)
                };

                rta.Add(asigConEval.Key, promediosAlumnos);
            }

            return rta;
        }


    }
}