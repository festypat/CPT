using CPT.Helper.Dto.Response;
using CPT.Helper.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static CPT.Helper.Model.NotificationModel;

namespace CPT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly INotificationTask _notification;

        protected BaseController(INotificationTask notification)
        {
            _notification = notification;
        }

        private bool IsValidOperation() => !_notification.HasNotifications;

        protected new ActionResult Response(BaseResponse response)
        {
            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(response);
            }
            else
            {
                if (response == null)
                    response = new Response();

                response.Success = false;
                response.Errors = _notification.Notifications.Select(error => error);
                switch (_notification.Notifications.LastOrDefault().NotificationType)
                {
                    case ENotificationType.InternalServerError:
                        return StatusCode((int)HttpStatusCode.InternalServerError, response);
                    case ENotificationType.BusinessRules:
                        return Conflict(response);
                    case ENotificationType.NotFound:
                        return NotFound(response);
                    default:
                        return BadRequest(response);
                }
            }
        }

        protected new IActionResult Response(dynamic response)
        {
            try
            {
                if (IsValidOperation())
                {
                    if (response.StatusCode == HttpResponseCodes.Success)
                        return Ok(new
                        {
                            success = true,
                            result = response
                        });

                    if (response.StatusCode == HttpResponseCodes.BadRequest)
                        return BadRequest(new
                        {
                            success = false,
                            result = response
                        });

                    if (response.StatusCode == HttpResponseCodes.RecordNotFound)
                        return NotFound(new
                        {
                            success = false,
                            result = response
                        });

                    if (response.StatusCode == HttpResponseCodes.DuplicateRecord)
                        return Conflict(new
                        {
                            success = false,
                            result = response
                        });

                    if (response.StatusCode == HttpResponseCodes.InternalError)
                        return StatusCode(StatusCodes.Status500InternalServerError, (new
                        {
                            success = false,
                            result = response
                            // errors = _notification.Notifications.Select(error => error)
                        }));

                    if (response == null)
                        return NoContent();

                    return StatusCode(StatusCodes.Status500InternalServerError, (new
                    {
                        success = false,
                        errors = _notification.Notifications.Select(error => error)
                    }));
                }

                return BadRequest(new
                {
                    success = false,
                    errors = _notification.Notifications.Select(error => error)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ex
                });
            }
        }
       
        protected new IActionResult Response(int? id = null, object response = null)
        {
            if (IsValidOperation())
            {
                if (id == null)
                    return Ok(new
                    {
                        success = true,
                        data = response
                    });

                return CreatedAtAction("Get", new { id },
                    new
                    {
                        success = true,
                        data = response ?? new object()
                    });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.Notifications.Select(error => error)
            });
        }

    }

    class Response : BaseResponse
    {
    }
}
