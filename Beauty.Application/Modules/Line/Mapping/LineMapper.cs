using Beauty.Application.Modules.Line.ViewModel;
using Beauty.Model.Line;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Line.Mapping
{
    public static class LineMapper
    {
        public static LineModel ToCreateModel(this LineViewModel line, IHttpContextAccessor httpContextAccessor)
        {
            return new LineModel
            {
                Title = line.Title,
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static LineModel ToUpdateModel(this LineModel model, LineViewModel line)
        {
            model.Title = line.Title;
            return model;
        }

        public static List<LineViewModel> ToFindAllViewModel(this IQueryable<LineModel> lines)
        {
            return lines.Select(x => new LineViewModel
            {
                Id = x.Id,
                Title = x.Title,
                ServiceCount = x.Services.Count,
            }).ToList();
        }
    }
}
