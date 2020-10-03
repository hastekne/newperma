using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.Net;
using System.Net.Mail;

namespace SeccionAdministrador.Helpers
{
    public class EnviarCorreo
    {
        public void EnviarEmail(string tipoSubject, string parametro1Msg, string receiver, string parametro2Msg="",string parametro3Msg="")
        {
            string message = "", subject = "";
            switch (tipoSubject)
            {
                case "Bienvenido":
                    subject = "Nombre de usuario y contraseña BP.";
                    message = @"<html>
                        <div align='center'>
                            <h1 style='font-family:arial;'> Bienvenido a la plataforma de Seguimiento y permanencia.</h1>
                        </div>
                        <body>
                            <p style='font-family:arial;'> Su nombre de usuario es:<br/>" + parametro2Msg + @"</p>
                            <p style='font-family:arial;'> Contraseña:<br>" + parametro1Msg + @"</br></p>
                        </body>
                    </html> 
                    ";
                    break;
                case "RevisarBP":
                    subject = "Buena práctica para revisión.";
                    message = @"<html>
                        <div align='center'>
                            <h1 style='font-family:arial;'>Tiene una buena práctica para revisión</h1>
                        </div>
                        <body>
                            <p style='font-family:arial;'>Título de la buena práctica:<br/>" + parametro1Msg + @"</p>
                            <div align='right'>
	                            <label style='color: 9C9998; font-family:arial; font-size:80%;'>Dar solución a la revisión lo antes posible para evitar que se acomulen.</label>
                            </div>
                        </body>
                    </html>
                    ";
                    break;
                case "ListaBP":
                    subject = "Buena práctica lista.";
                    message = @"<html>
                        <div align='center'>
                            <h1 style='font-family:arial;'>Felicidades buena práctica aprobada.</h1>
                        </div>
                        <body>
                            <div align='justify'>
                                <p style='font-family:arial;'>La buen práctica con el título <label style='font-weight:bold'>" + parametro1Msg + @"</label> fue aprobada por el revisor, proceda a elegir la plantilla y publicar su buena práctica.</p>
                            </div>
                            <div align='right' style='font-family:arial;'>
                                <label> Gracias por su colaboración.</label>
                            </div>
                       </body>
                    </html>
                    ";
                    break;
                case "CorregirBP":
                    subject = "Buena práctica para corregir.";
                    message = @"<html>
                        <div align='center'>
                            <h1 style='font-family:arial;'>Correcciones sobre buena práctica</h1>
                        </div>
                        <body>
                            <p style='font-family:arial;'>Tiene correcciones pendientes para realizar sobre la buena práctica: " + parametro1Msg + @"</p>
                             <p>Descargue los comentarios del revisor para consultar las correcciones solicitadas.</p>

                            <div align='right'>
	                            <label style='color: 9C9998; font-family:arial; font-size:80%;'>Dar solución a las correcciones para poder realizar la publicicación de su buena práctica.</label>
                            </div>
                        </body>
                    </html>
                    ";
                    break;
                case "CanceladaBP":
                    subject = "Buena práctica cancelada.";
                    message = @"<html>
                        <div align='center'>
                            <h1 style='font-family:arial;'>Buena práctica cancelada.</h1>
                        </div>
                        <body>
                            <div align='justify'>
                                <p style='font-family:arial;'>La buen práctica ha sido cancelada por observaciones del revisor, le recomendamos plantear una nueva estructura de buena práctica.</p>
                            </div>
                            <div align='right' style='font-family:arial;'>
                                <label> Gracias por su colaboración.</label>
                            </div>
                        </body>
                    </html>
                    ";
                    break;
                case "BienvenidoEnlace":
                    subject = "Nombre de usuario enlace y contraseña.";
                    message = @"<html>
                        <div align='center'>
                            <h1 style='font-family:arial;'> Bienvenido a la plataforma de Eventos.</h1>
                        </div>
                        <body>
                            <p style='font-family:arial;'> Su nombre de usuario es:<br/>" + parametro2Msg + @"</p>
                            <p style='font-family:arial;'> Contraseña:<br>" + parametro1Msg + @"</br></p>
                        </body>
                    </html> 
                    ";
                    break;

            }

            //------VER LA PARTE DEL CORREO, USUARIO y CONTRASENIA DE LA CUAL SE ENVIAN LOS CORREOS


            var senderEmail = new MailAddress("mc_mendoza@seg.guanajuato.gob.mx", "Martha Mendoza");//Ejemplo con gmaill
            var receiverEmail = new MailAddress(receiver, "Receiver");
            var password = "M2pm2019";
            var sub = subject;
            var body = message;
            var smtp = new SmtpClient
            {
                //Host = "smtp.gmail.com", para gmal 
                Host = "SMTP.Office365.com",
                Port = 587,
                EnableSsl = true,//Tenia true
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            })
            {
                smtp.Send(mess);
            }
        }
    }
}