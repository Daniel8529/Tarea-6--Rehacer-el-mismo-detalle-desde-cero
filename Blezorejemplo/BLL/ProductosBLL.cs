using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System;
using Blezorejemplo.Entidades;
using Blezorejemplo.DAL;
using System.Linq.Expressions;

namespace Blezorejemplo.BLL
{
    public class ProductosBLL
    {
        private Contexto contexto;

        public ProductosBLL(Contexto _contexto)
        {
            contexto = _contexto;
        }
       
        public bool insertar(Productos inseta)
        {
            bool paso = false;
            try
            {
                if (Existes(inseta.ProductoId))

                    return Modificar(inseta);

                else
                contexto.Productos.Add(inseta);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }
        public Productos? Buscar(int ProductoId)
        {

            Productos? productos;
            try
            {
                productos = contexto.Productos.Find(ProductoId);
            }
            catch (Exception)
            {
                throw;
            }

            return productos;
        }
        public bool Existe(string descripcion)
        {

            bool encontrado = false;

            try
            {
                encontrado = contexto.Productos.Any(l => l.Descripcion.ToLower() == descripcion.ToLower());
            }
            catch (Exception)
            {
                throw;
            }

            return encontrado;
        }
        private bool Modificar(Productos productos)

        {

            bool paso = false;



            try

            {

                contexto.Database.ExecuteSqlRaw($"Delete FROM ProductosDetalles where ProductoId={productos.ProductoId}");



                foreach (var anterior in productos.DetalleProducto)

                {

                    contexto.Entry(anterior).State = EntityState.Added;

                }



                contexto.Entry(productos).State = EntityState.Modified;



                paso = contexto.SaveChanges() > 0;

            }

            catch (Exception)

            {

                throw;

            }

            finally

            {

                contexto.Dispose();

            }



            return paso;

        }


        public bool Eliminar(int id)
        {
            bool paso = false;


            try
            {

                var adicionales = contexto.Productos.Find(id);
                if (adicionales != null)
                {


                    contexto.Productos.Remove(adicionales);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return paso;


        }


        public bool Existes(int id)
        {


            bool encontrado = false;

            try
            {

                encontrado = contexto.Productos.Any(e => e.ProductoId == id);

            }
            catch (Exception)
            {
                throw;
            }

            return encontrado;

        }
        public List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
        {

            List<Productos> lista = new List<Productos>();
            try
            {
                lista = contexto.Productos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }
        public List<Productosdetalle> GetListd(Expression<Func<Productosdetalle, bool>> criterio)
        {
            List<Productosdetalle> lista = new List<Productosdetalle>();
            try
            {
                lista = contexto.ProductosDetalle.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            
            return lista;
        }
        public List<Productos> GeLista()
        {

            return contexto.Productos.ToList();



        }
        public List<Productosdetalle> GeListad()
        {

            return contexto.ProductosDetalle.ToList();



        }
        




    }

}