using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tapioca.HATEOAS;
using APIWithASPNETCore.Data.VO;

namespace APIWithASPNETCore.Hypermedia
{
    public class BookEnricher : ObjectContentResponseEnricher<PersonVO>
    {
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = urlHelper.Link("GetBook", new { id = content.Id }),
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = urlHelper.Link("GetBookPost", new { id = content.Id }),
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = urlHelper.Link("GetBookPut", new { id = content.Id }),
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = urlHelper.Link("GetBookDelete", new { id = content.Id }),
                Rel = RelationType.self,
                Type = "int"
            });

            return null;
        }
    }
}
