﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SlimFormaturas.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlimFormaturas.Api.Controllers {
    public class ApiController : ControllerBase {
        protected readonly NotificationHandler Notifications;
        protected ApiController(NotificationHandler notifications ) {
            Notifications = notifications;
        }

        protected bool IsValidOperation() {
            return !Notifications.HasNotifications;
        }

        protected new ActionResult Response(object result = null) {
            if (IsValidOperation()) {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = Notifications.Notifications.GroupBy(m => m.PropertyName).Select(a => a.ToArray())
            });
        }
    }
}
