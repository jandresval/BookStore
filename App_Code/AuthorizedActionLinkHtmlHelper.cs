﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BookStore.App_Code
{
    public static class AuthorizedActionLinkHtmlHelper
    {


        /// <summary>
        /// Checks user security permissions.  If allowed, displays link. Otherwise, displays nothing.
        /// </summary>
        /// <returns>Link or empty string.</returns>
        public static MvcHtmlString AuthorizedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            if (ActionIsAccessibleToUser(htmlHelper, actionName, controllerName))
            {
                return LinkExtensions.ActionLink(htmlHelper, linkText, actionName, controllerName);
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }

        /// <summary>
        /// Determines if specified action is accessible to current user.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper object.</param>
        /// <param name="actionName">Action name to test.</param>
        /// <returns>True/false if action is accessible to current user.</returns>
        public static bool ActionIsAccessibleToUser(this HtmlHelper htmlHelper, string actionName)
        {
            return AuthorizedActionLinkHtmlHelper.ActionIsAccessibleToUser(htmlHelper, actionName, String.Empty);
        }

        /// <summary>
        /// Determines if specified action is accessible to current user.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper object.</param>
        /// <param name="actionName">Action name to test.</param>
        /// <param name="controllerName">Controller name to test.</param>
        /// <returns>True/false if action is accessible to current user.</returns>
        public static bool ActionIsAccessibleToUser(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            // Fetch controller.
            ControllerBase controllerBase;
            if (string.IsNullOrWhiteSpace(controllerName))
            {
                // Assume current controller.
                controllerBase = htmlHelper.ViewContext.Controller;
            }
            else
            {
                // Perform lookup within current area.

                // Get controller factor.
                IControllerFactory controllerFactory = ControllerBuilder.Current.GetControllerFactory();

                // Get controller.
                IController controller = controllerFactory.CreateController(htmlHelper.ViewContext.RequestContext, controllerName);

                // Ensure controller exists.
                if (controller == null)
                {
                    // Controller doesn't exist.
                    throw new ArgumentException("Specified controller does not exist.");
                }

                controllerBase = (ControllerBase)controller;
            }

            // Check on authorization.
            return ActionIsAccessibleToUser(htmlHelper, actionName, controllerBase);
        }

        /// <summary>
        /// Determines if specified action is accessible to current user.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper object.</param>
        /// <param name="actionName">Action name to test.</param>
        /// <param name="controllerBase">Controller to test.</param>
        /// <returns>True/false if action is accessible to current user.</returns>
        private static bool ActionIsAccessibleToUser(this HtmlHelper htmlHelper, string actionName, ControllerBase controllerBase)
        {
            // Get controller context.
            var controllerContext = new ControllerContext(htmlHelper.ViewContext.RequestContext, controllerBase);

            // Get controller descriptor.
            var controllerDescriptor = new ReflectedControllerDescriptor(controllerBase.GetType());

            // Get action descriptor.
            var actionDescriptor = controllerDescriptor.FindAction(controllerContext, actionName);

            // Check on authorization.
            return ActionIsAuthorized(actionDescriptor, controllerContext);
        }

        /// <summary>
        /// Tests if authorization works for action.
        /// </summary>
        /// <param name="actionDescriptor">Action to test.</param>
        /// <param name="controllerContext">Controller context (including user) to test.</param>
        /// <returns>True/false if action is authorized.</returns>
        private static bool ActionIsAuthorized(ActionDescriptor actionDescriptor, ControllerContext controllerContext)
        {
            if (actionDescriptor == null)
            {
                // Action does not exist.
                return false;
            }

            // Get authorization context fo controller.
            AuthorizationContext authContext = new AuthorizationContext(controllerContext, actionDescriptor);

            // run each auth filter until on fails
            // performance could be improved by some caching
            var filters = FilterProviders.Providers.GetFilters(controllerContext, actionDescriptor);
            FilterInfo filterInfo = new FilterInfo(filters);

            foreach (IAuthorizationFilter authFilter in filterInfo.AuthorizationFilters)
            {
                // Attempt authorization.
                authFilter.OnAuthorization(authContext);

                // If result is non-null, user is not authorized.
                if (authContext.Result != null)
                {
                    return false;
                }
            }

            // Assume user is authorized.
            return true;
        }
    }
}