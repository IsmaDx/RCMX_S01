using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using S1rcmxAPI;
using S1rcmxAPI.Models;
using S1rcmxAPI.Models.ResponseModel;
namespace S1rcmxAPI.Controllers
{
    public class InitialController : ApiController
    {

        

        [HttpPost]
        public IHttpActionResult PostCodeUUID(Request rsq)
        {
            if (rsq.uuid != String.Empty || rsq.uuid != null || rsq.uuid != "")
            {
                string _uuid = rsq.uuid;
                ResponseM responseM = new ResponseM();
                CommonMethods mds = new CommonMethods();
                    try
                    {
                        
                        ShoukoEntities entities = new ShoukoEntities();

                        var aux = entities.TransactSessionId.Count(x => x.Code == _uuid) > 0;
                        if (aux)
                        {
                            return BadRequest("UUID Already Exist");
                        }
                        else
                        {
                            TransactSessionId transactSessionId = new TransactSessionId();
                            transactSessionId.Code = _uuid.ToLower();
                            entities.TransactSessionId.Add(transactSessionId);
                            entities.SaveChanges();

                            responseM.Message = "OK";
                            responseM.HTTPMethod = "GET";
                            responseM.EndPoint = "RCMX";
                            return Ok(responseM);
                        }
                    }
                    catch (Exception ex)
                    {
                        return InternalServerError(ex);
                    }
                
            }
            else
            {
                return BadRequest();
            }
         }

        [HttpPost]
        public IHttpActionResult PostLogger(PostLoggerModel Posty)
        {
            //Modelo no nulo
            if (Posty == null)
            {
                return BadRequest();
            }
            //UUID o Fecha no nulos.
            else if (Posty.UUID.Length < 36 || Posty.FecOps == null)
            {
                return BadRequest();
            }
            else
            {
                //Post
                try
                {
                    ShoukoEntities Late = new ShoukoEntities();
                    Logger Logan = new Logger();

                    Logan.UUID = Posty.UUID;
                    Logan.FecOp = Posty.FecOps;
                    Logan.SerFol = Posty.SerFol;
                    Logan.NumFol = Posty.NumFol;

                    Late.Logger.Add(Logan);
                    Late.SaveChanges();
                    return Ok("OK");
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
        }

        [HttpGet]
        public IHttpActionResult Help()
        {
            ResponseM responseM = new ResponseM();
            responseM.Message = "body: uuid: uuidv4.ToLower()";
            responseM.HTTPMethod = "POST";
            responseM.EndPoint = "PostCodeUUID";
            return Ok(responseM);
        }

        [HttpGet]
        public IHttpActionResult RCMX ()
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult UUID()
        {
            CommonMethods commonMethods = new CommonMethods();
            string tmps = commonMethods.GenUID();
            return Ok(tmps);
        }

        [HttpGet]
        public IHttpActionResult PswConfirma()
        {
            try
            {
                using (var shouko = new ShoukoEntities())
                {
                   var value = shouko.PswConfirma
                        .Select (p => p.confirma)
                        .ToList ();
                    string str = value[0].ToString();
                    var Anon = new
                    { PswConfi = str };
                    return Ok(Anon);
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
    }

