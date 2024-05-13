using Microsoft.AspNetCore.Mvc;
using OnboardingApi.Domain.Dtos;
using OnboardingApi.Extensions;

namespace OnboardingApi.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        //public static IActionResult ProduceErrorResponse(ActionContext context)
        //{
        //    var errors = context.ModelState.GetErrorMessages();
        //    var response = new ErrorResource(messages: errors);
            
        //    return new BadRequestObjectResult(response);
        //}
    }
}