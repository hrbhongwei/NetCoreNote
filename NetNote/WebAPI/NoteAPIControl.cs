using Microsoft.AspNetCore.Mvc;
using NetNote.Services;
using NetNote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.WebAPI
{
    [Route("webapi/note")]
    public class NoteAPIControl: Controller
    {
        private INoteRepository _noteRepository;
        private INoteTypeRepository _noteTypeRepository;
        public NoteAPIControl(INoteRepository noteRepository, INoteTypeRepository noteTypeRepository)
        {
            _noteRepository = noteRepository;
            _noteTypeRepository = noteTypeRepository;
        }
        [HttpGet]
        public IActionResult Get(int pageindex=1)
        {
            var pagesize = 5;
            var notes = _noteRepository.PageList(pageindex, pagesize);
            ViewBag.PageCount = notes.Item2;
            ViewBag.PageIndex = pageindex;
            var result = notes.Item1.Select(r => new NoteViewModel
            {
                Id = r.Id,
                Title = string.IsNullOrEmpty(r.Password) ? r.Title : "内容加密",
                Content = string.IsNullOrEmpty(r.Password) ? r.Content : "",
                Attachment = string.IsNullOrEmpty(r.Password) ? r.Attachment : "",
                Type = r.Type.Name
            });
            return Ok(result);
        }
    }
}
