using FoodTruckServices.Interfaces;
using FoodTruckServices.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Filters
{
    public class AuthFilter : IActionFilter
    {
        private readonly IBusiness _business;
        private const string _resourceUrl = "authFilter";
        private const string _authHeaderKey = "authentication";

        public AuthFilter(IBusiness business)
        {
            _business = business;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.ContainsKey(_authHeaderKey))
            {
                context.HttpContext.Response.StatusCode = 403;
            }
            var authHeader = context.HttpContext.Request.Headers.Single(x => x.Key == _authHeaderKey).Value;

            var tokenValidationResult = _business.ValidateToken(authHeader);
            
            if(tokenValidationResult == null || tokenValidationResult.Item2 == TokenResponseEnum.InvalidToken)
            {
                context.HttpContext.Response.StatusCode = 403;
            }
            else if(tokenValidationResult.Item2 == TokenResponseEnum.TokenExpired)
            {
                context.HttpContext.Response.StatusCode = 401;
            }
            else if (tokenValidationResult.Item1.UserId == 0)
            {
                context.HttpContext.Response.StatusCode = 403;
            }
            else
            {
                context.HttpContext.Request.HttpContext.Items.Add(Constants.Tokens.UserInfo, tokenValidationResult.Item1);
            }
        }
    }
}
