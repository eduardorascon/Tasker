using System;
using AutoMapper;
using Tasker.Configuracion;

namespace Tasker.Classes
{
    public static class AutomapperTypeAdapter
    {
        public static TTarget ProyectarComo<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            var typeMap = Mapper.FindTypeMapFor(typeof (TSource), typeof (TTarget));

            if (typeMap == null)
            {
                //scan all assemblies finding Automapper Profile
                InitializeAutoMapper.InitializarAutoMapper();
            }

            try
            {
                var projection = Mapper.Map<TSource, TTarget>(source);
                return projection;
            }
            catch (Exception)
            {
                return Mapper.Map<TSource, TTarget>(source);
            }
        }

        public static TTarget ProyectarColeccionComo<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            var typeMap = Mapper.FindTypeMapFor(typeof(TSource), typeof(TTarget));

            if (typeMap == null)
            {
                //scan all assemblies finding Automapper Profile
                InitializeAutoMapper.InitializarAutoMapper();
            }

            try
            {
                var projection = Mapper.Map<TSource, TTarget>(source);
                return projection;
            }
            catch (Exception)
            {
                return Mapper.Map<TSource, TTarget>(source);
            }
        }


        public static void SetEntityODataObjectValue<T>(T entity, object dataObject, bool isEntity = true) where T : class
        {

            if (entity != null)
            {
                // recorriendo las propiedades del Entity
                foreach (var entityProp in entity.GetType().GetProperties())
                {
                    // Verificando si las propiedades del Entity existen en el DataObject
                    var dataObjectProp = dataObject.GetType().GetProperty(entityProp.Name);
                    // Actualizando solo las propiedades que Existan en el DataObject y en la Entity
                    if (dataObjectProp != null)
                    {
                        Type t = entityProp.PropertyType;
                        //recuperando el valor de la propiedad para la entidad
                        var entityPropValue = entityProp.GetValue(entity, null);
                        // recuperando el valor de la propiedad para el DataObject
                        var dataObjectPropValue = dataObjectProp.GetValue(dataObject, null);
                        //- verificando las propiedades que han cambiado, es decir solo se actualizaran las propiedades que son diferentes
                        if (entityPropValue != dataObjectPropValue)
                        {
                            //- Hacer el reemplazo de los Valores.
                            //- verificando si es una entidad
                            if (isEntity)
                            {
                                entity.GetType().GetProperty(entityProp.Name).SetValue(entity, dataObjectProp.GetValue(dataObject, null), null);
                            }
                            else
                            {
                                //- si no es una entidad entonces es el DataObject
                                dataObject.GetType().GetProperty(dataObjectProp.Name).SetValue(dataObject, entityProp.GetValue(entity, null), null);
                            }
                            //  MessageBox.Show(string.Format("Entity = {0} Type = {1}  DataObject = {2}", entityProp.Name, t.ToString(), dataObjectProp.Name));
                        }
                    }
                }
            }
        }
    }
}
