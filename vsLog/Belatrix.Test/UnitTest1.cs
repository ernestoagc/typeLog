using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Belatrix.Helper;
namespace Belatrix.Test
{
    [TestClass]
    public class UnitTest1
    {
        #region casos fallidos
        [TestMethod]
        public void LogError_SinTipoLog()
        {
            string[] tiposLog = null;
            string[] tiposMensaje = null;
            string mensaje = "probando log";


            try
            {

                Assert.IsTrue(JobLogger.LogMessage(mensaje, tiposLog, tiposMensaje));

            }
            catch (Exception ex)
            {
                Assert.Fail("prueba fallida!. Falta tipo de mensaje");
            }

        }

        [TestMethod]
        public void LogError_SinTipoMensaje()
        {
            string[] tiposLog = { "CONSOLE" };
            string[] tiposMensaje = null;


            Console.Out.WriteLine("Llamando a PruebaFalla");


            try
            {

                JobLogger.LogMessage("mensaje de prueba. fallido", tiposLog, tiposMensaje);

            }
            catch (Exception ex)
            {
                Assert.Fail("prueba fallida!. Falta tipo de mensaje");
            }

        }

        [TestMethod]
        public void LogError_SinMensaje()
        {

            string[] tiposLog = { "CONSOLE" };
            string[] tiposMensaje = { "ERROR", "WARNING" };
            string mensaje = "";


            try
            {

                if (!JobLogger.LogMessage(mensaje, tiposLog, tiposMensaje))
                    Assert.Fail("prueba fallida!. Falta mensaje");

            }
            catch (Exception ex)
            {
                Assert.Fail("prueba fallida!");
            }
        }

        
        #endregion

        #region casos exitosos
        [TestMethod]
        public void LogExitoso_Consola()
        {
            string[] tiposLog = { "CONSOLE" };
            string[] tiposMensaje = { "ERROR", "WARNING" };
            string mensaje = "probando log Consola";

            try
            {
                Assert.IsTrue(JobLogger.LogMessage(mensaje, tiposLog, tiposMensaje));
            }
            catch (Exception ex)
            {
                Assert.Fail("prueba fallida!. Falta tipo de mensaje");
            }

        }

        [TestMethod]
        public void LogExitoso_Archivo()
        {
            string[] tiposLog = { "FILE" };
            string[] tiposMensaje = { "ERROR", "WARNING" };
            string mensaje = "probando log Archivo";

            try
            {
                Assert.IsTrue(JobLogger.LogMessage(mensaje, tiposLog, tiposMensaje));
            }
            catch (Exception ex)
            {
                Assert.Fail("prueba fallida!. Falta tipo de mensaje");
            }

        }

        [TestMethod]
        public void LogExitoso_BaseDatos()
        {
            string[] tiposLog = { "BD" };
            string[] tiposMensaje = { "ERROR", "WARNING" };
            string mensaje = "probando log Base Datos";

            try
            {
                Assert.IsTrue(JobLogger.LogMessage(mensaje, tiposLog, tiposMensaje));
            }
            catch (Exception ex)
            {
                Assert.Fail("prueba fallida!.");
            }

        }

        [TestMethod]
        public void LogExitoso_ConsolaArchivoBaseDatos()
        {
            string[] tiposLog = { "CONSOLE", "BD", "FILE" };
            string[] tiposMensaje = { "ERROR", "WARNING", "MESSAGE" };
            string mensaje = "probando log Consola, Archivo y Base Datos";

            try
            {
                Assert.IsTrue(JobLogger.LogMessage(mensaje, tiposLog, tiposMensaje));
            }
            catch (Exception ex)
            {
                Assert.Fail("prueba fallida!.");
            }

        }


        #endregion
        
    }    
}
