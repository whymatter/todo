using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TodoAPI.Extensions;
using TodoAPI.Stores;

namespace TodoAPI.Requirements {
    public class TodoListOwnerRequirement : AuthorizationHandler<TodoListOwnerRequirement>, IAuthorizationRequirement {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TodoListOwnerRequirement requirement) {
            if (!(context.Resource is AuthorizationFilterContext resource)) {
                throw new Exception($"{nameof(AuthorizationHandlerContext)} resource of wrong type ({context.Resource?.GetType()})");
            }

            var routeValues = resource.RouteData.Values;

            if (routeValues.TryGetValue("listId", out var listId) == false) {
                if (routeValues.TryGetValue("id", out listId) == false) {
                    context.Fail();
                }
            }

            var userId = context.User.GetUserId();

            var todoStore = resource.HttpContext.RequestServices.GetService<ITodoStore>();
            if (todoStore.GetTodoLists(userId).Any(o => o.Id == Convert.ToInt32(listId))) {
                context.Succeed(requirement);
            }
            else {
                context.Fail();
            }
        }
    }
}