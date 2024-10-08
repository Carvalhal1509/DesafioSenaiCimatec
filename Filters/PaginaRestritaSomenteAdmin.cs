﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using DesafioSenaiCimatec.Models;
using Newtonsoft.Json;

namespace DesafioSenaiCimatec.Filters
{
    public class PaginaRestritaSomenteAdmin : ActionFilterAttribute

    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                TB_USUARIO contato = JsonConvert.DeserializeObject<TB_USUARIO>(sessaoUsuario);
                if (contato == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
                if (contato.TP_USUARIO != Enums.TP_USUARIO.Administrador && contato.TP_USUARIO != Enums.TP_USUARIO.Usuariocadastro && contato.TP_USUARIO != Enums.TP_USUARIO.Usuarioconsulta)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                }

            }

            base.OnActionExecuted(context);
        }
    }
}