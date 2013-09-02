using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Northwind.MusicStore.WebUI.Tests.Unit.Controllers
{
    //TODO: This should be in SSW.Common.MVCTests
    //Resources: 
    //https://danielsaidi.wordpress.com/tag/modelstate-isvalid/
    //http://blog.overridethis.com/blog/post/2010/04/22/MVC2-Model-Validation-and-Testing-Scenarios.aspx
    //http://stackoverflow.com/questions/2657358/net-4-rtm-metadatatype-attribute-ignored-when-using-validator
    public static class ControllerExtensions
    {

        public static ActionResult CallWithModelValidation<C, R, T>(this C controller
                                                                    , Func<C, R> action
                                                                    , T model)
            where C : Controller
            where R : ActionResult
            where T : class
        {

            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults);
            foreach (var validationResult in validationResults)
            {
                controller
                    .ModelState
                    .AddModelError(validationResult.MemberNames.First(),
                                   validationResult.ErrorMessage);
            }

            return action(controller);
        }
    }
}
