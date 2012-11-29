// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace SignalKo.SystemMonitor.Monitor.Web.Controllers {
    public partial class AgentConfigurationController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AgentConfigurationController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Save() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Save);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AgentConfigurationController Actions { get { return MVC.AgentConfiguration; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "AgentConfiguration";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "AgentConfiguration";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string Load = "Load";
            public readonly string Save = "Save";
            public readonly string Editor = "Editor";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants {
            public const string Load = "Load";
            public const string Save = "Save";
            public const string Editor = "Editor";
        }


        static readonly ActionParamsClass_Save s_params_Save = new ActionParamsClass_Save();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Save SaveParams { get { return s_params_Save; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Save {
            public readonly string agentConfiguration = "agentConfiguration";
        }
        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string Editor = "~/Views/AgentConfiguration/Editor.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_AgentConfigurationController: SignalKo.SystemMonitor.Monitor.Web.Controllers.AgentConfigurationController {
        public T4MVC_AgentConfigurationController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.JsonResult Load() {
            var callInfo = new T4MVC_JsonResult(Area, Name, ActionNames.Load);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Save(SignalKo.SystemMonitor.Common.Model.AgentConfiguration agentConfiguration) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Save);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "agentConfiguration", agentConfiguration);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Editor() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Editor);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
